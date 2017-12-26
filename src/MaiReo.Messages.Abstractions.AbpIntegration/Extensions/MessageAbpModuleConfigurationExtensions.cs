using Abp.Configuration.Startup;
using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.Modules
{
    public static class MessageAbpModuleConfigurationExtensions
    {
        public static IMessageConfiguration Messages(
            this IModuleConfigurations modules )
        {
            return modules.AbpConfiguration.Get<IMessageConfiguration>();
        }
    }
}
