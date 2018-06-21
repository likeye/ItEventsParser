using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace EventLibrary
{
    public class EventsParserFromHtml : IParse
    {
        private List<Events> eventsList = new List<Events>();
        private Events eventParsed = new Events();
        public List<Events> Parse(string city)
        {
            var nodes = getNodes(city);
            foreach (var item in nodes)
            {
                if (item.SelectSingleNode("a/div[1]") != null)
                {
                    eventParsed.Name = item.SelectSingleNode("a/div[@class=\"colTab title\"]").InnerText.Trim();
                    eventParsed.Date = item.SelectSingleNode("a/div[@class=\"colTab date \"]/span[@class=\"colDataDay\"]").InnerText.Trim();
                    eventParsed.Description = item.SelectSingleNode("a/div[@class=\"colTab topic phoneOff\"]").InnerText.Trim();
                    eventParsed.Link = "https://crossweb.pl/" + item.SelectSingleNode("a").Attributes["href"].Value.Trim();
                    eventsList.Add(eventParsed);
                }
            }
            return eventsList;
        }
        public void showParsedList(List<Events> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"Name: {eventsList[i].Name} Date: {eventsList[i].Date} Description: {eventsList[i].Description} \n Link: {eventsList[i].Link} \n");
            }
        }
        private IEnumerable<HtmlNode> getNodes(string city)
        {
            city = "wroclaw";
            var url = @"https://crossweb.pl/wydarzenia/" + city + "/";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var node = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div[@class=\"event-list \"]");
            var nodes = node.Elements("div");
            return nodes;
        }
    }
}
