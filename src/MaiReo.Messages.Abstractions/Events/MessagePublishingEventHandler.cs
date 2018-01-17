#region 程序集 Version=2.1.1
/*
 * 发布消息事件类型定义
 */
#endregion
using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions.Events
{
    public delegate Task MessagePublishingEventHandler( object sender, MessagePublishingEventArgs e );

}