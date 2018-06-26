using EventLibrary.Interfaces;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace EventLibrary.Services
{
    public class EmailService : IEmail
    {
        public string PrepareBody(DB.Event parsedEvent)
        {
            if (parsedEvent == null)
                throw new ArgumentException("Event is null");

            string body = "";
            body +=
                $"A new event has appeared!!! \n Event's name: {parsedEvent.Name} event's date: {parsedEvent.Date} " +
                $"event's description: {parsedEvent.Description} \n Link: {parsedEvent.Link}";
            return body;
        }
        public MailMessage Create(string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["author"]);
            mail.To.Add(ConfigurationManager.AppSettings["sTo"]);
            mail.Subject = "New Event!";
            mail.Body = body;
            return mail;
        }
        public void Send(MailMessage mail)
        {
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["host"], int.Parse(ConfigurationManager.AppSettings["SmtpPort"]));
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }

    public class EmailServiceMock : IEmail
    {
        public string PrepareBody(DB.Event parsedEvent)
        {
            string body = "";
            body +=
                $"A new event has appeared!!! \n Event's name: {parsedEvent.Name} event's date: {parsedEvent.Date} " +
                $"event's description: {parsedEvent.Description} \n Link: {parsedEvent.Link}";
            return body;
        }
        public MailMessage Create(string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["author"]);
            mail.To.Add(ConfigurationManager.AppSettings["sTo"]);
            mail.Subject = "New Event!";
            mail.Body = body;
            return mail;
        }
        public void Send(MailMessage mail)
        {
            Console.WriteLine("Email sent");
        }
    }
}
