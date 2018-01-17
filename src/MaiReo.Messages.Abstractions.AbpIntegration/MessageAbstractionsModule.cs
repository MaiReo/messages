#region 程序集 Version=2.1.2
/*
 * Abp模块
 * 将所有消息处理程序注册到依赖注入容器
 * 将消息模块配置注册到依赖注入容器
 */
#endregion 程序集
using MaiReo.Messages.Abstractions;

namespace Abp.Modules
{
    public class MessageAbstractionsModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(
                new MessageHandlerRegistrar() );
        }
        public override void Initialize()
        {
            if (!IocManager.IsRegistered<IMessageConfiguration>())
            {
                IocManager.Register<IMessageConfiguration,
                MessageConfiguration>();
            }

        }

    }
}
