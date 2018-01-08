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
        public override string BrokerAddress => "test.baishijiaju.com";

        public override int BrokerPort => Default.BrokerPort;

        public override string ReceiverGroupId => typeof( TestMessageConfiguration ).Assembly.GetName().Name;

        public override ISet<string> Subscription => new HashSet<string>()
        {
            "TestTopic"
        };

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
