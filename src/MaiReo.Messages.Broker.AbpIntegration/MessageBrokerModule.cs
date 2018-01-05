using MaiReo.Messages.Abstractions;
using MaiReo.Messages.Broker;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.Modules
{
    [DependsOn( typeof( MessageAbstractionsModule ) )]
    public class MessageBrokerModule : AbpModule
    {
        private IMessageBroker _messageBroker;
        public override void PreInitialize()
        {
            IocManager.Register<IMessageBrokerModuleConfiguration,
                MessageBrokerModuleConfiguration>();
            IocManager.Register<IMessageBrokerLogger,
                ConsoleMessageBrokerLogger>();
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
            if (!config.AutoStart)
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
