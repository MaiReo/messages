using System;
using Confluent.Kafka;

namespace MaiReo.Messages.Receiver
{
    public interface IKafkaConsumerBuilder
    {
        IKafkaConsumerBuilder Configure(
            Action<IKafkaConsumerOption> action );
        Consumer<string, string> Build();
    }
}
