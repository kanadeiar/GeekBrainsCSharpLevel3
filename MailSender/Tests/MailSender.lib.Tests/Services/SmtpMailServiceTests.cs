﻿using MailSender.lib.Interfaces;
using MailSender.lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MailSender.lib.Tests.Services
{
    [TestClass]
    public class SmtpMailServiceTests
    {
        private SmtpMailService _MailService;
        [TestMethod]
        public void GetSender_Give_NotNull_Stub_Test()
        {
            var stub = Mock.Of<IStatistic>();
            _MailService = new SmtpMailService(stub);
            var address = "test@test.ru";
            var port = 25;
            var useSsl = true;
            var login = "login";
            var password = "password".Encrypt();
            var expected = new SmtpSender(address, port, useSsl, login, password, stub);

            var actual = _MailService.GetSender(address, port, useSsl, login, password);
            
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
        [TestMethod]
        public void GetSender_TypeIsMailSender()
        {
            var stub = Mock.Of<IStatistic>();
            _MailService = new SmtpMailService(stub);
            var address = "test@test.ru";
            var port = 25;
            var useSsl = true;
            var login = "login";
            var password = "password".Encrypt();

            var actual = _MailService.GetSender(address, port, useSsl, login, password);

            Assert.IsInstanceOfType(actual, typeof(IMailSender));
        }
    }
}
