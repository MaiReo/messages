using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessageReceiverWrapper
    {
        bool IsConnected { get; }
        void Connect();
        void Disconnect();
        
    }
}
