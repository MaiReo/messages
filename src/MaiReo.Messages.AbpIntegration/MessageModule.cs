#region 程序集 Version=2.1.2
/*
 * 此空白消息模块依赖于所有消息模块
 * 即依赖此模块的模块具有所有消息功能
 */
#endregion 程序集
using MaiReo.Messages.Abstractions;

namespace Abp.Modules
{
    [DependsOn(
        typeof( MessageAbstractionsModule ),
        typeof( MessagePublisherModule ),
        typeof( MessageReceiverModule )
        )]
    public class MessageModule : AbpModule
    {

    }
}
