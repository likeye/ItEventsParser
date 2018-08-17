using System.Collections.Generic;
using EventLibrary.Interfaces;
using EventLibrary.Model;
using HtmlAgilityPack;

namespace EventLibrary.Services
{
    public class EventsParser : IParse
    {
        private readonly List<Event> _eventsList = new List<Event>();

        public IEnumerable<Event> Parse(ParseConfiguration config)
        {
            var cities = config.City.Split(' ');
            foreach (var city in cities)
            {
                var events = GetEvents(city);

                foreach (var eventNode in events)
                {
                    if (!CanParse(eventNode)) continue;

                    if (IsTypeAndCostNull(config.Type, config.Cost))
                    {
                        AddParsedEvent(eventNode, city);
                    }
                    else if (IsCostNull(config.Type, config.Cost))
                    {
                        if (NodeDesc(eventNode).Contains(config.Type))
                        {
                            AddParsedEvent(eventNode, city);
                        }
                    }
                    else if (IsTypeNull(config.Type))
                    {
                        if (NodeCost(eventNode).Contains(config.Cost))
                        {
                            AddParsedEvent(eventNode, city);
                        }
                    }
                    else
                    {
                        if (NodeCost(eventNode).Contains(config.Cost) && NodeDesc(eventNode).Contains(config.Type))
                        {
                            AddParsedEvent(eventNode, city);
                        }
                    }
                }
            }
            return _eventsList;
        }

        private void AddParsedEvent(HtmlNode @event, string city)
        {
            var eventParsed = CreateEvent(@event, city);
            _eventsList.Add(eventParsed);
        }

        private static bool CanParse(HtmlNode @event)
        {
            return @event.SelectSingleNode("a/div[1]") != null;
        }

        private Event CreateEvent(HtmlNode element, string item)
        {
            var eventParsed = new Event
            {
                Name = NodeName(element),
                Date = NodeDate(element),
                Description = NodeDesc(element),
                Link = NodeLink(element),
                City = item,
                HasSentEmail = null
            };
            return eventParsed;
        }

        private static bool IsTypeNull(string type)
        {
            return type == null;
        }

        private static bool IsCostNull(string type, string cost)
        {
            return type != null && cost == null;
        }

        private static bool IsTypeAndCostNull(string type, string cost)
        {
            return type == null && cost == null;
        }

        private IEnumerable<HtmlNode> GetEvents(string city)
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
