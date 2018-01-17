#region 程序集 Version=2.1.1
/*
 * 对消息发送框架进行包装的定义
 */
#endregion
using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessagePublisherWrapper
    {
        /// <summary>
        /// 连接消息中心
        /// </summary>
        void Connect();
        /// <summary>
        /// 从消息中心断开连接
        /// </summary>
        void Disconnect();
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        [Obsolete( "Use SendAsync instead.", true )]
        void Send( IMessageWrapper message );
        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendAsync( IMessageWrapper message );
        /// <summary>
        /// 一个指示是否连接到消息中心的值
        /// </summary>
        bool IsConnected { get; }
    }
}
