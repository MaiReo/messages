#region 程序集 Version=2.1.1
/*
 * 判断消息配置的扩展方法
 */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MaiReo.Messages.Abstractions
{

    public static class MessageConfigurationExtensions
    {
        /// <summary>
        /// 验证消息配置是否可用
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
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
