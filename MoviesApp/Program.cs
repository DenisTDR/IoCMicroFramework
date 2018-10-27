using System;
using System.Collections.Generic;
using System.IO;
using IoCFramework;
using MoviesApp.Implementations;
using MoviesApp.Interfaces;
using Newtonsoft.Json;

namespace MoviesApp
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var container = SetupIoC(args);

            UseIoC(container);
            UseIoC(container);

//            _writeConfig(container);
        }

        private static void UseIoC(IIoCContainer container)
        {
//            var finder = container.GetInstance<IMovieFinder>();
//            var movie = finder.FindById(2);
//            Console.WriteLine(movie.Name);

            var lister = container.GetInstance<IMovieLister>();
            lister.DisplayMovies();
        }

        private static IIoCContainer SetupIoC(IReadOnlyList<string> args)
        {
            IIoCContainer container;
            if (args.Count < 2)
            {
                throw new Exception("Invalid arguments");
            }

            if (args[0] == "code")
            {
                if (args.Count != 3)
                {
                    throw new Exception("Invalid arguments");
                }

                container = new IoCContainer();

                if (args[1] == "txt")
                {
                    container.AddTransient<IMovieFinder, MovieFinderTxt>(args[2]);
                }
                else
                {
                    container.AddTransient<IMovieFinder, MovieFinderJson>(args[2]);
                }

                container.AddSingleton<IMovieLister, MovieLister>();
            }
            else if (args[0] == "config")
            {
                var factory = new IoCContainerFactory(args[1]);
                container = factory.CreateContainer();
            }
            else
            {
                throw new Exception("Invalid arguments");
            }

            return container;
        }

        private static void _writeConfig(IIoCContainer container)
        {
            using (var sw = new StreamWriter(new FileStream("./config/config.json", FileMode.Create)))
            {
                var settings = new JsonSerializerSettings {Formatting = Formatting.Indented};
                sw.WriteLine(JsonConvert.SerializeObject(container.GetConfiguration(), settings));
            }
        }
    }
}