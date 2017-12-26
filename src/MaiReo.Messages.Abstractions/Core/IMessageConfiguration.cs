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
        string Schema { get; }

        IPAddress ListenAddress { get; }

        string ListenAddressForPubSub { get; }

        int HighWatermark { get; }

        int XSubPort { get; }

        int XPubPort { get; }

        event MessagePublishingEventHandler MessagePublishing;

        event MessageReceivingEventHandler MessageReceiving;

        Task OnMessagePublishingAsync( MessagePublishingEventArgs e, CancellationToken cancellationToken = default( CancellationToken ) );

        Task OnMessageReceivingAsync( MessageReceivingEventArgs e, CancellationToken cancellationToken = default( CancellationToken ) );

        string[] SubscribingMessageTopics { get; }

    }
}
