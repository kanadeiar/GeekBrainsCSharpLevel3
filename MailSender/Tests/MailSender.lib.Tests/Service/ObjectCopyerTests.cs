using MailSender.lib.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.lib.Tests.Service
{
    [TestClass]
    public class ObjectCopyerTests
    {
        [TestMethod]
        public void CopyTo_Not_Null()
        {
            var value = new TestClass
            {
                TestValue = 10,
            };

            var actual = new TestClass();
            value.CopyTo(actual);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void CopyTo_TestClass_Value()
        {
            var value = new TestClass
            {
                TestValue = 10,
            };
            var expected = value;

            var actual = new TestClass();
            value.CopyTo(actual);

            Assert.AreEqual(expected.TestValue, actual.TestValue);
        }
        public class TestClass
        {
            public int TestValue { get; set; }
        }
    }
}
