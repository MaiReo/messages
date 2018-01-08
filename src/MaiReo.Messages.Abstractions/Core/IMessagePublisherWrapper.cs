using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessagePublisherWrapper
    {
        void Connect();
        void Disconnect();

        [Obsolete( "Use SendAsync instead.", true )]
        void Send( IMessageWrapper message );

        Task SendAsync( IMessageWrapper message );

        bool IsConnected { get; }
    }
}
