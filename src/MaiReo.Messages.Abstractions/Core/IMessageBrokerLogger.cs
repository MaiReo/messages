using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessageBrokerLogger
    {
        void LogReceive( IMessageWrapper message );
    }
}
