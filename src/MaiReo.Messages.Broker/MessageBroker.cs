using MaiReo.Messages.Abstractions;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MaiReo.Messages.Broker
{
    public class MessageBroker : IMessageBroker
    {
        private readonly IMessageConfiguration _configuration;
        private CancellationTokenSource _cancellationTokenSource;

        private Task _proxyTask;

        private Proxy _proxy;
        private MessageBroker()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }
        public MessageBroker(
            IMessageConfiguration configuration )
            : this()
        {
            if (!configuration.IsValid())
            {
                throw new ArgumentException( $"nameof(configuration) is not valid!" );
            }
            this._configuration = configuration;
            Logger = NullMessageBrokerLogger.Default;
        }

        public IMessageBrokerLogger Logger { get; set; }

        public bool IsStarted
            => _proxyTask != null && (!_proxyTask.IsCompleted)
            && (!_cancellationTokenSource.IsCancellationRequested);
        public IMessageBroker Startup()
        {
            if (IsStarted)
            {
                return this;
            }
            var cancellationTokenSource = _cancellationTokenSource = new CancellationTokenSource();
            _proxyTask = Task.Run( () =>
            {
                var address = _configuration.GetAddress();
                var pubAddress = $"@{_configuration.Schema}://{address}:{_configuration.XPubPort}";
                var subAddress = $"@{_configuration.Schema}://{address}:{_configuration.XSubPort}";
#if DEBUG
                System.Diagnostics.Debug.WriteLine( $"[{nameof( MessageBroker )}]XPub listening on: {pubAddress}" );
                System.Diagnostics.Debug.WriteLine( $"[{nameof( MessageBroker )}]XSub listening on: {subAddress}" );
#endif
                using (var xpubSocket = new XPublisherSocket( pubAddress ))
                using (var xsubSocket = new XSubscriberSocket( subAddress ))
                using (var controlIn = new DealerSocket( "@inproc://control-in" ))
                using (var controlOut = new DealerSocket( "@inproc://control-out" ))
                using (var controlInRecv = new DealerSocket( ">inproc://control-in" ))
                using (var controlOutRecv = new DealerSocket( ">inproc://control-out" ))
                {
                    Task.Run( () =>
                    {
                        while (!cancellationTokenSource.IsCancellationRequested)
                        {
                            try
                            {
                                var topic = controlInRecv.ReceiveFrameString();
                                var timestamp_bytes = controlInRecv.ReceiveFrameBytes();
                                var timestamp_int64 = BitConverter.ToInt64( timestamp_bytes, 0 );
                                var timestamp = new DateTimeOffset( timestamp_int64, TimeSpan.FromMinutes( 0 ) );
                                var message = controlInRecv.ReceiveFrameString();
                                var wrapper = new MessageWrapper( topic, message, timestamp );
                                Logger?.LogReceive( wrapper );
                            }
                            catch (System.ObjectDisposedException)
                            {
                                return;
                            }

                        }
                    }, cancellationTokenSource.Token );
                    //proxy messages between frontend / backend
                    var proxy = _proxy = new Proxy( xsubSocket, xpubSocket, controlIn, controlOut );
                    //WARNING:blocks indefinitely.
                    proxy.Start();
                }
            }, cancellationTokenSource.Token );
            return this;
        }

        public IMessageBroker Shutdown()
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
            try
            {
                _proxy?.Stop();
                _proxy = null;
            }
            catch
            {
            }
            
            return this;
        }

    }
}
