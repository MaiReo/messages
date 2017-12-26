using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public static class MessageExtensions
    {
        public static string GetMessageTopicOrDefault<T>( this T message )
            where T : class, IMessage, new()
        {
            var type = typeof( T );
            var topicName = type
                .GetCustomAttribute
                    <MessageTopicAttribute>()
                ?.TopicName
                ?? type.Name;
            if (topicName.Length > 32)
                throw new NotSupportedException(
                    "Does not support such topic type " +
                    "which length is greater than 32." );
            return topicName;
        }
    }
}
