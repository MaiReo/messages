using MaiReo.Messages.Abstractions;

namespace Abp.Modules
{
    [DependsOn(
        typeof( MessageAbstractionsModule ),
        typeof( MessageBrokerModule ),
        typeof( MessagePublisherModule ),
        typeof( MessageReceiverModule )
        )]
    public class MessageModule : AbpModule
    {

    }
}
