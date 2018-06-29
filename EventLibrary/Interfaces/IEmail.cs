using System.Collections.Generic;
using System.Net.Mail;

namespace EventLibrary.Interfaces
{
    public interface IEmail
    {
        void Send(IEnumerable<DB.Event> eventList);     
    }
}
