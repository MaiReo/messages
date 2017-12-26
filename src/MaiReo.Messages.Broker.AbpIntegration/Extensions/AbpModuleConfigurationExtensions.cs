using Abp.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Broker.AbpIntegration
{
    public static class AbpModuleConfigurationExtensions
    {
        public static IMessageBrokerModuleConfiguration MaiReoMessagesBroker(
            this IModuleConfigurations modules )
        {
            return modules.AbpConfiguration.Get<IMessageBrokerModuleConfiguration>();
        }
    }
}
