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

        [TestMethod]
        public void Auth_NoPassword_ReturnedFalse()
        {
            // Arrange
            AuthorizeHelper authorizeHelper = new AuthorizeHelper();

            string testPhone = "89184293165";

            bool expected = false;

            // Actual
            var result = authorizeHelper.Auth(testPhone, null).Result;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Auth_NoPhone_ReturnedFalse()
        {
            // Arrange
            AuthorizeHelper authorizeHelper = new AuthorizeHelper();

            string testPassword = "123";

            bool expected = false;

            // Actual
            var result = authorizeHelper.Auth(null, testPassword).Result;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Auth_NoDataAll_ReturnedFalse()
        {
            // Arrange
            AuthorizeHelper authorizeHelper = new AuthorizeHelper();

            bool expected = false;

            // Actual
            var result = authorizeHelper.Auth(null, null).Result;

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
