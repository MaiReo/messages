using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessageBroker
    {
        bool IsStarted { get; }

        IMessageBroker Startup();

        IMessageBroker Shutdown();
    }
}
