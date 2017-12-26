using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using MaiReo.Messages.Abstractions.Events;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MaiReo.Messages.Tests
{
    public class TestMessageConfiguration : MessageConfiguration, IMessageConfiguration
    {
        public override string Schema => "tcp";

        public override IPAddress ListenAddress => IPAddress.IPv6Loopback;

        public override string ListenAddressForPubSub => this.GetAddress();

        public override int HighWatermark => 1000;

        public override int XSubPort => 5555;

        public override int XPubPort => 5666;

        public virtual MessagePublishingEventArgs LatestMessagePublishingEventArgs { get; protected set; }

        public virtual MessageReceivingEventArgs LatestMessageReceivingEventArgs { get; protected set; }

        public override Task OnMessagePublishingAsync( MessagePublishingEventArgs e, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            LatestMessagePublishingEventArgs = e;
            return base.OnMessagePublishingAsync( e, cancellationToken );
        }

        public override Task OnMessageReceivingAsync( MessageReceivingEventArgs e, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            LatestMessageReceivingEventArgs = e;
            return base.OnMessageReceivingAsync( e, cancellationToken );
        }
    }
}
