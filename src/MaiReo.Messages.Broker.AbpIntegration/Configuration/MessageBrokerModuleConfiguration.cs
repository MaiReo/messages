using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Broker.AbpIntegration
{
    public class MessageBrokerModuleConfiguration : MessageBrokerConfiguration, IMessageBrokerModuleConfiguration, IMessageBrokerConfiguration
    {
        public bool AutoStartBroker { get; set; }
    }
}
