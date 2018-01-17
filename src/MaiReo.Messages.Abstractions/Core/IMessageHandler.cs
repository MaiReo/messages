#region 程序集 Version=2.1.1
/*
 * 消息处理类定义
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessageHandler
    {
    }

    public interface IMessageHandler<in T> : IMessageHandler
        where T : class, IMessage, new()
    {
        /// <summary>
        /// 处理一个消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        Task HandleMessageAsync( T message, DateTimeOffset timestamp );
    }
}
