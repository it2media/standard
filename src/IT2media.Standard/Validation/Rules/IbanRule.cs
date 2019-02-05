using System.Text.RegularExpressions;

namespace IT2media.Standard.Validation.Rules
{
    public class IbanRule : IValidationRule<string>
    {
        private const string Pattern = @"^[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}$";
        private const int LetterCheckDigit = 55;
        private const int NumberCheckDigit = 48;

        public IbanRule(string message)
        {
            Message = message;
        }

        public string Message { get; }
        public bool Validate(string value)
        {
            return ValidateAccount(value);
        }

        private bool ValidateAccount(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                return false;
            }

            account = account.Replace(" ", string.Empty);

            if (Regex.IsMatch(account, Pattern))
            {
                account = account.Substring(4) + account.Substring(0, 4);
                int checksum = 0;

                foreach (char character in account)
                {
                    checksum = char.IsLetter(character) ? GetCheckSumLetter(checksum, character)
                        : GetCheckSumNumber(checksum, character);
                }

                return checksum == 1;
            }

            return false;
        }

        private int GetCheckSumLetter(int checksum, char character)
        {
            return ((100 * checksum) + (character - LetterCheckDigit)) % 97;
        }

        private int GetCheckSumNumber(int checksum, char character)
        {
            return ((10 * checksum) + (character - NumberCheckDigit)) % 97;
        }
    }
}