#region 程序集 Version=2.1.1
/*
 * 消息发送定义
 */
#endregion
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessagePublisher
    {
        /// <summary>
        /// 发送一个消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="message">消息实例</param>
        /// <param name="callerMemberName">运行时绑定的调用方名字</param>
        /// <returns></returns>
        Task PublishAsync<T>( T message,
            [CallerMemberName]string callerMemberName = "" )
            where T : class, IMessage, new();
    }

}
