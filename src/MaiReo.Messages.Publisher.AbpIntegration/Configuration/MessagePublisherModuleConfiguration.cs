#region 程序集 Version=2.1.0
/*
 * 模块自身配置实现类
 */
#endregion
using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Publisher
{
    public class MessagePublisherModuleConfiguration : IMessagePublisherModuleConfiguration
    {
        public MessagePublisherModuleConfiguration()
        {
            AutoStart = true;
        }
        public bool AutoStart { get; set; }
    }
}
