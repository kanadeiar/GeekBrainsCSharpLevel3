using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MailSender.Infrastructure.Validations
{
    /// <summary> Валидация с помощью регулярного выражения </summary>
    public class RegexValidation : ValidationRule
    {
        private Regex _Regex;
        public string Pattern
        {
            get => _Regex.ToString();
            set => _Regex = string.IsNullOrEmpty(value) ? null : new Regex(value);
        }
        private bool _AllowNull;
        public bool AllowNull
        {
            get => _AllowNull;
            set => _AllowNull = value;
        }
        private string _ErrorMessage;
        public string ErrorMessage
        {
            get => _ErrorMessage;
            set => _ErrorMessage = value;
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is null)
                if (AllowNull)
                    return ValidationResult.ValidResult;
                else
                    new ValidationResult(false, ErrorMessage ?? "Отустствует значение");
            if (_Regex is null)
                return ValidationResult.ValidResult;
            if (!(value is string str))
                str = value.ToString();
            if (_Regex.IsMatch(str))
                return ValidationResult.ValidResult;
            else
                return new ValidationResult(false, ErrorMessage ?? $"Значение не соответствует требованию выражения {Pattern}");
        }
    }
}
