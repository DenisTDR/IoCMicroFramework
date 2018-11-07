using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IoCFramework.Config
{
    public interface IDiConfig<T>
    {
        T InterfaceType { get; set; }
        T ImplementationType { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        DiType Type { get; set; }
    }
}