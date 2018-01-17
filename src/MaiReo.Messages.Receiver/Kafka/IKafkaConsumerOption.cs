#region 程序集 Version=2.1.0
/*
 * Kafka消费者配置定义
 */
#endregion
namespace MaiReo.Messages.Receiver
{
    public interface IKafkaConsumerOption
    {
        /// <summary>
        /// Kafka服务地址(地址:端口)
        /// </summary>
        string ServerAddress { get; set; }

        string GroupId { get; set; }

        bool? AutoCommit { get; set; }

        int? AutoCommitInterval { get; set; }

    }
}
