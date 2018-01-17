#region 程序集 Version=2.1.0
/*
 * Kafka消费者配置实现类
 */
#endregion
namespace MaiReo.Messages.Receiver
{
    public class KafkaConsumerOption : IKafkaConsumerOption
    {
        /// <summary>
        /// Kafka服务地址(地址:端口)
        /// </summary>
        public string ServerAddress { get; set; }

        public string GroupId { get; set; }
        public bool? AutoCommit { get; set; }
        public int? AutoCommitInterval { get; set; }
    }
}
