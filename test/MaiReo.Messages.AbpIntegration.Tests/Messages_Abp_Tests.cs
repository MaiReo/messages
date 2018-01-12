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
            config.IsValid().ShouldBe( true );
            var publisher = Resolve<IMessagePublisher>();
            
            var stringPropertyValue = "string";
            var message = new TestMessage
            {
                String = stringPropertyValue
            };
            config.MessagePublishing += async ( sender, e ) =>
            {
                e.ShouldBe( config.LatestMessagePublishingEventArgs );
                e.Topic.ShouldBe( "TestTopic" );
                var strongTyped = JsonConvert.DeserializeObject<TestMessage>( e.Message );
                strongTyped.String.ShouldBe( stringPropertyValue );
            };
            config.MessageReceiving += async ( sender, e ) =>
            {
                e.ShouldBe( config.LatestMessageReceivingEventArgs );
                e.Topic.ShouldBe( "TestTopic" );
                var strongTyped = JsonConvert.DeserializeObject<TestMessage>( e.Message );
                strongTyped.String.ShouldBe( stringPropertyValue );
            };
            await publisher.PublishAsync( message ).ConfigureAwait( false );
            await Task.Delay( 10000 ).ConfigureAwait( false );
            config.LatestMessagePublishingEventArgs.ShouldNotBeNull();
            config.LatestMessageReceivingEventArgs.ShouldNotBeNull();
        }
    }
}
