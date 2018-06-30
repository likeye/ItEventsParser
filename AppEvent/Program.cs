using EventClasses.EventParser;
using EventLibrary.DB;
using EventLibrary.Services;
using System;
using System.Configuration;
using System.Threading;

namespace AppEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailService emailService = new EmailService();
            EventsParser eventsParser = new EventsParser();
            DataBaseOperations dbOperations = new DataBaseOperations();
            var eventsList = eventsParser.Parse(ConfigurationManager.AppSettings["city"],
                ConfigurationManager.AppSettings["type"], ConfigurationManager.AppSettings["cost"]);
            eventsParser.ShowParsedList(eventsList);
            while (true)
            {
                Console.WriteLine("*** calling MyMethod *** ");
                dbOperations.Insert(eventsList);
                var eventsListFromDB = dbOperations.ReadAllToList();
                emailService.Send(eventsListFromDB);
                Console.WriteLine("\n All done");
                Thread.Sleep(60 *1000 * int.Parse(ConfigurationManager.AppSettings["Time"]));
            }
        }
    }
}
