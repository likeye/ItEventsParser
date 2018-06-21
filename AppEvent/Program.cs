using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventLibrary;
namespace AppEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            EventsParserFromHtml eventsParser = new EventsParserFromHtml();
            DataBaseOperations dbOperations = new DataBaseOperations();
            var eventsList = eventsParser.Parse("wroclaw");
            eventsParser.ShowParsedList(eventsList);
            dbOperations.InsertData(eventsList);
            
           
            Console.ReadKey();
        }
    }
}
