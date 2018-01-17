#region 程序集 Version=2.1.0
/*
 * 模块自身配置实现类
 */
#endregion
using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Receiver
{
    public class MessageReceiverModuleConfiguration : IMessageReceiverModuleConfiguration
    {
        public MessageReceiverModuleConfiguration()
        {
            AutoStart = true;
        }
        public bool AutoStart { get; set; }
    }
}
