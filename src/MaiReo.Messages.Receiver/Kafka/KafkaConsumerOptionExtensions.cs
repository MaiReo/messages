#region 程序集 Version=2.1.0
/*
 * 扩展方法。转换配置类为Kafka配置字典
 */
#endregion
using System.Collections.Generic;

namespace MaiReo.Messages.Receiver
{
    public static class KafkaConsumerOptionExtensions
    {
        public static IDictionary<string, object> ToConfig(
            this IKafkaConsumerOption option )
        {
            var config = new Dictionary<string, object>()
            {
                { "bootstrap.servers", option?.ServerAddress },
                { "group.id",option?.GroupId ??
                    throw new System.InvalidOperationException(
                    "GroupId is required for consumer api but not set.") },
            };
            var autoCommit = option.AutoCommit ?? false;

            config.Add( "enable.auto.commit",
                autoCommit.ToString().ToLowerInvariant() );
            if (autoCommit
                && option.AutoCommitInterval.HasValue
                && option.AutoCommitInterval > 1)
            {
                config.Add( "auto.commit.interval.ms",
                    option.AutoCommitInterval.Value );
            }
            return config;
        }
    }
}
