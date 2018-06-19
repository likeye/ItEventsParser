using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EventLibrary
{
    public class Events
    {
        private readonly string name, date, description, link;
        public Events(string name, string date, string description, string link)
        {
            this.name = name;
            this.date = date;
            this.description = description;
            this.link = link;
        }

        public string GetName()
        {
            return this.name;
        }
        public string GetDate()
        {
            return this.date;
        }
        public string GetDesc()
        {
            return this.description;
        }
        public string GetLink()
        {
            return this.link;
        }

    }
}
