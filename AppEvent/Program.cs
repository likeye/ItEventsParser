using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventLibrary;
using EventLibrary.Interfaces;
using EventLibrary.DB;
using EventLibrary.SMTP;
using EventLibrary.EventClasses;
namespace AppEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            EventsParser eventsParser = new EventsParser();
            DataBaseOperations dbOperations = new DataBaseOperations();
            var eventsList = eventsParser.Parse("wroclaw");
            eventsParser.ShowParsedList(eventsList);
            dbOperations.Insert(eventsList);   
            Console.ReadKey();
        }
    }
}
