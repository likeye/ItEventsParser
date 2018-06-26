using System.Collections.Generic;
using EventLibrary.EventClasses;

namespace EventLibrary.Interfaces
{
    public interface IParse
    {
        IEnumerable<Event> Parse(string city, string type, string cost);
    }
}
