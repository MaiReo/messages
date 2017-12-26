using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MaiReo.Messages.Abstractions
{

    public static class MessageConfigurationExtensions
    {
        static MessageConfigurationExtensions()
        {
            ValidSchemas = new[]
            {
                "tcp",
                "udp",
                "ipc"
            };
        }
        public static readonly string[] ValidSchemas;
        public static bool IsValid(
            this IMessageConfiguration configuration )
        {
            if (configuration == null)
                return false;
            if (string.IsNullOrWhiteSpace( configuration.Schema ))
                return false;
            if (!ValidSchemas.Any( s => s == configuration.Schema ))
                return false;
            if (configuration.ListenAddress == null)
                return false;
            if (configuration.XPubPort < 80)
                return false;
            if (configuration.XSubPort < 80)
                return false;
            if (configuration.XPubPort == configuration.XSubPort)
                return false;
            var address = configuration.GetAddress();
            if (!Uri.TryCreate( $"{configuration.Schema}://{address}:{configuration.XSubPort}",
                UriKind.Absolute, out var xsubAddr ))
                return false;
            if (!Uri.TryCreate( $"{configuration.Schema}://{address}:{configuration.XPubPort}",
                UriKind.Absolute, out var xpubAddr ))
                return false;
            if (configuration.HighWatermark < 0)
                return false;
            return true;
        }

        public static string GetAddress( this IMessageConfiguration configuration )
        {
            if (configuration == null)
                return null;
            var address = configuration.ListenAddress.AddressFamily
                == System.Net.Sockets.AddressFamily.InterNetworkV6
                ? $"[{configuration.ListenAddress}]".Replace( "[[", "[" ).Replace( "]]", "]" )
                : $"{configuration.ListenAddress}";
            return address;
        }
    }
}
