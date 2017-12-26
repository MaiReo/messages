using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessageWrapper
    {
        string Topic { get; }

        DateTimeOffset Timestamp { get; }

        string Message { get; }
    }
}
