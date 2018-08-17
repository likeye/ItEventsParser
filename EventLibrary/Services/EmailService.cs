using EventLibrary.DB;
using EventLibrary.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace EventLibrary.Services
{
    public class EmailService : IEmail
    {
        private readonly DataBaseOperations _dbOperations = new DataBaseOperations();
        private readonly SmtpClient _smtp = new SmtpClient(ConfigurationManager.AppSettings["host"], int.Parse(ConfigurationManager.AppSettings["smtpPort"]));
        private string PrepareBody(IEnumerable<EventDb> parsedEvents)
        {
            var body = "";
            foreach (var parsedEvent in parsedEvents)
            {
                if (parsedEvent.HasSentEmail == "Yes") continue;
                var eventEmailSent = new EventDb()
                    { HasSentEmail = "Yes" };
                _dbOperations.UpdateEvent(parsedEvent.id, eventEmailSent);
                body +=
                    $"A new event has appeared!!! \n Event's name: {parsedEvent.Name} \n event's date: {parsedEvent.Date} \n" +
                    $"event's description: {parsedEvent.Description} \n Link: {parsedEvent.Link} \n City {parsedEvent.City} \n";
            }
            return body;
        }
        private MailMessage Create(string body)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["author"]);
            mail.To.Add(ConfigurationManager.AppSettings["sTo"]);
            mail.Subject = "New Event!";
            mail.Body = body;
            return mail;
        }
        public void Send(IEnumerable<EventDb> eventList)
        {
            var body = PrepareBody(eventList);

            if (!body.Any()) return;
            var mail = Create(body);
            _smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"],
                ConfigurationManager.AppSettings["password"]);
            _smtp.EnableSsl = true;
            _smtp.Send(mail);
        }
    }
}
