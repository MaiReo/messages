using MaiReo.Messages.Abstractions;
using MaiReo.Messages.Tests;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MaiReo.Messages.AbpIntegration.Tests
{
    public class Messages_Abp_Tests : MessagesTestBase
    {
        [Fact]
        public async Task Messages_Abp_Test()
        {
            var config = Resolve<IMessageConfiguration>() as TestMessageConfiguration;
            var publisher = Resolve<IMessagePublisher>();
            config.IsValid().ShouldBe( true );
            var stringPropertyValue = "string";
            var message = new TestMessage
            {
                String = stringPropertyValue
            };
            var timestamp = DateTimeOffset.UtcNow;
            config.MessagePublishing += ( sender, e ) =>
            {
                e.ShouldBe( config.LatestMessagePublishingEventArgs );
                e.Timestamp.ShouldBe( timestamp );
                e.Topic.ShouldBe( "TestTopic" );
                var strongTyped = JsonConvert.DeserializeObject<TestMessage>( e.Message );
                strongTyped.String.ShouldBe( stringPropertyValue );
            };
            config.MessageReceiving += ( sender, e ) =>
            {
                e.ShouldBe( config.LatestMessageReceivingEventArgs );
                e.Timestamp.ShouldBe( timestamp );
                e.Topic.ShouldBe( "TestTopic" );
                var strongTyped = JsonConvert.DeserializeObject<TestMessage>( e.Message );
                strongTyped.String.ShouldBe( stringPropertyValue );
            };
            await publisher.PublishAsync( message, timestamp );
            await Task.Delay( 2000 );

            config.LatestMessagePublishingEventArgs.ShouldNotBeNull();
            config.LatestMessageReceivingEventArgs.ShouldNotBeNull();
        }
    }
}
