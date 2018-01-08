using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MaiReo.Messages.Abstractions
{

    public static class MessageConfigurationExtensions
    {
        public static bool IsValid(
            this IMessageConfiguration configuration )
        {
            if (configuration == null)
                return false;
            if (string.IsNullOrWhiteSpace( configuration.BrokerAddress ))
                return false;

            if (configuration.BrokerPort <= 0)
                return false;
            if (!Uri.TryCreate( $"tcp://{ configuration.BrokerAddress}:{ configuration.BrokerPort}",
                UriKind.Absolute, out var xsubAddr ))
                return false;
            return true;
        }
    }
}
