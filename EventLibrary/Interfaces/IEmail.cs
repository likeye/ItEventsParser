using System.Net.Mail;

namespace EventLibrary.Interfaces
{
    public interface IEmail
    {
        void Send(MailMessage mail);
        string PrepareBody(DB.Event parsedEvent);
        MailMessage Create(string body);
    }
}
