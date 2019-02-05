using System;
using System.Diagnostics.CodeAnalysis;
using IT2media.Standard.Validation;
using IT2media.Standard.Validation.Rules;

namespace IT2media.Standard.Samples.Validation
{
    [ExcludeFromCodeCoverage]
    public class ValidationSample
    {
        private readonly ValidatableObject<string> _mail;
        private readonly ValidatableObject<string> _iban;

        private const string MailMessage = "Wrong Mail";
        private const string MandatoryMessage = "Field is empty";
        private const string IbanMessage ="Wrong Iban";

        public ValidationSample()
        {
            _mail = new ValidatableObject<string>(new EmailRule(MailMessage),
                new MandatoryRule<string>(MandatoryMessage))
                    { Value = "hdsfhf@d"};
            _iban = new ValidatableObject<string>(new IbanRule(IbanMessage))
                    { Value = "CZ65 0800 0000 1920 0014 5399"};
        }

        public void Validate()
        {
            _mail.Validate();
            _iban.Validate();

            EmailIsNotValid();
            IbanIsValid();
        }

        private void IbanIsValid()
        {
            if (_iban.IsValid)
            {
                Console.WriteLine($"Iban is valid");
            }
        }

        private void EmailIsNotValid()
        {
            if (!_mail.IsValid)
            {
                Console.WriteLine(_mail.Messages);
            }
        }
    }
}