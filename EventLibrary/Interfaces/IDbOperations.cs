using System.Collections.Generic;
using EventLibrary.Model;

namespace EventLibrary.Interfaces
{
    public interface IDbOperations
    {
        void Insert(IEnumerable<Event> eventsList);
        void UpdateEvent(int dbEventId, DB.EventDb newEvent);
        void DeleteSingle(int id);
        void DeleteSingle(string name);
        string ReadSingle(int id);
        string ReadSingle(string name);
        IEnumerable<DB.EventDb> ReadAllToList();
    }
}
