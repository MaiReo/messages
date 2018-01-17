#region 程序集 Version=2.1.2
/*
 * 定义自一个支持自启动的Abp模块配置类
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public interface IAutoStartModuleConfiguration
    {
        /// <summary>
        /// 是否应该自动启动
        /// </summary>
        bool AutoStart { get; set; }
    }
}
