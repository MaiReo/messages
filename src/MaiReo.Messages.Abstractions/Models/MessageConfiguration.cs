using MaiReo.Messages.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions
{
    public class MessageConfiguration : IMessageConfiguration
    {
        public static readonly MessageConfiguration Default;
        static MessageConfiguration()
        {
            Default = new MessageConfiguration
            {
                BrokerPort = 8092,
                BrokerAddress = "127.0.0.1",
                PublisherDelayBeforeSend = 1,
                PublisherRetryCount = 0,
                ReceiverAutoCommitEnabled = true,
                ReceiverAutoCommitInterval = 100,
                PublisherTimeout = 5000
            };
        }

        public MessageConfiguration()
        {
            Subscription = new HashSet<string>();
        }
        public virtual string BrokerAddress { get; set; }
        public virtual int BrokerPort { get; set; }

        public virtual ISet<string> Subscription { get; }
        public virtual string ReceiverGroupId { get; set; }
        public int? PublisherRetryCount { get; set; }
        public int? PublisherDelayBeforeSend { get; set; }
        public bool? ReceiverAutoCommitEnabled { get; set; }
        public int? ReceiverAutoCommitInterval { get; set; }
        public int? PublisherTimeout { get; set; }

        public virtual event MessagePublishingEventHandler MessagePublishing;
        public virtual event MessageReceivingEventHandler MessageReceiving;

        public virtual Task OnMessagePublishingAsync( MessagePublishingEventArgs e, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            return Task.Run( () =>
            {
                MessagePublishing?.Invoke( this, e );
            }, cancellationToken );
        }

        public virtual Task OnMessageReceivingAsync( MessageReceivingEventArgs e, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            return Task.Run( () =>
            {
                MessageReceiving?.Invoke( this, e );
            }, cancellationToken );
        }
    }
}
