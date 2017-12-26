using Abp.Modules;
using MaiReo.Messages.Abstractions;
using MaiReo.Messages.Broker;
using MaiReo.Messages.Broker.AbpIntegration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo
{
    public class MessageBrokerModule : AbpModule
    {
        private IMessageBroker _messageBroker;
        public override void PreInitialize()
        {
            IocManager.Register<IMessageBrokerConfiguration,
                MessageBrokerModuleConfiguration>();
            IocManager.Register<IMessageBrokerModuleConfiguration,
                MessageBrokerModuleConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention( typeof( MessageBrokerModule ).Assembly );
        }

        public override void PostInitialize()
        {
            RegisterIfNot<IMessageBroker, MessageBroker>();
            _messageBroker = IocManager.Resolve<IMessageBroker>();

            var config = IocManager.Resolve
                <IMessageBrokerModuleConfiguration>();
            if (!config.AutoStartBroker)
            {
                return;
            }
            if (!_messageBroker.IsStarted)
            {
                _messageBroker.Startup();
            }

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
            _messageBroker?.Shutdown();
        }
    }
}
