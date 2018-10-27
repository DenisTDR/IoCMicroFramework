using System;

namespace IoCFramework.Config
{
    public class DiConfig : IDiConfig<Type>
    {
        public Type InterfaceType { get; set; }
        public Type ImplementationType { get; set; }
        public DiType Type { get; set; }
    }
}