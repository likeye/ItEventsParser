using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace EventLibrary_Tests
{
    class EventsPatser_Tests
    {
        [Test]
        public void Parser_Test(string city)
        {
            city = "wroclaw";
            var url = @"https://crossweb.pl/wydarzenia/" + city + "/";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var nodeNames = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div/div/a/div[@class=\"colTab title\"]"); 
            var nodeDatas = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div/div/a/div/span[@class=\"colDataDay\"]");
            var nodeDescriptions = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div/div/a/div[@class=\"colTab topic phoneOff\"]");
            var nodeLinks = doc.DocumentNode.SelectNodes("//*[@id=\"container\"]/div/div/a");
            string result = "";
            foreach (var item in nodeNames)
            {
                result += item.InnerText;
            }
            Assert.IsNotNull(result);


        }
    }
}
