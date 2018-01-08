using MaiReo.Messages.Abstractions;
using MaiReo.Messages.Receiver;
using System;

namespace Abp.Modules
{
    [DependsOn( typeof( MessageAbstractionsModule ) )]
    public class MessageReceiverModule : AbpModule
    {
        private IMessageReceiverWrapper _messageReceiverWrapper;
        public override void PreInitialize()
        {
            IocManager.Register<IMessageReceiverModuleConfiguration,
                MessageReceiverModuleConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention( typeof( MessageReceiverModule ).Assembly );
        }

        public override void PostInitialize()
        {
            RegisterIfNot<IKafkaConsumerOption, KafkaConsumerOption>();
            RegisterIfNot<IKafkaConsumerBuilder, KafkaConsumerBuilder>();
            RegisterIfNot<IMessageReceiverWrapper, KafkaConsumerWrapper>();
            _messageReceiverWrapper = IocManager.Resolve<IMessageReceiverWrapper>();

            var config = IocManager.Resolve
                <IMessageReceiverModuleConfiguration>();
            if (!config.AutoStart)
            {
                return;
            }
            _messageReceiverWrapper.Connect();

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
            _messageReceiverWrapper?.Disconnect();
        }
    }
}
