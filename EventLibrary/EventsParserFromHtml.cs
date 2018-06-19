using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace EventLibrary
{
    public class EventsParserFromHtml
    {
        public void Parser(string city)
        {
            var url = @"https://crossweb.pl/wydarzenia/" + city + "/";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var nodes = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div[1]/div[2]");
            var innerTexts = nodes.Select(node => node.InnerText);
        }
    }
}
