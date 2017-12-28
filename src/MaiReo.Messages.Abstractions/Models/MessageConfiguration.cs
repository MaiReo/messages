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
                Schema = "tcp",
                ListenAddress = IPAddress.Any,
                XSubPort = 5010,
                XPubPort = 5011,
                ListenAddressForPubSub = "messagebroker",
                HighWatermark = 1000
            };
        }

        public MessageConfiguration()
        {
            SubscribingMessageTopics = new HashSet<string>();
        }
        public virtual string Schema { get; set; }

        public virtual IPAddress ListenAddress { get; set; }

        public virtual int XSubPort { get; set; }

        public virtual int XPubPort { get; set; }

        public virtual string ListenAddressForPubSub { get; set; }

        public virtual int HighWatermark { get; set; }

        public HashSet<string> SubscribingMessageTopics { get; }

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
