using System.Collections.Generic;

namespace EventLibrary.Interfaces
{
    public interface IEmail
    {
        void Send(IEnumerable<DB.Events> eventList);
    }
}
