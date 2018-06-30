using EventLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using EventLibrary.DB;
using NUnit.Framework.Internal.Commands;

namespace EventLibrary.Services
{
    public class EmailService : IEmail
    {
        private readonly DataBaseOperations _dbOperations = new DataBaseOperations();
        private readonly SmtpClient _smtp = new SmtpClient(ConfigurationManager.AppSettings["host"], int.Parse(ConfigurationManager.AppSettings["SmtpPort"]));

        private string PrepareBody(IEnumerable<DB.Event> parsedEvents)
        {
            string body = "";
            foreach (var parsedEvent in parsedEvents)
            {
                if (parsedEvent.HasSentEmail != "Yes")
                {
                    DB.Event eventEmailSent = new Event()
                    { HasSentEmail = "Yes" };
                    _dbOperations.UpdateEvent(parsedEvent.id, eventEmailSent);
                    body +=
                        $"A new event has appeared!!! \n Event's name: {parsedEvent.Name} \n event's date: {parsedEvent.Date} \n" +
                        $"event's description: {parsedEvent.Description} \n Link: {parsedEvent.Link} \n City {parsedEvent.City} \n";
                }
            }
            return body;
        }
        private MailMessage Create(string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["author"]);
            mail.To.Add(ConfigurationManager.AppSettings["sTo"]);
            mail.Subject = "New Event!";
            mail.Body = body;
            return mail;
        }
        public void Send(IEnumerable<DB.Event> eventList)
        {
            var body = PrepareBody(eventList);
           
            if (body.Any())
            {
                var mail = Create(body);
                _smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"],
                    ConfigurationManager.AppSettings["password"]);
                _smtp.EnableSsl = true;
                _smtp.Send(mail);
            }
        }
           
    }
}
