using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLibrary
{
    public interface IParse
    {
        List<Events> Parse(string city);
    }
}
