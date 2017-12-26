using System;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MaiReo.Messages.Abstractions;
using MaiReo.Messages.Tests;

namespace MaiReo.Messages.AbpIntegration.Tests
{
    [DependsOn(
        typeof( Abp.TestBase.AbpTestBaseModule ),
        typeof(MessageModule)
        )]
    public class MessagesTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            //EF Core InMemory Db does not support transactions.
            Configuration.UnitOfWork.IsTransactional = false; 

            SetupInMemoryDb();
            IocManager.Register<IMessageConfiguration, TestMessageConfiguration>();
        }

        public override void Initialize()
        {
            Configuration.Modules.MessagesBroker().AutoStart = true;
            IocManager.RegisterAssemblyByConvention( typeof( MessagesTestModule ).GetAssembly() );
        }
        private void SetupInMemoryDb()
        {
            var services = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase();

            var serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(
                IocManager.IocContainer,
                services
            );
        }

        public override void PostInitialize()
        {

        }

        private void RegisterIfNot<TService, TImpl>( Abp.Dependency.DependencyLifeStyle lifeStyle = Abp.Dependency.DependencyLifeStyle.Singleton ) where TService : class where TImpl : class, TService
        {
            if (IocManager.IsRegistered<TService>())
            {
                return;
            }
            IocManager.Register<TService, TImpl>( lifeStyle );
        }

        private void RegisterIfNot<T>( Abp.Dependency.DependencyLifeStyle lifeStyle = Abp.Dependency.DependencyLifeStyle.Singleton ) where T : class
        {
            if (IocManager.IsRegistered<T>())
            {
                return;
            }
            IocManager.Register<T>( lifeStyle );
        }
    }
}