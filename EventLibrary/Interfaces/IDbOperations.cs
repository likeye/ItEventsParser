using System.Collections.Generic;
using EventLibrary.EventClasses;

namespace EventLibrary.Interfaces
{
    public interface IDbOperations
    {
        void Insert(IEnumerable<Event> eventsList);
    }
}
