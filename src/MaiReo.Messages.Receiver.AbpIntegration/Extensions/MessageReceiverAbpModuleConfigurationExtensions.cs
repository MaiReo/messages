using Abp.Configuration.Startup;
using MaiReo.Messages.Receiver;
namespace Abp.Modules
{
    public static class MessageReceiverAbpModuleConfigurationExtensions
    {
        public static IMessageReceiverModuleConfiguration MessageReceiver(
            this IModuleConfigurations modules )
        {
            return modules.AbpConfiguration.Get<IMessageReceiverModuleConfiguration>();
        }
    }
}
