using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using MaiReo.Messages.Abstractions.Events;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

namespace MaiReo.Messages.Tests.net452
{
    public class TestMessageConfiguration : MessageConfiguration, IMessageConfiguration
    {
        public override string BrokerAddress =>
            "test.baishijiaju.com";
        //"101.200.53.90";
        //"192.168.122.1";

        public override int BrokerPort => Default.BrokerPort;

        public override string ReceiverGroupId => typeof( TestMessageConfiguration ).Assembly.GetName().Name;


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
