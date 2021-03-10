using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Input;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MailSender.lib.Tests.Models
{
    [TestClass]
    public class SchedulerMailSenderTests
    {
        private SchedulerMailSender _scheduler;
        private DateTime _testDateTime;
        private Sender _sender;
        private List<Recipient> _recipients;
        private Message _message;

        [TestInitialize]
        public void Init()
        {
            var stub = Mock.Of<IMailSender>();
            _scheduler = new SchedulerMailSender(stub);
            _testDateTime = new DateTime(2021, 1, 1);
            _sender = new Sender
            {
                Address = "from@mail.ru",
            };
            _recipients = new List<Recipient>()
            {
                new Recipient {Address = "to1@mail.ru"},
                new Recipient {Address = "to2@mail.ru"},
            };
            _message = new Message
            {
                Subject = "Title",
                Text = "Message",
            };
        }

        //[TestMethod]
        //public void Scheduler_Ctor_Stub_NotNull()
        //{
        //    var stub = Mock.Of<IMailSender>();

        //    var actual = new SchedulerMailSender(stub);

        //    Assert.IsNotNull(actual);
        //}
        //[TestMethod]
        //public void Scheduler_Ctor_SchedulerMailSender()
        //{
        //    var stub = Mock.Of<IMailSender>();

        //    var actual = new SchedulerMailSender(stub);

        //    Assert.IsInstanceOfType(actual, typeof(SchedulerMailSender));
        //}
        //[TestMethod]
        //public void AddTask_Verify_Values()
        //{
        //    _scheduler.Start(_testDateTime, _sender, _recipients, _message);

        //    Assert.AreEqual(_testDateTime, _scheduler.DateTimeSend);
        //    Assert.AreEqual(_sender.Address, _scheduler.Sender.Address);
        //    CollectionAssert.AreEqual(_recipients, (System.Collections.ICollection)_scheduler.Recipients);
        //    Assert.AreEqual(_message.Subject, _scheduler.Message.Subject);
        //    Assert.AreEqual(_message.Text, _scheduler.Message.Text);
        //}
        //[TestMethod]
        //public void AddTask_Is_Called_Mock()
        //{
        //    _testDateTime = DateTime.Now;
        //    var mock = new Mock<IMailSender>();
        //    mock.Setup(d => d.Send(It.IsAny<string>(), It.IsAny<ICollection<string>>(), It.IsAny<string>(), It.IsAny<string>(), null));
        //    _scheduler = new SchedulerMailSender(mock.Object);

        //    _scheduler.Start(_testDateTime, _sender, _recipients, _message);

        //    Thread.Sleep(2100);
        //    mock.Verify(m => m.Send(It.IsAny<string>(), It.IsAny<ICollection<string>>(), It.IsAny<string>(), It.IsAny<string>(), null), Times.Once);
        //}
        //[TestMethod]
        //public void Stop_Is_Not_CalledMock()
        //{
        //    _testDateTime = DateTime.Now.AddSeconds(1);
        //    var mock = new Mock<IMailSender>();
        //    mock.Setup(d => d.Send(It.IsAny<string>(), It.IsAny<ICollection<string>>(), It.IsAny<string>(), It.IsAny<string>(), null));
        //    _scheduler = new SchedulerMailSender(mock.Object);

        //    _scheduler.Start(_testDateTime, _sender, _recipients, _message);
        //    _scheduler.Stop();
        //    Thread.Sleep(2200);

        //    mock.Verify(m => m.Send(It.IsAny<string>(), It.IsAny<ICollection<string>>(), It.IsAny<string>(), It.IsAny<string>(), null), Times.Never());
        //}
        //[TestMethod]
        //public void AddTask_Is_Called_Mock_In_5_Seconds()
        //{
        //    DateTime timeCall = new();
        //    _testDateTime = DateTime.Now.AddSeconds(5);
        //    var mock = new Mock<IMailSender>();
        //    mock.Setup(d => d.Send(It.IsAny<string>(), It.IsAny<ICollection<string>>(), It.IsAny<string>(), It.IsAny<string>(), null)).Callback((
        //        () =>
        //        {
        //            timeCall = DateTime.Now;
        //        }));
        //    _scheduler = new SchedulerMailSender(mock.Object);

        //    _scheduler.Start(_testDateTime, _sender, _recipients, _message);

        //    Thread.Sleep(6000);

        //    mock.Verify(m => m.Send(It.IsAny<string>(), It.IsAny<ICollection<string>>(), It.IsAny<string>(), It.IsAny<string>(), null), Times.Once);
        //    Assert.IsTrue(timeCall <= _testDateTime.AddSeconds(1) && timeCall >= _testDateTime.AddSeconds(-1));
        //}
        //[TestMethod]
        //public void EmailSended_Be_Invoked()
        //{
        //    bool isEventCalled = default;
        //    _scheduler.MissionCompleted += (_, _) =>
        //    {
        //        isEventCalled = true;
        //    };

        //    _scheduler.Start(_testDateTime, _sender, _recipients, _message);

        //    Thread.Sleep(1100);
        //    Assert.AreEqual(true, isEventCalled);
        //}
    }
}
