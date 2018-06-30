using EventClasses.EventParser;
using EventLibrary.DB;
using System;
using System.Configuration;
using System.Threading;
using EventLibrary.Services;
using NUnit.Framework.Internal;

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
            //Console.WriteLine("");
            //Console.WriteLine(dbOperations.ReadSingle(22));

            //dbOperations.DeleteSingle(2);

            //EventLibrary.DB.Event eventsAb = new Event()
            //{
            //    Name = "Bak",
            //    Date = "22.22.22"
            //};
            //dbOperations.UpdateEvent(2,eventsAb);
        }
    }
}
