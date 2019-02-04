using System.Text.RegularExpressions;

namespace IT2media.Standard.Validation.Rules
{
    public class EmailRule : IValidationRule<string>
    {
        private const string Pattern =
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public EmailRule(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(value);
        }
    }
}
