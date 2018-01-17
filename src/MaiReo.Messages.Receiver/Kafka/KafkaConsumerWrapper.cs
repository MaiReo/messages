#region 程序集 Version=2.1.0
/*
 * Kafka消费者包装实现类
 */
#endregion
using Confluent;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using MaiReo.Messages.Abstractions;
using MaiReo.Messages.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaiReo.Messages.Receiver
{
    public class KafkaConsumerWrapper : IMessageReceiverWrapper, IDisposable
    {
        private readonly IMessageConfiguration _messageConfiguration;
        private Consumer<string, string> _consumer;
        private CancellationTokenSource _cancellationTokenSource;
        public bool IsConnected
            => (!disposedValue) && _consumer != null
            && (!_cancellationTokenSource.IsCancellationRequested);

        private TimeSpan PoolTimeout;

        public IKafkaConsumerBuilder ConsumerBuilder { get; set; }

        public KafkaConsumerWrapper()
        {
            PoolTimeout = TimeSpan.FromSeconds( 1 );
            ConsumerBuilder = new KafkaConsumerBuilder();
        }

        public KafkaConsumerWrapper(
            IMessageConfiguration messageConfiguration ) : this()
        {
            this._messageConfiguration
                = messageConfiguration.IsValid()
                ? messageConfiguration
                : throw new ArgumentException(
                    "Invalid Message Configuration" );

        }



        public void Connect()
        {
            if (disposedValue)
                throw new ObjectDisposedException( nameof( _consumer ) );
            var consumer = _consumer = ConsumerBuilder
                ?.Configure( Configurator )
                ?.Build();
            if (_messageConfiguration.Subscription?.Any() == true)
            {
                consumer?.Subscribe( _messageConfiguration.Subscription );
            }
            Task.Delay( 500 ).GetAwaiter().GetResult();
            var cancellationTokenSource
                = _cancellationTokenSource = new CancellationTokenSource();
            Task.Run( () =>
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    if (consumer.Consume( out var msg, PoolTimeout ))
                    {
                        var topic = msg.Topic;
                        var timestamp =
#if NET45
                        new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.FromHours(0))
                        .AddMilliseconds(
#else
                        DateTimeOffset.FromUnixTimeMilliseconds(
#endif

                        msg.Timestamp.UnixTimestampMs );
                        var message = msg.Value ?? "{}";
                        var wrapper = new MessageWrapper( topic, message, timestamp );
                        var eventArgs = new MessageReceivingEventArgs( wrapper );
                        _messageConfiguration.OnMessageReceivingAsync(
                            eventArgs, cancellationTokenSource.Token );
                    }
                }
            }, cancellationTokenSource.Token );
        }

        public void Disconnect()
        {
            if (disposedValue)
                return;
            _cancellationTokenSource?.Cancel();
            Task.Delay( PoolTimeout ).GetAwaiter().GetResult();
        }

        private void Configurator( IKafkaConsumerOption option )
        {
            option.GroupId = _messageConfiguration.ReceiverGroupId;
            option.ServerAddress
                = (_messageConfiguration.BrokerAddress
                ?? MessageConfiguration.Default.BrokerAddress)
                +
                ":" +
                (_messageConfiguration.BrokerPort > 0
                && _messageConfiguration.BrokerPort < 65536
                ? _messageConfiguration.BrokerPort
                : MessageConfiguration.Default.BrokerPort);
            option.AutoCommit = _messageConfiguration.ReceiverAutoCommitEnabled
                ?? MessageConfiguration.Default.ReceiverAutoCommitEnabled;
            option.AutoCommitInterval = _messageConfiguration.ReceiverAutoCommitInterval
                ?? MessageConfiguration.Default.ReceiverAutoCommitInterval;
        }

#region IDisposable Support
        private bool disposedValue = false;


        protected virtual void Dispose( bool disposing )
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Disconnect();
                }
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
                _consumer = null;
                disposedValue = true;
            }
        }

        // ~KafkaConsumerWrapper() {
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            Dispose( true );
        }
#endregion
    }
}
