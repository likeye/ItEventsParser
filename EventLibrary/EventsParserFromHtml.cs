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
            var nodeNames = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div/div/a/div[@class=\"colTab title\"]"); //to i 
            var nodeDatas = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div/div/a/div/span[@class=\"colDataDay\"]");
            var nodeDescriptions = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div/div/a/div[@class=\"colTab topic phoneOff\"]"); // to teoretycznie jest so samo (?)
            var nodeLinks = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div/div/a");
            //foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div/div/a/div/span") )
            //{
            //    var text = node.Attributes[]
            //}

            var innerTextNames = nodeNames.Select(node => node.InnerText);
            var innerTextDatas = nodeDatas.Select(node => node.InnerText);
            var innerTextDescriptions = nodeDescriptions.Select(node => node.InnerText);
            var innerTextLinks = nodeLinks.Select(node => node.InnerText);
            
        }
    }
}
