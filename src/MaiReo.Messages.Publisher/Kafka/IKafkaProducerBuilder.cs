using System;
using Confluent.Kafka;

namespace MaiReo.Messages.Publisher
{
    public interface IKafkaProducerBuilder
    {
        IKafkaProducerBuilder Configure(
            Action<IKafkaProducerOption> action );
        Producer<string, string> Build();
    }
}
