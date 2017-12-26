using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public class MessageWrapper
    {
        public MessageWrapper()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }

        public string Topic { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Message { get; set; }
    }
}
