using System.Collections.Generic;

namespace MaiReo.Messages.Publisher
{
    public static class KafkaProducerOptionExtensions
    {
        public static IDictionary<string, object> ToConfig(
            this IKafkaProducerOption option )
            => new Dictionary<string, object>()
            {
                { "bootstrap.servers", option?.ServerAddress },
                { "retries", option?.RetryCount ?? 0 },
                { "linger.ms", option?.DelayBeforeSend ?? 0 },
                //set message.timeout.ms but it doesn't work. 2018/1/8 15:06 +0800
                { "default.topic.config", new Dictionary<string, object>
                    {
                        { "message.timeout.ms", option?.Timeout ?? 5000 }
                    }
                }
            };
    }
}
