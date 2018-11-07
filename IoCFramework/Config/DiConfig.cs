using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IoCFramework.Config
{
    public class DiConfig : IDiConfig<Type>
    {
        public Type InterfaceType { get; set; }
        public Type ImplementationType { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public DiType Type { get; set; }
    }
}