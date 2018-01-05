using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Broker
{
    public class ConsoleMessageBrokerLogger : IMessageBrokerLogger
    {
        public const string NULL = nameof( NULL );
        public const string LEVEL_INFO = "INFO";
        public const string RECV = "MESSAGE-PROXY_RECEIVED";

        public virtual void LogReceive( IMessageWrapper message )
        {
            Log( message, RECV );
        }

        private void Log( IMessageWrapper message, string tag )
        {
#if DEBUG
            System.Diagnostics.Debug
#else
            Console
#endif
            .WriteLine( string.Join( "^",
            LEVEL_INFO,
            DateTimeOffset.UtcNow.ToString(),
            tag,
            message?.Topic ?? NULL,
            (message?.Timestamp)?.ToString() ?? NULL,
            message?.Message?.Replace( "\r", "" )?.Replace( "\n", "" ) ?? NULL
            ) );
        }
    }
}
