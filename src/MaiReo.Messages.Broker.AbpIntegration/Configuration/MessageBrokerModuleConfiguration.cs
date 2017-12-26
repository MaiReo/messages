using MaiReo.Messages.Abstractions;

namespace MaiReo.Messages.Broker
{
    public class MessageBrokerModuleConfiguration : MessageConfiguration, IMessageBrokerModuleConfiguration, IMessageConfiguration
    {
        public bool AutoStart { get; set; }
    }
}
