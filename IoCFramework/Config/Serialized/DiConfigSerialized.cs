using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IoCFramework.Config.Serialized
{
    public class DiConfigSerialized : IDiConfig<string>
    {
        public string InterfaceType { get; set; }
        public string ImplementationType { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public DiType Type { get; set; }
    }
}