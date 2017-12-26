using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Receiver
{
    public class MessageReceiverModuleConfiguration : IMessageReceiverModuleConfiguration
    {
        public MessageReceiverModuleConfiguration()
        {
            AutoStart = true;
        }
        public bool AutoStart { get; set; }
    }
}
