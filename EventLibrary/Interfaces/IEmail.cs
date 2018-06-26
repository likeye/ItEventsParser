using System.Net.Mail;
using EventLibrary.EventClasses;

namespace EventLibrary.Interfaces
{
    public interface IEmail
    {
        void Send(MailMessage mail);
        string PrepareBody(Event parsedEvent);
        MailMessage Create(string body);
    }
}
