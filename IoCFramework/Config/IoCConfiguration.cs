using System.Collections.Generic;
using IoCFramework.Config.Serialized;

namespace IoCFramework.Config
{
    public class IoCConfiguration
    {
        public List<DiConfigSerialized> DiConfigs { get; set; }
        public List<DiConfigWithArgumentsSerialized> DiConfigsWithArguments { get; set; }
    }
}