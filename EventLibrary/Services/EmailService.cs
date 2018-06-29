using EventLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        private string PrepareBody(DB.Event parsedEvent)
        {
                DB.Event eventEmailSent = new Event()
                    {HasSentEmail = "Yes"};
                _dbOperations.UpdateEvent(parsedEvent.id, eventEmailSent);

                string body = "";
                body +=
                    $"A new event has appeared!!! \n Event's name: {parsedEvent.Name} \n event's date: {parsedEvent.Date} \n" +
                    $"event's description: {parsedEvent.Description} \n Link: {parsedEvent.Link} \n City {parsedEvent.City}";
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
            foreach (var dbEvent in eventList)
            {
                if (dbEvent.HasSentEmail != "Yes")
                {
                    var body = PrepareBody(dbEvent);
                    var mail = Create(body);
                    _smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"],
                        ConfigurationManager.AppSettings["password"]);
                    _smtp.EnableSsl = true;
                    _smtp.Send(mail);
                }
                else
                {
                    Console.WriteLine("Email has already been sent");
                }
            }
        }
    }
}
