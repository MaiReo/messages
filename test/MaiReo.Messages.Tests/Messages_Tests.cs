using MaiReo.Messages.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Newtonsoft.Json;
using MaiReo.Messages.Publisher;
using MaiReo.Messages.Receiver;
using System.Collections.Generic;

namespace MaiReo.Messages.Tests
{
    public class Messages_Tests
    {
        [Fact]
        public async Task OneTopic()
        {
            var config = new TestMessageConfiguration();
            config.Subscription.Add( "TestTopic" );
            config.IsValid().ShouldBe( true );
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
                e.Topic.ShouldBe( "TestTopic" );
                var strongTyped = JsonConvert.DeserializeObject<TestMessage>( e.Message );
                strongTyped.String.ShouldBe( stringPropertyValue );
            };
            var publisherWrapper = new KafkaProducerWrapper( config );
            var receiverWrapper = new KafkaConsumerWrapper( config );
            var publisher = new MessagePublisher( config, publisherWrapper );
            receiverWrapper.Connect();
            publisherWrapper.Connect();
            receiverWrapper.IsConnected.ShouldBe( true );
            publisherWrapper.IsConnected.ShouldBe( true );
            await publisher.PublishAsync( message );
            publisherWrapper.Disconnect();
            publisherWrapper.IsConnected.ShouldBe( false );
            config.LatestMessagePublishingEventArgs.ShouldNotBeNull();
            await Task.Delay( 10000 ).ConfigureAwait( false );
            config.LatestMessageReceivingEventArgs.ShouldNotBeNull();
            receiverWrapper.Disconnect();
            receiverWrapper.IsConnected.ShouldBe( false );
        }

        [Fact]
        public async Task SpecifingTopics()
        {
            var config = new TestMessageConfiguration();
            var pubCount = 0;
            var subCount = 0;
            var subTopicList = new List<string>();
            config.IsValid().ShouldBe( true );
            var stringPropertyValue = "string";
            var message1 = new TestMessage
            {
                String = stringPropertyValue
            };
            var message2 = new TestMessage2
            {
                String = stringPropertyValue
            };
            var message3 = new TestMessage3
            {
                String = stringPropertyValue
            };

            config.MessagePublishing += async ( sender, e ) =>
            {
                pubCount++;
                e.ShouldBe( config.LatestMessagePublishingEventArgs );
                new[] { "TestTopic", "2TestTopic", "3TestTopic" }
                .ShouldContain( e.Topic );
                var strongTyped = JsonConvert.DeserializeObject<TestMessage>( e.Message );
                strongTyped.String.ShouldBe( stringPropertyValue );
            };
            config.Subscription.Add( "2TestTopic" );
            config.Subscription.Add( "3TestTopic" );

            config.MessageReceiving += async ( sender, e ) =>
            {
                subCount++;
                subTopicList.Add( e.Topic );
                config.Subscription.ShouldContain( e.Topic );
                var strongTyped = JsonConvert.DeserializeObject<TestMessage>( e.Message );
                strongTyped.String.ShouldBe( stringPropertyValue );
            };
            var publisherWrapper = new KafkaProducerWrapper( config );
            var receiverWrapper = new KafkaConsumerWrapper( config );
            var publisher = new MessagePublisher( config, publisherWrapper );
            receiverWrapper.Connect();
            publisherWrapper.Connect();
            await publisher.PublishAsync( message1 );
            await publisher.PublishAsync( message2 );
            await publisher.PublishAsync( message3 );
            publisherWrapper.Disconnect();
            await Task.Delay( 10000 ).ConfigureAwait( false );
            receiverWrapper.Disconnect();
            config.LatestMessagePublishingEventArgs.ShouldNotBeNull();
            config.LatestMessageReceivingEventArgs.ShouldNotBeNull();
            pubCount.ShouldBe( 3 );
            subCount.ShouldBe( 2 );
        }
    }
}
