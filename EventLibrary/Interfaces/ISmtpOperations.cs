using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EventLibrary.EventClasses;
using EventLibrary.DB;
using EventLibrary.SMTP;
namespace EventLibrary.Interfaces
{
    public interface ISmtpOperations
    {
        void SendEmail(MailMessage mail);
        string BodyOfEmail(Event parsedEvent);
        MailMessage CreateEmail(string body);
    }
}
