using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public class NullMessageBrokerLogger : IMessageBrokerLogger
    {
        public void LogReceive( IMessageWrapper message )
        {
            //No action.
        }

        static NullMessageBrokerLogger() => Default = new NullMessageBrokerLogger();
        public static NullMessageBrokerLogger Default;
    }
}
