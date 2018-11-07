using MessagesApp.Implementations;

namespace MessagesApp.Interfaces
{
    public interface IAlertService
    {
        void SetRecipient(Actor recipient);
        void TriggerAlert(string alertMessage);
    }
}