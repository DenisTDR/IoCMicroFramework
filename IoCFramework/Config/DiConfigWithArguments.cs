using System.Collections.Generic;

namespace IoCFramework.Config
{
    public class DiConfigWithArguments : DiConfig
    {
        public IList<object> PrimitiveArgumentList { get; set; }
    }
}