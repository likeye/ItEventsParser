using EventClasses.EventParser;
using EventLibrary.DB;
using System;
using System.Configuration;

namespace AppEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            EventsParser eventsParser = new EventsParser();
            DataBaseOperations dbOperations = new DataBaseOperations();
            var eventsList = eventsParser.Parse(ConfigurationManager.AppSettings["city"],ConfigurationManager.AppSettings["type"],ConfigurationManager.AppSettings["cost"]);
            eventsParser.ShowParsedList(eventsList);
            dbOperations.Insert(eventsList);   
            Console.ReadKey();
        }
    }
}
