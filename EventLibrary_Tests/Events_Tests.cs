using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using EventLibrary;
using EventLibrary.EventClasses;
using EventLibrary.DB;
using EventLibrary.SMTP;

namespace EventLibrary_Tests
{
    public class Events_Tests
    {
        public Event events1 = new Event();

        [TestCase("abba","11.03","abba","www.abba.pl")]
        public void Events_ReturnStringName(string name, string date, string desc, string link)
        {
            events1.Date = date;
            events1.Name = name;
            events1.Description = desc;
            events1.Link = link;

            //??
        }
    }
}
