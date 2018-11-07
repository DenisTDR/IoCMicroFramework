using System;
using MessagesApp.Interfaces;

namespace MessagesApp.Implementations
{
    public class EmailService : IMessageService
    {
        private readonly string _smtpServer;

        public EmailService(string smtpServer)
        {
            _smtpServer = smtpServer;
        }

        public void SendMessage(string message, Actor @from, Actor to)
        {
            Console.WriteLine(
                $"Sending email through '{_smtpServer}' from {from.Name}<{from.EmailAddress}> to {to.Name}<{to.EmailAddress}>\nBody: {message}");
        }
    }
}