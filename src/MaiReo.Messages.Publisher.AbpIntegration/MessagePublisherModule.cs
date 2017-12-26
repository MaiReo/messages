using MaiReo.Messages.Abstractions;
using MaiReo.Messages.Publisher;

namespace Abp.Modules
{
    [DependsOn( typeof( MessageAbstractionsModule ) )]
    public class MessagePublisherModule : AbpModule
    {
        private IMessagePublisherWrapper _messagePublisherWrapper;
        public override void PreInitialize()
        {
            IocManager.Register<IMessagePublisherModuleConfiguration,
                MessagePublisherModuleConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention( typeof( MessagePublisherModule ).Assembly );
        }

        public override void PostInitialize()
        {
            RegisterIfNot<IMessagePublisher, MessagePublisher>();
            RegisterIfNot<IMessagePublisherWrapper, NetmqXPublisherWrapper>();
            _messagePublisherWrapper = IocManager.Resolve<IMessagePublisherWrapper>();

            var config = IocManager.Resolve
                <IMessagePublisherModuleConfiguration>();
            if (!config.AutoStart)
            {
                return;
            }
            _messagePublisherWrapper.Connect();

        }

        private void RegisterIfNot<TService, TImpl>(
            Abp.Dependency.DependencyLifeStyle lifeStyle
            = Abp.Dependency.DependencyLifeStyle.Singleton )
            where TImpl : class, TService
            where TService : class
        {
            if (IocManager.IsRegistered<TService>())
            {
                return;
            }
            IocManager.Register<TService, TImpl>( lifeStyle );
        }
        public override void Shutdown()
        {
            _messagePublisherWrapper?.Disconnect();
        }
    }
}
