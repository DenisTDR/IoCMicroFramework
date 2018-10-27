using System.Collections.Generic;

namespace IoCFramework.Config.Serialized
{
    public class DiConfigWithArgumentsSerialized : DiConfigSerialized
    {
        public IList<object> PrimitiveArgumentList { get; set; }
    }
}