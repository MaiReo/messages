#region 程序集 Version=2.1.0
/*
 * Abp推荐的模块配置获取方式
 * 这是一个快捷方式
 */
#endregion
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
