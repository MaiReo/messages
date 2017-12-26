using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public interface IAutoStartModuleConfiguration
    {
        bool AutoStart { get; set; }
    }
}
