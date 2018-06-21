using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using EventLibrary.Interfaces;
using EventLibrary.DB;
using EventLibrary.EventClasses;

namespace EventLibrary.SMTP
{
    public class SmtpOperations : ISmtpOperations
    {
        public string BodyOfEmail(Event parsedEvent)
        {
            string body = "";
            body +=
                $"A new event has appeared!!! \n Event's name: {parsedEvent.Name} event's date: {parsedEvent.Date} " +
                $"event's description: {parsedEvent.Description} \n Link: {parsedEvent.Link}";
            return body;
        }
        public MailMessage CreateEmail(string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["author"]);
            mail.To.Add(ConfigurationManager.AppSettings["sTo"]);
            mail.Subject = "New Event!";
            mail.Body = body;
            return mail;
        }
        public void SendEmail(MailMessage mail)
        {
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["host"], int.Parse(ConfigurationManager.AppSettings["SmtpPort"]));
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
