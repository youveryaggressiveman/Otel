using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otel.Core.Helper;
using System;

namespace HotelUnitTest
{
    [TestClass]
    public class AuthUnitTest
    {
        [TestMethod]
        public void Auth_TestUser_ReturnedTrue()
        {
            // Arrange
            AuthorizeHelper authorizeHelper = new AuthorizeHelper();

            string testPhone = "89184293165";
            string testPassword = "123";

            bool expected = true;

            // Actual
            var result = authorizeHelper.Auth(testPhone, testPassword).Result;

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
