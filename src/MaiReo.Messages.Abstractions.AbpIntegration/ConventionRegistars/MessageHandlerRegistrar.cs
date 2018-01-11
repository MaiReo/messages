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