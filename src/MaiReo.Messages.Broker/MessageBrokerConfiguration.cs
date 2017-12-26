using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public class MessageBrokerConfiguration : IMessageBrokerConfiguration
    {
        static MessageBrokerConfiguration()
        {
            Default = new MessageBrokerConfiguration
            {
                XSubAddress = "tcp://[::]:5010",
                XPubAddress = "tcp://[::]:5011",
            };
        }
        public static readonly MessageBrokerConfiguration Default;
        public MessageBrokerConfiguration()
        {

        }

        public string XSubAddress { get; set; }

        public string XPubAddress { get; set; }
    }
}
