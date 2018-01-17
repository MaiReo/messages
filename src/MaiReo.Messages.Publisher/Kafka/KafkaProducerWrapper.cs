#region 程序集 Version=2.1.0
/*
 * Kafka生产者包装实现
 */
#endregion
using System;
using Confluent;
using Confluent.Kafka;
using MaiReo.Messages.Abstractions;
using System.Threading.Tasks;
using System.Threading;

namespace MaiReo.Messages.Publisher
{
    public class KafkaProducerWrapper : IMessagePublisherWrapper, IDisposable
    {
        private readonly IMessageConfiguration _messageConfiguration;

        private Producer<string, string> _producer;

        public bool IsConnected =>
            (!disposedValue) && (_producer != null);

        public KafkaProducerWrapper()
            => ProducerBuilder = new KafkaProducerBuilder();

        public KafkaProducerWrapper(
            IMessageConfiguration messageConfiguration )
            : this()
        {
            this._messageConfiguration
                = messageConfiguration.IsValid()
                ? messageConfiguration
                : throw new ArgumentException(
                    "Invalid Message Configuration" );

        }


        public IKafkaProducerBuilder ProducerBuilder { get; set; }

        public void Connect()
        {
            if (disposedValue) return;
            if (IsConnected) return;

            _producer = ProducerBuilder
                ?.Configure( Configurator )
                ?.Build();
        }

        public void Disconnect()
        {
            this.Dispose();
        }

        [Obsolete( "Use SendAsync instead.", true )]
        public void Send( IMessageWrapper message )
        {
            //No actions.
        }

        public async Task SendAsync( IMessageWrapper message )
        {
            if (IsConnected)
            {
                var retMsg = await _producer.ProduceAsync( message.Topic,
                              //nameof( message.Message ),
                              null,
                              message.Message,
                              blockIfQueueFull: false ).ConfigureAwait( false );

                if (retMsg.Error?.HasError == true)
                {
                    throw new InvalidOperationException( retMsg.Error.Reason );
                }
                return;
            }
            if (disposedValue)
            {
                throw new ObjectDisposedException(
                  nameof( _producer ) );
            }
            throw new InvalidOperationException(
              "The Producer has not been initialized." );
        }


        private void Configurator( IKafkaProducerOption option )
        {
            option.DelayBeforeSend = _messageConfiguration.PublisherDelayBeforeSend
                 ?? MessageConfiguration.Default.PublisherDelayBeforeSend;
            option.RetryCount = _messageConfiguration.PublisherRetryCount
                ?? MessageConfiguration.Default.PublisherRetryCount;
            option.ServerAddress
                = (_messageConfiguration.BrokerAddress
                ?? MessageConfiguration.Default.BrokerAddress)
                +
                ":" +
                (_messageConfiguration.BrokerPort > 0
                && _messageConfiguration.BrokerPort < 65536
                ? _messageConfiguration.BrokerPort
                : MessageConfiguration.Default.BrokerPort);
            option.Timeout = _messageConfiguration.PublisherTimeout
                ?? MessageConfiguration.Default.PublisherTimeout;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose( bool disposing )
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _producer?.Dispose();
                }
                _producer = null;
                disposedValue = true;
            }
        }

        // ~KafkaPublisherWrapper() {
        //   Dispose(false);
        // }

        public void Dispose()
        {
            Dispose( true );
        }
        #endregion

    }
}
