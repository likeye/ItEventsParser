using System.Collections.Generic;
using EventLibrary.Model;

namespace EventLibrary.Interfaces
{
    public interface IParse
    {
        IEnumerable<Event> Parse(ParseConfiguration config);
    }
}
