#region 程序集 Version=2.1.2
/*
 * 将消息处理类注册到依赖注入容器的逻辑
 */
#endregion

using Abp.Dependency;
using Castle.MicroKernel.Registration;
using MaiReo.Messages.Abstractions;

namespace Abp.Modules
{
    internal class MessageHandlerRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly( IConventionalRegistrationContext context )
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly( context.Assembly )
                .IncludeNonPublicTypes()
                .BasedOn<IMessageHandler>()
                .WithServiceAllInterfaces()
                .WithServiceSelf()
                .LifestyleTransient()
                );
        }


    }
}