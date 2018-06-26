using System;
using AutoFixture;
using AutoFixture.NUnit3;
using EventLibrary.EventClasses;
using EventLibrary.Interfaces;
using EventLibrary.SMTP;
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

        [Test]
        [AutoData]
        //TestCase'y
        public void Events_IsNotNull_ReturnMessage()
        {
            // Arrange
            Fixture fixture = new Fixture();

            var testEvent = fixture.Create<Event>();

            IEmail mock = new EmailServiceMock();

            var result = mock.PrepareBody(testEvent);

            Assert.IsTrue(result.Length > 0);
        }
    }
}
