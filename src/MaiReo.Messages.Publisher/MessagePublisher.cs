using MaiReo.Messages.Abstractions;
using MaiReo.Messages.Abstractions.Events;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MaiReo.Messages.Publisher
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IMessagePublisherWrapper _publisherWrapper;
        private readonly IMessageConfiguration _configuration;

        public MessagePublisher(
            IMessageConfiguration configuration,
            IMessagePublisherWrapper publisherWrapper )
        {
            this._configuration = configuration
                ?? throw new ArgumentNullException(
                    nameof( configuration ) );
            this._publisherWrapper = publisherWrapper
                ?? throw new ArgumentNullException(
                    nameof( publisherWrapper ) );
        }


        public virtual Task PublishAsync<T>(
            T message,
            DateTimeOffset? timestamp = null,
            [CallerMemberName]string callerMemberName = "" )
            where T : class, IMessage, new()
        {
            if (message == default( T ))
                throw new ArgumentNullException( nameof( message ) );
            return Task.Run( async () =>
             {
                 var topic = message.GetMessageTopicOrDefault<T>();
                 var json = JsonConvert.SerializeObject( message );
                 var wrapper = new MessageWrapper( topic, json, timestamp );
                 _publisherWrapper.Send( wrapper );
                 var eventArgs = new MessagePublishingEventArgs( wrapper
                     , callerMemberName );
                 await _configuration.OnMessagePublishingAsync( eventArgs );
             } );
        }

    }
}
