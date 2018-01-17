#region 程序集 Version=2.1.2
/*
 * Abp推荐的模块配置获取方式
 * 这是一个快捷方式
 */
#endregion
using Abp.Configuration.Startup;
using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.Modules
{
    public static class MessageAbpModuleConfigurationExtensions
    {
        public static IMessageConfiguration Messages(
            this IModuleConfigurations modules )
        {
            return modules.AbpConfiguration.Get<IMessageConfiguration>();
        }
    }
}
