using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using EventLibrary;

namespace EventLibrary_Tests
{
    public class Events_Tests
    {
        public Events events1 = new Events("abba", "11.03", "abba", "www.abba.pl");

        [Test]
        public void events_ReturnStringName()
        {
            var text = events1.GetName();
            var expectedResult = "abba";
            Assert.AreEqual(expectedResult,text);
        }
        [Test]
        public void events_ReturnStringDate()
        {
            var text = events1.GetDate();
            var expectedResult = "11.03";
            Assert.AreEqual(expectedResult, text);
        }
        [Test]
        public void events_ReturnStringDesc()
        {
            var text = events1.GetDesc();
            var expectedResult = "abba";
            Assert.AreEqual(expectedResult, text);
        }
        [Test]
        public void events_ReturnStringLink()
        {
            var text = events1.GetLink();
            var expectedResult = "www.abba.pl";
            Assert.AreEqual(expectedResult, text);
        }
    }
}
