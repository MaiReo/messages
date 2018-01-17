#region 程序集 Version=2.1.1
/*
 * 发布消息事件参数
 */
#endregion
using System;

namespace MaiReo.Messages.Abstractions.Events
{
    public class MessagePublishingEventArgs : IMessageWrapper
    {
        public MessagePublishingEventArgs(
            MessageWrapper messageWrapper,
            string callerMemberName = "" )
        {
            this.MessageWrapper = messageWrapper
                ?? throw new ArgumentNullException( 
                    nameof( messageWrapper ) );
            this.CallerMemberName = callerMemberName;
        }
        public string CallerMemberName { get; }

        protected virtual IMessageWrapper MessageWrapper { get; }

        public string Topic => MessageWrapper.Topic;

        public DateTimeOffset Timestamp => MessageWrapper.Timestamp;

        public string Message => MessageWrapper.Message;
    }
}