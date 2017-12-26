using MaiReo.Messages.Abstractions;
using MaiReo.Messages.Abstractions.Events;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MaiReo.Messages.Receiver
{
    public class NetmqXSubscriberWrapper : IMessageReceiverWrapper
    {
        private readonly IMessageConfiguration _configuration;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _receiveTask;
        private NetmqXSubscriberWrapper()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }
        public NetmqXSubscriberWrapper(
            IMessageConfiguration configuration)
            : this()
        {
            if (!configuration.IsValid())
                throw new ArgumentException("configuration is invalid!",
                    nameof(configuration));
            this._configuration = configuration;
        }

        public virtual void Connect()
        {
            if (IsConnected)
                return;

            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
            var connectionString = $">{_configuration.Schema}://{_configuration.ListenAddressForPubSub}:{_configuration.XPubPort}";
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[{nameof(NetmqXSubscriberWrapper)}] Sub on:{connectionString}");
#endif
            var cancellationTokenSource = _cancellationTokenSource = new CancellationTokenSource();
            _receiveTask = Task.Run(() =>
           {
               using (var subscriber = new SubscriberSocket(connectionString))
               {
                   subscriber.Options.ReceiveHighWatermark
                           = _configuration.HighWatermark;
                   if (_configuration.SubscribingMessageTopics?.Any() == true)
                   {
                       foreach (var topic in _configuration.SubscribingMessageTopics)
                       {
                           subscriber.Subscribe(topic);
                       }
                   }
                   else
                   {
                       subscriber.SubscribeToAnyTopic();
                   }
                   Task.Delay(500).GetAwaiter().GetResult();
                   do
                   {
                       var topic = subscriber.ReceiveFrameString();
                       var timestamp = new DateTimeOffset(BitConverter.ToInt64(subscriber.ReceiveFrameBytes(), 0), TimeSpan.FromMinutes(0));
                       var message = subscriber.ReceiveFrameString();
                       var wrapper = new MessageWrapper(topic, message, timestamp);
                       var eventArgs = new MessageReceivingEventArgs(wrapper);
                       _configuration.OnMessageReceivingAsync(eventArgs, cancellationTokenSource.Token);
                   } while (!cancellationTokenSource.IsCancellationRequested);
               }
           }, cancellationTokenSource.Token);
        }

        public virtual void Disconnect()
        {
            if (!IsConnected)
                return;
            _cancellationTokenSource.Cancel();
        }


        #region IDisposable Support
        private bool disposedValue = false;

        public bool IsConnected =>
            (_receiveTask != null)
            && (!_receiveTask.IsCompleted)
            && (!_cancellationTokenSource
                .IsCancellationRequested);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!_cancellationTokenSource.IsCancellationRequested)
                    {
                        _cancellationTokenSource.Cancel();
                    }
                }
                _cancellationTokenSource = null;
                _receiveTask = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
