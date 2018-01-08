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
