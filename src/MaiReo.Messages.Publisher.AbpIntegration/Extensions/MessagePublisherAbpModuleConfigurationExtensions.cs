using Abp.Configuration.Startup;
using MaiReo.Messages.Publisher;

namespace Abp.Modules
{
    public static class MessagePublisherAbpModuleConfigurationExtensions
    {
        public static IMessagePublisherModuleConfiguration MessagePublisher(
            this IModuleConfigurations modules )
        {
            return modules.AbpConfiguration.Get<IMessagePublisherModuleConfiguration>();
        }
    }
}
