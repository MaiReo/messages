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
