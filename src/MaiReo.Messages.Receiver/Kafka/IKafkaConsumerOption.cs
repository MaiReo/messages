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
