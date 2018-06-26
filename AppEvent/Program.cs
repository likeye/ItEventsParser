using EventLibrary.DB;
using EventLibrary.EventClasses;
using EventLibrary.Services;
using System;
namespace AppEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            EventsParser eventsParser = new EventsParser();
            DataBaseOperations dbOperations = new DataBaseOperations();
            var eventsList = eventsParser.Parse("warszawa",null, null);
            eventsParser.ShowParsedList(eventsList);
            //dbOperations.Insert(eventsList);   
            Console.ReadKey();
        }
    }
}
