using EventLibrary.DB;
using EventLibrary.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using EventLibrary.Model;
using Event = EventLibrary.EventClasses.Event;

namespace AppEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailService emailService = new EmailService();
            EventsParser eventsParser = new EventsParser();
            DataBaseOperations dbOperations = new DataBaseOperations();

            var eventList = eventsParser.Parse(new ParseConfiguration()
            {
                City = ConfigurationManager.AppSettings["city"],
                Type = ConfigurationManager.AppSettings["type"],
                Cost = ConfigurationManager.AppSettings["cost"]
            });

            var events = eventList as IList<Event> ?? eventList.ToList();

            ShowParsedList(events);

            while (true)
            {
                Console.WriteLine("*** calling MyMethod *** ");
                dbOperations.Insert(events);

                try
                {
                    var eventsListFromDb = dbOperations.ReadAllToList();
                    emailService.Send(eventsListFromDb);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("\n All done");
                Thread.Sleep(60 * 1000 * int.Parse(ConfigurationManager.AppSettings["time"]));
            }
        }

        static void ShowParsedList(IEnumerable<Event> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine($"Name: {item.Name} Date: {item.Date} Description: {item.Description} \n Link: {item.Link} City: {item.City} \n");
            }
        }
    }
}
