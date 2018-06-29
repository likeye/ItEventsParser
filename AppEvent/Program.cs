using EventClasses.EventParser;
using EventLibrary.DB;
using System;
using System.Configuration;
using EventLibrary.Services;

namespace AppEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailService emailService = new EmailService();
            EventsParser eventsParser = new EventsParser();
            DataBaseOperations dbOperations = new DataBaseOperations();
            var eventsList = eventsParser.Parse(ConfigurationManager.AppSettings["city"],ConfigurationManager.AppSettings["type"],ConfigurationManager.AppSettings["cost"]);
            eventsParser.ShowParsedList(eventsList);
            Console.WriteLine("");
            Console.WriteLine(dbOperations.ReadSingle(22));
            
            
            //var eventsListFromDB = dbOperations.ReadAllToList();
            //emailService.Send(eventsListFromDB);
            
            //dbOperations.DeleteSingle(2);

            //EventLibrary.DB.Event eventsAb = new Event()
            //{
            //    Name = "Bak",
            //    Date = "22.22.22"
            //};
            //dbOperations.UpdateEvent(2,eventsAb);
            
            //dbOperations.Insert(eventsList);
           
            //emailService.Send(eventsListFromDB);
            Console.ReadKey();
        }
    }
}
