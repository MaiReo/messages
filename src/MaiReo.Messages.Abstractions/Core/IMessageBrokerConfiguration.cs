using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessageBrokerConfiguration
    {
        string XSubAddress { get; set; }

        string XPubAddress { get; set; }


    }
}
