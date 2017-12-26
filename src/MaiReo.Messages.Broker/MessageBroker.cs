using MaiReo.Messages.Abstractions;
using MaiReo.Messages.Abstractions.Core;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MaiReo.Messages.Broker
{
    public class MessageBroker : IMessageBroker
    {
        private readonly IMessageBrokerConfiguration _configuration;
        private CancellationTokenSource _cancellationTokenSource;

        private Task _proxyTask;
        private MessageBroker()
        {
            _configuration = MessageBrokerConfiguration.Default;
        }

        public MessageBroker(
            IMessageBrokerConfiguration configuration ) : this()
        {
            if (!configuration.IsValid())
            {
                throw new ArgumentException( $"nameof(configuration) is not valid!" );
            }
            this._configuration = configuration;
        }
        public bool IsStarted
            => _proxyTask != null && (!_proxyTask.IsCompleted);
        public IMessageBroker Startup()
        {
            if (IsStarted)
            {
                return this;
            }
            _cancellationTokenSource = new CancellationTokenSource();
            _proxyTask = Task.Run( () =>
            {
                using (var xsubSocket = new XSubscriberSocket( "@" + _configuration.XSubAddress ))
                using (var xpubSocket = new XPublisherSocket( "@" + _configuration.XPubAddress ))
                {
                    //proxy messages between frontend / backend
                    var proxy = new Proxy( xsubSocket, xpubSocket );
                    //WARNING:blocks indefinitely.
                    proxy.Start();
                }
            }, _cancellationTokenSource.Token );
            return this;
        }

        public IMessageBroker Shutdown()
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
            return this;
        }

    }
}
