using AutoFixture;
using AutoFixture.NUnit3;
using EventLibrary.EventClasses;
using EventLibrary.Interfaces;
using EventLibrary.Services;
using NUnit.Framework;

namespace EventLibrary_Tests
{
    public class Events_Tests
    {
        [Test]
        public void Events_IsNUll_ReturnEmptyBodyMessage()
        {
            IEmail mock = new EmailServiceMock();

            var result = mock.PrepareBody(null);

            //Assert.Throws<Exception>
            Assert.IsEmpty(result);
        }

    }
}
