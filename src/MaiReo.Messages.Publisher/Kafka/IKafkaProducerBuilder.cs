#region 程序集 Version=2.1.0
/*
 * 构造Kafka生产者的定义
 */
#endregion
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
