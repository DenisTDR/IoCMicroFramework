namespace IoCFramework.Config
{
    public interface IDiConfig<T>
    {
        T InterfaceType { get; set; }
        T ImplementationType { get; set; }
        DiType Type { get; set; }
    }
}