using System.Security.Cryptography;
using MailSender.lib.Interfaces;
using MailSender.lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.lib.Tests.Services
{
    [TestClass]
    public class Rfc2898EncryptorTests
    {
        private IEncryptService _encrypt = new Rfc2898Encryptor();
        [TestMethod]
        public void EncryptTest_Hello_And_Decrypt_with_Password()
        {
            const string expected = "Hello World!";
            const string key = "password12345";

            var encrypt = _encrypt.Encrypt(expected, key);
            var decrypt = _encrypt.Decrypt(encrypt, key);

            Assert.AreEqual(decrypt, expected);
        }
        [TestMethod]
        [ExpectedException(typeof(CryptographicException))]
        public void EncryptTest_Wrong_Decryption_thrown_CryptographicException()
        {
            const string str = "Hello World!";
            const string password = "password12345";

            var encryptedStr = _encrypt.Encrypt(str, password);
            var wrong = _encrypt.Decrypt(encryptedStr, "123");
        }
    }
}
