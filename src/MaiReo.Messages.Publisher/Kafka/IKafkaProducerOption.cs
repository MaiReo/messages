#region 程序集 Version=2.1.0
/*
 * Kafka生产者配置项定义
 */
#endregion
namespace MaiReo.Messages.Publisher
{
    public interface IKafkaProducerOption
    {
        /// <summary>
        /// Kafka服务地址(地址:端口)
        /// </summary>
        string ServerAddress { get; set; }
        /// <summary>
        /// 发送重试次数(次)
        /// </summary>
        int? RetryCount { get; set; }
        /// <summary>
        /// 发送前等待时间(毫秒)
        /// </summary>
        int? DelayBeforeSend { get; set; }
        int? Timeout { get; set; }
    }
}
