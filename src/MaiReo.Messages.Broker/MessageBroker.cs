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

        }
        public bool IsStarted
            => _proxyTask != null && (!_proxyTask.IsCompleted)
            && (!_cancellationTokenSource.IsCancellationRequested);
        public IMessageBroker Startup()
        {
            if (IsStarted)
            {
                return this;
            }
            _cancellationTokenSource = new CancellationTokenSource();
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
                {
                    //proxy messages between frontend / backend
                    var proxy = _proxy = new Proxy( xsubSocket, xpubSocket );
                    //WARNING:blocks indefinitely.
                    proxy.Start();
                }
            }, _cancellationTokenSource.Token );
            return this;
        }

        public IMessageBroker Shutdown()
        {
            try
            {
                _proxy?.Stop();
                _proxy = null;
            }
            catch
            {
            }
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
            return this;
        }

    }
}
