using System;
using IoCFramework.Config;

namespace IoCFramework
{
    public interface IIoCContainer
    {
        void AddSingleton<TInterface, TType>(params object[] args);
        void AddTransient<TInterface, TType>(params object[] args);
        void AddSingleton(Type interfaceType, Type implementationType, params object[] args);
        void AddTransient(Type interfaceType, Type implementationType, params object[] args);
        void AddSingleton<TInterface>(TInterface obj);
        void AddConfig(DiConfig config);
        T GetInstance<T>();
        IoCConfiguration GetConfiguration();
    }
}