using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Publisher
{
    public class MessagePublisherModuleConfiguration : IMessagePublisherModuleConfiguration
    {
        public MessagePublisherModuleConfiguration()
        {
            AutoStart = true;
        }
        public bool AutoStart { get; set; }
    }
}
