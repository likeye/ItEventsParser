using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using EventLibrary.Interfaces;
using EventLibrary.DB;
using EventLibrary.SMTP;

namespace EventLibrary.EventClasses
{
    public class EventsParser : IParse
    {
        private readonly List<Event> _eventsList = new List<Event>();
        public List<Event> Parse(string city, string type, string cost)
        {
            var nodes = GetNodes(city);
            foreach (var item in nodes)
            {
                if (item.SelectSingleNode("a/div[1]") != null)
                {
                    if (type == null && cost == null)
                    {
                        Event eventParsed = new Event();
                        eventParsed.Name = item.SelectSingleNode("a/div[@class=\"colTab title\"]").InnerText.Trim();
                        eventParsed.Date = item.SelectSingleNode("a/div[@class=\"colTab date \"]/span[@class=\"colDataDay\"]").InnerText.Trim();
                        eventParsed.Description = item.SelectSingleNode("a/div[@class=\"colTab topic phoneOff\"]").InnerText.Trim();
                        eventParsed.Link = "https://crossweb.pl/" + item.SelectSingleNode("a").Attributes["href"].Value.Trim();
                        _eventsList.Add(eventParsed);
                    }
                    else if (type != null && cost == null)
                    {
                        if (item.SelectSingleNode("a/div[@class=\"colTab topic phoneOff\"]").InnerText.Trim().Contains(type))
                        {
                            Event eventParsed = new Event();
                            eventParsed.Name = item.SelectSingleNode("a/div[@class=\"colTab title\"]").InnerText.Trim();
                            eventParsed.Date = item.SelectSingleNode("a/div[@class=\"colTab date \"]/span[@class=\"colDataDay\"]").InnerText.Trim();
                            eventParsed.Description = item.SelectSingleNode("a/div[@class=\"colTab topic phoneOff\"]").InnerText.Trim();
                            eventParsed.Link = "https://crossweb.pl/" + item.SelectSingleNode("a").Attributes["href"].Value.Trim();
                            _eventsList.Add(eventParsed);
                        }
                    }
                    else if (type == null && cost != null)
                    {
                        if (item.SelectSingleNode("a/div[@class=\"colTab cost phoneOff tabletOff\"]").InnerText.Trim().Contains(cost))
                        {
                            Event eventParsed = new Event();
                            eventParsed.Name = item.SelectSingleNode("a/div[@class=\"colTab title\"]").InnerText.Trim();
                            eventParsed.Date = item.SelectSingleNode("a/div[@class=\"colTab date \"]/span[@class=\"colDataDay\"]").InnerText.Trim();
                            eventParsed.Description = item.SelectSingleNode("a/div[@class=\"colTab topic phoneOff\"]").InnerText.Trim();
                            eventParsed.Link = "https://crossweb.pl/" + item.SelectSingleNode("a").Attributes["href"].Value.Trim();
                            _eventsList.Add(eventParsed);
                        }
                    }
                    else
                    {
                        if (item.SelectSingleNode("a/div[@class=\"colTab cost phoneOff tabletOff\"]").InnerText.Trim().Contains(cost) && item.SelectSingleNode("a/div[@class=\"colTab topic phoneOff\"]").InnerText.Trim().Contains(type))
                        {
                            Event eventParsed = new Event();
                            eventParsed.Name = item.SelectSingleNode("a/div[@class=\"colTab title\"]").InnerText.Trim();
                            eventParsed.Date = item.SelectSingleNode("a/div[@class=\"colTab date \"]/span[@class=\"colDataDay\"]").InnerText.Trim();
                            eventParsed.Description = item.SelectSingleNode("a/div[@class=\"colTab topic phoneOff\"]").InnerText.Trim();
                            eventParsed.Link = "https://crossweb.pl/" + item.SelectSingleNode("a").Attributes["href"].Value.Trim();
                            _eventsList.Add(eventParsed);
                        }
                    }
                }
            }
            return _eventsList;
        }
        public void ShowParsedList(List<Event> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"Name: {_eventsList[i].Name} Date: {_eventsList[i].Date} Description: {_eventsList[i].Description} \n Link: {_eventsList[i].Link} \n");
            }
        }
        private IEnumerable<HtmlNode> GetNodes(string city)
        {
            var url = @"https://crossweb.pl/wydarzenia/" + city + "/";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var node = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div[@class=\"event-list \"]");
            var nodes = node.Elements("div");
            return nodes;
        }
    }
}
