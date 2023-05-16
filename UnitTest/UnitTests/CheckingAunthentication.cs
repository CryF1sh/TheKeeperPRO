using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheKeeperPro.Class;
using System;

namespace CheckingAunthentication
{
    [TestClass]
    public class CheckingAunthentication
    {
        [TestMethod]
        public void CorrectPassword()
        {
            UserAuthenticationTest user = new UserAuthenticationTest();
            Assert.AreEqual(true, UserAuthenticationTest.CodeAuthentication(true), "Проверка на правельный введённый код");            
        }
        [TestMethod]
        public void IncorrectPassword()
        {
            UserAuthenticationTest user = new UserAuthenticationTest();
            Assert.AreEqual(false, UserAuthenticationTest.CodeAuthentication(false), "Проверка на неправельный введённый код");
        }
    }
}
