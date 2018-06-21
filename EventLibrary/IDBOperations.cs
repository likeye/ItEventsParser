using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLibrary
{
    public interface IDBOperations
    {
        void InsertData(List<Events> eventsList);
    }
}
