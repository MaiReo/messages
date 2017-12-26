using MaiReo.Messages.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Newtonsoft.Json;
using MaiReo.Messages.Broker;
using MaiReo.Messages.Publisher;
using MaiReo.Messages.Receiver;

namespace MaiReo.Messages.Tests
{
    public class Messages_Tests
    {
        [Fact]
        public async Task AllTopic()
        {
            var config = new TestMessageConfiguration();
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
            var broker = new MessageBroker( config );
            var publisherWrapper = new NetmqXPublisherWrapper( config );
            var receiverWrapper = new NetmqXSubscriberWrapper( config );
            var publisher = new MessagePublisher( config, publisherWrapper );
            broker.Startup();
            broker.IsStarted.ShouldBe( true );
            publisherWrapper.Connect();
            receiverWrapper.Connect();
            receiverWrapper.IsConnected.ShouldBe( true );
            await publisher.PublishAsync( message, timestamp );
            await Task.Delay( 2000 );
            publisherWrapper.Disconnect();
            receiverWrapper.Disconnect();
            receiverWrapper.IsConnected.ShouldBe( false );
            broker.Shutdown();
            broker.IsStarted.ShouldBe( false );
            config.LatestMessagePublishingEventArgs.ShouldNotBeNull();
            config.LatestMessageReceivingEventArgs.ShouldNotBeNull();

        }

        [Fact]
        public async Task SpecifingTopics()
        {
            var config = new TestMessageConfiguration();
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
            var timestamp = DateTimeOffset.UtcNow;
            config.MessagePublishing += ( sender, e ) =>
            {
                e.ShouldBe( config.LatestMessagePublishingEventArgs );
                e.Timestamp.ShouldBe( timestamp );
                new[] { "TestTopic", "TestTopic2", "TestTopic3" }
                .ShouldContain( e.Topic );
                var strongTyped = JsonConvert.DeserializeObject<TestMessage>( e.Message );
                strongTyped.String.ShouldBe( stringPropertyValue );
            };
            config.SubscribingMessageTopics = new string[] 
            {
                "TestTopic", "TestTopic2"
            };
            config.MessageReceiving += ( sender, e ) =>
            {
                e.ShouldBe( config.LatestMessageReceivingEventArgs );
                e.Timestamp.ShouldBe( timestamp );
                config.SubscribingMessageTopics.ShouldContain( e.Topic );
                var strongTyped = JsonConvert.DeserializeObject<TestMessage>( e.Message );
                strongTyped.String.ShouldBe( stringPropertyValue );
            };
            var broker = new MessageBroker( config );
            var publisherWrapper = new NetmqXPublisherWrapper( config );
            var receiverWrapper = new NetmqXSubscriberWrapper( config );
            var publisher = new MessagePublisher( config, publisherWrapper );
            broker.Startup();
            publisherWrapper.Connect();
            receiverWrapper.Connect();
            await publisher.PublishAsync( message1, timestamp );
            await Task.Delay( 2000 );
            await publisher.PublishAsync( message2, timestamp );
            await Task.Delay( 2000 );
            await publisher.PublishAsync( message3, timestamp );
            await Task.Delay( 2000 );
            publisherWrapper.Disconnect();
            receiverWrapper.Disconnect();
            broker.Shutdown();
            config.LatestMessagePublishingEventArgs.ShouldNotBeNull();
            config.LatestMessageReceivingEventArgs.ShouldNotBeNull();
        }
    }
}
