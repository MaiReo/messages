using MaiReo.Messages.Abstractions;

namespace Abp.Modules
{
    public class MessageAbstractionsModule : AbpModule
    {
        public override void Initialize()
        {
            if (!IocManager.IsRegistered<IMessageConfiguration>())
            {
                IocManager.Register<IMessageConfiguration,
                MessageConfiguration>();
            }

        }

        public override void PostInitialize()
        {
            if (!IocManager.IsRegistered<IMessageBrokerLogger>())
            {
                IocManager.Register<IMessageBrokerLogger,
                NullMessageBrokerLogger>();
            }
        }

    }
}
