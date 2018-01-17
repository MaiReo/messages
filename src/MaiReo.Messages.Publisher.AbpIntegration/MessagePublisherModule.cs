#region 程序集 Version=2.1.0
/*
 * 消息发布模块
 * 注册自身配置到依赖注入容器
 * 注册Apache Kafka包装类到依赖注入容器
 * 注册调用Apache Kafka包装类的消息发送实现类到依赖注入容器
 * 读取自身配置决定是否启动Apache Kafka监听代理
 */
#endregion
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
            RegisterIfNot<IKafkaProducerOption, KafkaProducerOption>();
            RegisterIfNot<IKafkaProducerBuilder, KafkaProducerBuilder>();
            RegisterIfNot<IMessagePublisherWrapper, KafkaProducerWrapper>();
            RegisterIfNot<IMessagePublisher, MessagePublisher>();
            
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
