using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessagePublisherWrapper
    {
        void Connect();
        void Disconnect();

        void Send( IMessageWrapper message );
    }
}
