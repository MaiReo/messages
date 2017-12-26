using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Broker.AbpIntegration
{
    public interface IMessageBrokerModuleConfiguration : IMessageBrokerConfiguration
    {
        bool AutoStartBroker { get; set; }
    }
}
