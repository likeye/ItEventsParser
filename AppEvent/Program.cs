using EventLibrary.DB;
using EventLibrary.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
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
                City = ParseCity(ConfigurationManager.AppSettings["city"]),
                Type = ConfigurationManager.AppSettings["type"],
                Cost = ParseCost(ConfigurationManager.AppSettings["cost"])
            });

            var events = eventList as IList<Event> ?? eventList.ToList();

            ShowParsedList(events);
            
            Console.WriteLine();
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

        static string ParseCity(string city)
        {
            string asciiEq = Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(city));
            return asciiEq;
        }

        static string ParseCost(string cost)
        {
            if (cost == "bezplatny" || cost == "Bezplatny" || cost == "bezpłatny")
            {
                return "Bezpłatny";
            }
            else if (cost == "płatny" || cost == "platny" || cost == "Platny")
            {
                return "Płatny";
            }
            else
            {
                return cost;
            }
        }
    }
}
