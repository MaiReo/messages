namespace MaiReo.Messages.Publisher
{
    public class KafkaProducerOption : IKafkaProducerOption
    {
        /// <summary>
        /// Kafka服务地址(地址:端口)
        /// </summary>
        public string ServerAddress { get; set; }
        /// <summary>
        /// 发送重试次数(次)
        /// </summary>
        public int? RetryCount { get; set; }
        /// <summary>
        /// 发送前等待时间(毫秒)
        /// </summary>
        public int? DelayBeforeSend { get; set; }

        public int? Timeout { get; set; }
    }
}
