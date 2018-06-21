using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EventLibrary
{
    public interface ISMTPOperations
    {
        void SendEmail(MailMessage mail);
        string BodyOfEmail(Events parsedEvent);
        MailMessage CreateEmail(string body);
    }
}
