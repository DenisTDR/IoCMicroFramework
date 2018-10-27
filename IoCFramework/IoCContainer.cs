using System;
using System.Collections.Generic;
using System.Linq;
using IoCFramework.Config;

namespace IoCFramework
{
    public class IoCContainer : IIoCContainer
    {
        private readonly List<DiConfig> _configs = new List<DiConfig>();
        private readonly Dictionary<Type, object> _registeredInstances = new Dictionary<Type, object>();


        public void AddSingleton<TInterface, TType>(params object[] args)
        {
            _addService(typeof(TInterface), typeof(TType), DiType.Singleton, args);
        }

        public void AddTransient<TInterface, TType>(params object[] args)
        {
            _addService(typeof(TInterface), typeof(TType), DiType.Transient, args);
        }

        public void AddSingleton(Type interfaceType, Type implementationType, params object[] args)
        {
            _addService(interfaceType, implementationType, DiType.Singleton, args);
        }

        public void AddTransient(Type interfaceType, Type implementationType, params object[] args)
        {
            _addService(interfaceType, implementationType, DiType.Transient, args);
        }


        public void AddSingleton<TInterface>(TInterface obj)
        {
            if (_typeIsRegistered(typeof(TInterface)))
            {
                throw new Exception("The type " + typeof(TInterface).Name + " is already registered.");
            }

            _configs.Add(new DiConfig {InterfaceType = typeof(TInterface), Type = DiType.HotSingleton});
            _registeredInstances.Add(typeof(TInterface), obj);
        }

        public void AddConfig(DiConfig config)
        {
            _configs.Add(config);
        }

        public T GetInstance<T>()
        {
            return (T) _getInstance(typeof(T));
        }

        private bool _typeIsRegistered(Type type)
        {
            return _configs.Any(config => config.InterfaceType == type);
        }

        private void _addService(Type interfaceType, Type implementationType, DiType diType,
            IReadOnlyCollection<object> args = null)
        {
            if (!interfaceType.IsAssignableFrom(implementationType))
            {
                throw new Exception($"The type {implementationType.Name} is not subclass of {interfaceType.Name}");
            }

            if (_typeIsRegistered(implementationType))
            {
                throw new Exception("The type " + implementationType.Name + " is already registered.");
            }

            DiConfig config;
            if (args == null || args.Count == 0)
            {
                config = new DiConfig();
            }
            else
            {
                config = new DiConfigWithArguments {PrimitiveArgumentList = args.ToList()};
            }

            config.ImplementationType = implementationType;
            config.InterfaceType = interfaceType;
            config.Type = diType;

            _configs.Add(config);
        }

        private object _getInstance(Type type)
        {
            var config = _configs.FirstOrDefault(c => c.InterfaceType == type);
            if (config == null)
            {
                throw new Exception("Can't provide an implementation for " + type.Name + ". Type not registered.");
            }

            var instance = config.Type == DiType.Transient ? _createInstance(config) : _getInstance(config);

            if (instance == null)
            {
                throw new Exception("Couldn't find an implementation for " + type.Name);
            }

            return instance;
        }

        private object _getInstance(DiConfig config)
        {
            object instance;
            if (_registeredInstances.ContainsKey(config.InterfaceType))
            {
                instance = _registeredInstances[config.InterfaceType];
            }
            else
            {
                instance = _createInstance(config);
                _registeredInstances.Add(config.InterfaceType, instance);
            }

            return instance;
        }

        private object _createInstance(DiConfig config)
        {
            object instance;
            if (config is DiConfigWithArguments configWithArguments)
            {
                var ctor = config.ImplementationType.GetConstructor(configWithArguments.PrimitiveArgumentList
                    .Select(arg => arg.GetType()).ToArray());
                instance = ctor.Invoke(configWithArguments.PrimitiveArgumentList.ToArray());
            }
            else
            {
                instance = _createInstance(config.ImplementationType);
            }

            return instance;
        }

        private object _createInstance(Type implementationType)
        {
            var constructors = implementationType.GetConstructors();

            var paramLessCtor = constructors.FirstOrDefault(ctor => ctor.GetParameters().Length == 0);
            object instance = null;
            if (paramLessCtor != null)
            {
                instance = Activator.CreateInstance(implementationType);
            }
            else
            {
                foreach (var constructorInfo in constructors)
                {
                    var paramsTypes = constructorInfo.GetParameters().Select(param => param.ParameterType).ToList();
                    var matchedParamsTypes = TryMatchParams(paramsTypes);
                    if (matchedParamsTypes.Count == paramsTypes.Count)
                    {
                        // found a constructor that can be used for instantiation.
                        var paramsInstances = matchedParamsTypes.Select(_getInstance).ToArray();
                        instance = constructorInfo.Invoke(paramsInstances);
                    }
                }
            }

            if (instance == null)
            {
                throw new Exception("Couldn't instantiate a " + implementationType.Name);
            }

            return instance;
        }

        private List<Type> TryMatchParams(IEnumerable<Type> paramsTypes)
        {
            var matchedTypes = new List<Type>();
            foreach (var paramType in paramsTypes)
            {
                if (_typeIsRegistered(paramType))
                {
                    matchedTypes.Add(paramType);
                }
            }

            return matchedTypes;
        }

        #region configuration

        public IoCConfiguration GetConfiguration()
        {
            var configuration = IoCContainerFactory.CreateForm(this._configs);
            return configuration;
        }

        #endregion
    }
}