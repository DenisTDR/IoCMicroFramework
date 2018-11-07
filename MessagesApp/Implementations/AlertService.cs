using MessagesApp.Interfaces;

namespace MessagesApp.Implementations
{
    public class AlertService : IAlertService
    {
        private readonly IMessageService _messageService;
        private Actor _recipient;
        private readonly Actor _alertSender;

        public AlertService(IMessageService messageService)
        {
            _messageService = messageService;
            _alertSender = new Actor
                {Name = "Alert Service", EmailAddress = "alerts@gmail.com", Phone = "075232323223"};
        }

        public void SetRecipient(Actor recipient)
        {
            _recipient = recipient;
        }

        public void TriggerAlert(string alertMessage)
        {
            _messageService.SendMessage(alertMessage, _alertSender, _recipient);
        }
    }
}