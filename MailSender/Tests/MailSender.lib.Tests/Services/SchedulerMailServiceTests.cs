using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MailSender.lib.Tests.Services
{
    [TestClass]
    public class SchedulerMailServiceTests
    {
        private SchedulerMailService _schedulerMailService;
        [TestMethod]
        public void GetSender_Give_NotNull_Stub_Test()
        {
            var stub = Mock.Of<IMailSender>();
            _schedulerMailService = new SchedulerMailService();
            var expected = new SchedulerMailSender(stub);

            var actual = _schedulerMailService.GetScheduler(stub);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
        [TestMethod]
        public void GetSender_Give_SchedulerMailSender()
        {
            var stub = Mock.Of<IMailSender>();

            var actual = new SchedulerMailService().GetScheduler(stub);

            Assert.IsInstanceOfType(actual, typeof(SchedulerMailSender));
        }
    }
}
