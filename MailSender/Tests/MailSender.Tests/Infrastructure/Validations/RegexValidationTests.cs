using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Controls;
using MailSender.Infrastructure.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.Tests.Infrastructure.Validations
{
    [TestClass]
    public class RegexValidationTests
    {
        private RegexValidation _Validation;
        [TestInitialize]
        public void Init()
        {
            _Validation = new RegexValidation();
        }
        [TestMethod]
        public void Validate_Null_Value()
        {
            ValidationResult expected = ValidationResult.ValidResult;
            object value = null;

            _Validation.AllowNull = true;
            var actual = _Validation.Validate(value, CultureInfo.CurrentCulture);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Validate_Regex_Is_Null()
        {
            ValidationResult expected = ValidationResult.ValidResult;
            object value = "10";

            _Validation.Pattern = null;
            _Validation.AllowNull = false;
            var actual = _Validation.Validate(value, CultureInfo.CurrentCulture);

            Assert.AreEqual(expected, actual);
        }
        [DataTestMethod]
        [DataRow("test@mail.com")]
        [DataRow("tdsfasf-sdafs-asdfdsf@fdsfsd.sdfsdf.dsfdsf.asdfsa")]
        [DataRow("f@d.ru")]
        [DataRow("pupkin@pupok.net")]
        [DataRow("ini.ini@mail.ru")]
        public void Validate_On_Emails(string email)
        {
            ValidationResult expected = ValidationResult.ValidResult;
            object value = email;

            _Validation.Pattern = @"(\w+\.)*\w+@(\w+\.)+[A-Za-z]+";
            _Validation.AllowNull = false;
            var actual = _Validation.Validate(value, CultureInfo.CurrentCulture);

            Assert.AreEqual(expected, actual);
        }
    }
}
