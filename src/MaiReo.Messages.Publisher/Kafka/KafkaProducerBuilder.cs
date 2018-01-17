#region 程序集 Version=2.1.0
/*
 * Kafka生产者构造实现类
 */
#endregion
using System;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace MaiReo.Messages.Publisher
{
    public class KafkaProducerBuilder : IKafkaProducerBuilder
    {
        public KafkaProducerBuilder()
        {
            ProducerOption = new KafkaProducerOption();
        }

        public IKafkaProducerOption ProducerOption { get; set; }


        public Producer<string, string> Build()
        {
            var config = ProducerOption.ToConfig();
            return new Producer<string, string>( config,
                new StringSerializer( Encoding.UTF8 ),
                new StringSerializer( Encoding.UTF8 ) );
        }

        public IKafkaProducerBuilder Configure(
            Action<IKafkaProducerOption> action )
        {
            if (action == null)
                throw new ArgumentNullException(
                    nameof( action ) );

            action( ProducerOption );

            return this;
        }
    }
}
