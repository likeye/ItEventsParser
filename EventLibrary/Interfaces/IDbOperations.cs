using System.Collections.Generic;
using EventLibrary.EventClasses;

namespace EventLibrary.Interfaces
{
    public interface IDbOperations
    {
        void Insert(IEnumerable<Event> eventsList);
        void UpdateEvent(int? dbEventId, string dbEventName, string newName, string newDesc, string newLink, string newCity,
            string newDate);
        void DeleteSingle(string name, int? id);
        void ReadSingle(int? id, string name);
        void ReadAll();
    }
}
