using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IoCFramework.Config;
using IoCFramework.Config.Serialized;
using Newtonsoft.Json;

namespace IoCFramework
{
    public class IoCContainerFactory
    {
        private readonly string _jsonConfigFile;

        public IoCContainerFactory(string jsonConfigFile)
        {
            _jsonConfigFile = jsonConfigFile;
        }

        public IIoCContainer CreateContainer()
        {
            IIoCContainer container = new IoCContainer();

            using (var sr = new StreamReader(new FileStream(_jsonConfigFile, FileMode.Open)))
            {
                var configuration = JsonConvert.DeserializeObject<IoCConfiguration>(sr.ReadToEnd());
                foreach (var serializedConfig in configuration.DiConfigs)
                {
                    var config = new DiConfig
                    {
                        InterfaceType = GetType(serializedConfig.InterfaceType),
                        ImplementationType = GetType(serializedConfig.ImplementationType),
                        Type = serializedConfig.Type
                    };
                    container.AddConfig(config);
                }

                foreach (var serializedConfig in configuration.DiConfigsWithArguments)
                {
                    var config = new DiConfigWithArguments
                    {
                        InterfaceType = GetType(serializedConfig.InterfaceType),
                        ImplementationType = GetType(serializedConfig.ImplementationType),
                        Type = serializedConfig.Type,
                        PrimitiveArgumentList = serializedConfig.PrimitiveArgumentList.ToList()
                    };
                    container.AddConfig(config);
                }
            }

            return container;
        }

        public static IoCConfiguration CreateForm(IEnumerable<DiConfig> configs)
        {
            var configuration = new IoCConfiguration
            {
                DiConfigs = new List<DiConfigSerialized>(),
                DiConfigsWithArguments = new List<DiConfigWithArgumentsSerialized>()
            };
            foreach (var diConfig in configs)
            {
                DiConfigSerialized configSerialized;
                if (diConfig is DiConfigWithArguments diConfigWithArguments)
                {
                    configSerialized = new DiConfigWithArgumentsSerialized
                        {PrimitiveArgumentList = diConfigWithArguments.PrimitiveArgumentList.ToList()};
                    configuration.DiConfigsWithArguments.Add((DiConfigWithArgumentsSerialized) configSerialized);
                }
                else
                {
                    configSerialized = new DiConfigSerialized();
                    configuration.DiConfigs.Add(configSerialized);
                }

                configSerialized.Type = diConfig.Type;
                configSerialized.InterfaceType = diConfig.InterfaceType.FullName;
                configSerialized.ImplementationType = diConfig.ImplementationType.FullName;
            }

            return configuration;
        }

        private static Type GetType(string fullName)
        {
            var type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(t1 => t1.FullName == fullName);

            return type;
        }
    }
}