using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using EventLibrary;

namespace EventLibrary_Tests
{
    public class Events_Tests
    {
        public Events events1 = new Events();

        [TestCase("abba","11.03","abba","www.abba.pl")]
        public void events_ReturnStringName(string name, string date, string desc, string link)
        {
            events1.Date = date;
            events1.Name = name;
            events1.Description = desc;
            events1.Link = link;

            //??
        }
    }
}
