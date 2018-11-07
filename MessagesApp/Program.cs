using System;
using System.Collections.Generic;
using System.IO;
using IoCFramework;
using MessagesApp.Implementations;
using MessagesApp.Interfaces;
using Newtonsoft.Json;

namespace MessagesApp
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var container = SetupIoC(args);
            UseIoC(container);
//            _writeConfig(container, args);
        }

        private static void UseIoC(IIoCContainer container)
        {
            var alertService = container.GetInstance<IAlertService>();
            alertService.SetRecipient(new Actor
                {Name = "dev team", EmailAddress = "dev-team@gmail.com", Phone = "057235234"});
            alertService.TriggerAlert("some test");
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

                if (args[1] == "sms")
                {
                    container.AddTransient<IMessageService, SmsService>(args[2]);
                }
                else if (args[1] == "email")
                {
                    container.AddTransient<IMessageService, EmailService>(args[2]);
                }
                else
                {
                    throw new Exception("Invalid arguments");
                }

                container.AddTransient<IAlertService, AlertService>();
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

        private static void _writeConfig(IIoCContainer container, IReadOnlyList<string> args)
        {
            using (var sw = new StreamWriter(new FileStream($"./config/config-{args[1]}.json", FileMode.Create)))
            {
                var settings = new JsonSerializerSettings {Formatting = Formatting.Indented};
                sw.WriteLine(JsonConvert.SerializeObject(container.GetConfiguration(), settings));
            }
        }
    }
}