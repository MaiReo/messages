using MaiReo.Messages.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessageConfiguration
    {
        string Schema { get; set; }

        IPAddress ListenAddress { get; set; }

        string ListenAddressForPubSub { get; set; }

        int HighWatermark { get; set; }

        int XSubPort { get; set; }

        int XPubPort { get; set; }

        event MessagePublishingEventHandler MessagePublishing;

        event MessageReceivingEventHandler MessageReceiving;

        Task OnMessagePublishingAsync( MessagePublishingEventArgs e, CancellationToken cancellationToken = default( CancellationToken ) );

        Task OnMessageReceivingAsync( MessageReceivingEventArgs e, CancellationToken cancellationToken = default( CancellationToken ) );

        HashSet<string> SubscribingMessageTopics { get; }

    }
}
