using System;
using MessagesApp.Interfaces;

namespace MessagesApp.Implementations
{
    public class SmsService : IMessageService
    {
        private readonly string _smsServerPath;

        public SmsService(string smsServerPath)
        {
            _smsServerPath = smsServerPath;
        }

        public void SendMessage(string message, Actor from, Actor to)
        {
            Console.WriteLine(
                $"Sending sms through '{_smsServerPath}' from {from.Name}<{from.Phone}> to {to.Name}<{to.Phone}>\nBody: {message}");
        }
    }
}