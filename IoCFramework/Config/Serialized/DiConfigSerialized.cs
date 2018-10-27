namespace IoCFramework.Config.Serialized
{
    public class DiConfigSerialized : IDiConfig<string>
    {
        public string InterfaceType { get; set; }
        public string ImplementationType { get; set; }
        public DiType Type { get; set; }
    }
}