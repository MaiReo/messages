using Abp.Configuration.Startup;
using MaiReo.Messages.Broker;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.Modules
{
    public static class MessageBrokerAbpModuleConfigurationExtensions
    {
        public static IMessageBrokerModuleConfiguration MessagesBroker(
            this IModuleConfigurations modules )
        {
            return modules.AbpConfiguration.Get<IMessageBrokerModuleConfiguration>();
        }
    }
}
