using MessagesApp.Implementations;

namespace MessagesApp.Interfaces
{
    public interface IMessageService
    {
        void SendMessage(string message, Actor from, Actor to);
    }
}