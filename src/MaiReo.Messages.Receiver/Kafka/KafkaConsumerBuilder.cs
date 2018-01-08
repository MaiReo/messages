using System;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace MaiReo.Messages.Receiver
{
    public class KafkaConsumerBuilder : IKafkaConsumerBuilder
    {
        public KafkaConsumerBuilder()
        {
            ConsumerOption = new KafkaConsumerOption();
        }

        public IKafkaConsumerOption ConsumerOption { get; set; }


        public Consumer<string, string> Build()
        {
            var config = ConsumerOption.ToConfig();
            return new Consumer<string, string>( config,
                new StringDeserializer( Encoding.UTF8 ),
                new StringDeserializer( Encoding.UTF8 ) );
        }

        public IKafkaConsumerBuilder Configure(
            Action<IKafkaConsumerOption> action )
        {
            if (action == null)
                throw new ArgumentNullException(
                    nameof( action ) );

            action( ConsumerOption );

            return this;
        }
    }
}
