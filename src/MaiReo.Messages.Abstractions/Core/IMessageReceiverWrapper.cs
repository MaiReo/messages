#region 程序集 Version=2.1.1
/*
 * 对消息接收框架进行包装的定义
 */
#endregion
using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessageReceiverWrapper
    {
        /// <summary>
        /// 一个指示是否连接到消息中心的值
        /// </summary>
        bool IsConnected { get; }
        // <summary>
        /// 连接消息中心
        /// </summary>
        void Connect();
        /// <summary>
        /// 从消息中心断开连接
        /// </summary>
        void Disconnect();
        
    }
}
