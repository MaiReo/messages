#region 程序集 Version=2.1.0
/*
 * Kafka消费者构造定义
 */
#endregion
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
