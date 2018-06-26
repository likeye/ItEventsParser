using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using EventLibrary.Interfaces;
using EventLibrary.EventClasses;

namespace EventClasses.EventParser
{
    public class EventsParser : IParse
    {
        private readonly List<Event> _eventsList = new List<Event>();
        public IEnumerable<Event> Parse(string city, string type, string cost)
        {
            var nodes = GetNodes(city);
            foreach (var item in nodes)
            {
                if (item.SelectSingleNode("a/div[1]") != null)
                {
                    if (type == null && cost == null)
                    {
                        var eventParsed = new Event
                        {
                            Name = NodeName(item),
                            Date = NodeDate(item),
                            Description = NodeDesc(item),
                            Link = NodeLink(item)
                        };
                        _eventsList.Add(eventParsed);
                    }
                    else if (type != null && cost == null)
                    {
                        if (NodeDesc(item).Contains(type))
                        {
                            Event eventParsed = new Event
                            {
                                Name = NodeName(item),
                                Date = NodeDate(item),
                                Description = NodeDesc(item),
                                Link = NodeLink(item)
                            };
                            _eventsList.Add(eventParsed);
                        }
                    }
                    else if (type == null)
                    {
                        if (NodeCost(item).Contains(cost))
                        {
                            Event eventParsed = new Event
                            {
                                Name = NodeName(item),
                                Date = NodeDate(item),
                                Description = NodeDesc(item),
                                Link = NodeLink(item)
                            };
                            _eventsList.Add(eventParsed);
                        }
                    }
                    else
                    {
                        if (NodeCost(item).Contains(cost) && NodeDesc(item).Contains(type))
                        {
                            Event eventParsed = new Event
                            {
                                Name = NodeName(item),
                                Date = NodeDate(item),
                                Description = NodeDesc(item),
                                Link = NodeLink(item)
                            };
                            _eventsList.Add(eventParsed);
                        }
                    }
                }
            }
            return _eventsList;
        }
        public void ShowParsedList(IEnumerable<Event> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine($"Name: {item.Name} Date: {item.Date} Description: {item.Description} \n Link: {item.Link} \n");
            }
        }
        private IEnumerable<HtmlNode> GetNodes(string city)
        {
            var url = @"https://crossweb.pl/wydarzenia/" + city + "/";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var node1 = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div[@class=\"event-list \"]");
            var node2 = doc.DocumentNode.SelectSingleNode("//*[@id=\"container\"]/div[@class=\"event-list\"]");
            if (node1 == null)
            {
                var nodes = node2.Elements("div");
                return nodes;
            }
            else
            {
                var nodes = node1.Elements("div");
                return nodes;
            }
            
        }
        private string NodeName(HtmlNode item)
        {
            return item.SelectSingleNode("a/div[@class=\"colTab title\"]").InnerText.Trim();
        }
        private string NodeDate(HtmlNode item)
        {
             return item.SelectSingleNode("a/div/span[@class=\"colDataDay\"]").InnerText.Trim();
        }
        private string NodeDesc(HtmlNode item)
        {
            return item.SelectSingleNode("a/div[@class=\"colTab topic phoneOff\"]").InnerText.Trim();
        }
        private string NodeCost(HtmlNode item)
        {
            return item.SelectSingleNode("a/div[@class=\"colTab cost phoneOff tabletOff\"]").InnerText.Trim();
        }
        private string NodeLink(HtmlNode item)
        {
            return "https://crossweb.pl/" + item.SelectSingleNode("a").Attributes["href"].Value.Trim();
        }
    }
}
