#region 程序集 Version=2.1.1
/*
 * 消息包装实现类
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public class MessageWrapper : IMessageWrapper
    {
        public MessageWrapper()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }

        public MessageWrapper( string topic, string message, DateTimeOffset? timestamp = null )
            : this()
        {
            Topic = topic ?? throw new ArgumentNullException( nameof( topic ) );
            Message = message ?? throw new ArgumentNullException( nameof( message ) );
            Timestamp = timestamp ?? Timestamp;
        }

        public string Topic { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Message { get; set; }
    }
}
