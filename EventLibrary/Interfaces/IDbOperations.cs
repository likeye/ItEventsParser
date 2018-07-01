using System.Collections.Generic;
using EventLibrary.EventClasses;

namespace EventLibrary.Interfaces
{
    public interface IDbOperations
    {
        void Insert(IEnumerable<Event> eventsList);
        void UpdateEvent(int dbEventId, DB.Events newEvent);
        void DeleteSingle(int id);
        void DeleteSingle(string name);
        string ReadSingle(int id);
        string ReadSingle(string name);
        IEnumerable<DB.Events> ReadAllToList();
    }
}
