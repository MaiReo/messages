#region 程序集 Version=2.1.1
/*
 * 接收消息事件参数
 */
#endregion
using System;

namespace MaiReo.Messages.Abstractions.Events
{
    public class MessageReceivingEventArgs : IMessageWrapper
    {
        public MessageReceivingEventArgs(
            IMessageWrapper messageWrapper)
        {
            this.MessageWrapper = messageWrapper
                ?? throw new ArgumentNullException(
                    nameof( messageWrapper ) );
        }
        protected virtual IMessageWrapper MessageWrapper { get; }

        public string Topic => MessageWrapper.Topic;

        public DateTimeOffset Timestamp => MessageWrapper.Timestamp;

        public string Message => MessageWrapper.Message;
    }
}