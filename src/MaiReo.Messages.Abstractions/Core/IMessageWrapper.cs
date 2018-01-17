#region 程序集 Version=2.1.1
/*
 * 对消息的包装定义
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessageWrapper
    {
        /// <summary>
        /// 消息的话题
        /// </summary>
        string Topic { get; }
        /// <summary>
        /// 消息的时间戳
        /// </summary>
        DateTimeOffset Timestamp { get; }
        /// <summary>
        /// 消息正文
        /// </summary>
        string Message { get; }
    }
}
