#region 程序集 Version=2.1.1
/*
 * 此特性为消息结构指定不同的Topic名
 */
#endregion
using System;

namespace MaiReo.Messages.Abstractions
{
    [AttributeUsage( 
        AttributeTargets.Class, 
        Inherited = false,
        AllowMultiple = false )]
    public sealed class MessageTopicAttribute : Attribute
    {
        //private const string TOPIC_ALL = "";

        public MessageTopicAttribute( string topicName )
        {

            if (string.IsNullOrWhiteSpace( topicName ))
                throw new ArgumentException( 
                    $"{nameof( topicName )} cannot be null or white space." );
            this.TopicName = topicName;

        }

        public string TopicName { get; }

    }
}
