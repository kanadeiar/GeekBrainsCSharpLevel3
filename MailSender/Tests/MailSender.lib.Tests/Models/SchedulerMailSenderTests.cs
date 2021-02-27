using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Input;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MailSender.lib.Tests.Models
{
    [TestClass]
    public class SchedulerMailSenderTests
    {
        private SchedulerMailSender _scheduler;
        private DateTime _testDateTime;
        private string _testFrom;
        private List<string> _testTos;
        private string _testSubject;
        private string _testMessage;

        [TestInitialize]
        public void Init()
        {
            var stub = Mock.Of<IMailSender>();
            _scheduler = new SchedulerMailSender(stub);
            _testDateTime = new DateTime(2021, 1, 1);
            _testFrom = "from@mail.ru";
            _testTos = new List<string> { "to1@mail.ru", "to2@mail.ru" };
            _testSubject = "Title";
            _testMessage = "Message";
        }

        [TestMethod]
        public void Scheduler_Ctor_Stub_NotNull()
        {
            var stub = Mock.Of<IMailSender>();

            var actual = new SchedulerMailSender(stub);

            Assert.IsNotNull(actual);
        }
        [TestMethod]
        public void Scheduler_Ctor_SchedulerMailSender()
        {
            var stub = Mock.Of<IMailSender>();

            var actual = new SchedulerMailSender(stub);

            Assert.IsInstanceOfType(actual, typeof(SchedulerMailSender));
        }
        [TestMethod]
        public void AddTask_Verify_Values()
        {
            _scheduler.AddTask(_testDateTime, _testFrom, _testTos, _testSubject, _testMessage);

            Assert.AreEqual(_testDateTime, _scheduler.DateTimeSend);
            Assert.AreEqual(_testFrom, _scheduler.From);
            Assert.AreEqual(_testTos, _scheduler.Tos);
            Assert.AreEqual(_testSubject, _scheduler.Subject);
            Assert.AreEqual(_testMessage, _scheduler.Text);
        }
        [TestMethod]
        public void AddTask_Is_Called_Mock()
        {
            _testDateTime = DateTime.Now;
            var mock = new Mock<IMailSender>();
            mock.Setup(d => d.Send(It.IsAny<string>(),It.IsAny<ICollection<string>>(),It.IsAny<string>(),It.IsAny<string>()));
            _scheduler = new SchedulerMailSender(mock.Object);

            _scheduler.AddTask(_testDateTime, _testFrom, _testTos, _testSubject, _testMessage);

            Thread.Sleep(1100);
            mock.Verify(m => m.Send(It.IsAny<string>(),It.IsAny<ICollection<string>>(),It.IsAny<string>(),It.IsAny<string>()), Times.Once);
        }
        [TestMethod]
        public void Stop_Is_Not_CalledMock()
        {
            _testDateTime = DateTime.Now.AddSeconds(1);
            var mock = new Mock<IMailSender>();
            mock.Setup(d => d.Send(It.IsAny<string>(),It.IsAny<ICollection<string>>(),It.IsAny<string>(),It.IsAny<string>()));
            _scheduler = new SchedulerMailSender(mock.Object);

            _scheduler.AddTask(_testDateTime, _testFrom, _testTos, _testSubject, _testMessage);
            _scheduler.Stop();
            Thread.Sleep(2200);

            mock.Verify(m => m.Send(It.IsAny<string>(),It.IsAny<ICollection<string>>(),It.IsAny<string>(),It.IsAny<string>()), Times.Never());
        }
        [TestMethod]
        public void AddTask_Is_Called_Mock_In_5_Seconds()
        {
            DateTime timeCall = new();
            _testDateTime = DateTime.Now.AddSeconds(5);
            var mock = new Mock<IMailSender>();
            mock.Setup(d => d.Send(It.IsAny<string>(),It.IsAny<ICollection<string>>(),It.IsAny<string>(),It.IsAny<string>())).Callback((
                () =>
                {
                    timeCall = DateTime.Now;
                }));
            _scheduler = new SchedulerMailSender(mock.Object);

            _scheduler.AddTask(_testDateTime, _testFrom, _testTos, _testSubject, _testMessage);

            Thread.Sleep(6000);

            mock.Verify(m => m.Send(It.IsAny<string>(),It.IsAny<ICollection<string>>(),It.IsAny<string>(),It.IsAny<string>()), Times.Once);
            Assert.IsTrue(timeCall <= _testDateTime.AddSeconds(1) && timeCall >= _testDateTime.AddSeconds(-1));
        }
        [TestMethod]
        public void EmailSended_Be_Invoked()
        {
            bool isEventCalled = default;
            _scheduler.EmailSended += (_, _) =>
            {
                isEventCalled = true;
            };

            _scheduler.AddTask(_testDateTime, _testFrom, _testTos, _testSubject, _testMessage);

            Thread.Sleep(1100);
            Assert.AreEqual(true, isEventCalled);
        }
    }
}
