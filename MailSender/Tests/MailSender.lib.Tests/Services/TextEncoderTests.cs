using MailSender.lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.lib.Tests.Services
{
    [TestClass]
    public class TextEncoderTests
    {
        [TestMethod]
        public void Encode_ABC_to_BCD_key_1()
        {
            const string str = "ABC";
            const int key = 1;
            const string expected = "BCD";

            var actual = TextEncoder.Encode(str, key);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Decode_BCD_to_ABC_key_1()
        {
            const string str = "BCD";
            const int key = 1;
            const string expected = "ABC";

            var actual = TextEncoder.Decode(str, key);

            Assert.AreEqual(expected, actual);
        }
    }
}
