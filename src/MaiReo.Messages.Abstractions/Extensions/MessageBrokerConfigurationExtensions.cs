using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public static class MessageBrokerConfigurationExtensions
    {
        public static bool IsValid(
            this IMessageBrokerConfiguration configuration )
        {
            if (configuration == null)
                return false;
            if (string.IsNullOrWhiteSpace( configuration.XPubAddress )
                || string.IsNullOrWhiteSpace( configuration.XSubAddress ))
                return false;
            if (!Uri.TryCreate( configuration.XSubAddress,
                UriKind.Absolute, out var xsubAddr ))
                return false;
            if (!Uri.TryCreate( configuration.XPubAddress,
                UriKind.Absolute, out var xpubAddr ))
                return false;
            if (xpubAddr == xsubAddr)
                return false;
            if (xpubAddr.Port == xsubAddr.Port)
                return false;
            return true;
        }
    }
}
