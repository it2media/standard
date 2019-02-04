using FluentAssertions;
using IT2media.Standard.Validation.Rules;
using Xunit;

namespace IT2media.Standard.xUnitTests.Validation.Rules
{
    public class IbanRuleTests
    {
        [Fact]
        public void Account_is_empty_then_validation_should_be_false()
        {
            string account = string.Empty;
            IbanRule rule = new IbanRule("");
            bool actual = rule.Validate(account);
            actual.Should().BeFalse("Account is not empty");
        }

        [Fact]
        public void ErrorMessage_should_be_equal_to_given_message()
        {
            string message = "IBAN ist wrong";
            IbanRule rule = new IbanRule(message);
            string actualMessage = rule.Message;
            actualMessage.Should().BeEquivalentTo(message, "Wrong error-message");
        }

        [Fact]
        public void Account_is_Valid_then_validation_should_be_true()
        {
            string account = "CZ65 0800 0000 1920 0014 5399";
            IbanRule rule = new IbanRule("");
            bool actual = rule.Validate(account);
            actual.Should().BeTrue("Account is not valid");
        }

        [Fact]
        public void Account_is_not_Valid_then_validation_should_be_false()
        {
            string account = "CZ65 0800 0000 1920 0014 5360";
            IbanRule rule = new IbanRule("");
            bool actual = rule.Validate(account);
            actual.Should().BeFalse("Account is valid");
        }

        [Fact]
        public void Account_is_not_Valid_when_regex_is_wrong_then_validation_should_be_false()
        {
            string account = "CZ65 0800 0000 ";
            IbanRule rule = new IbanRule("");
            bool actual = rule.Validate(account);
            actual.Should().BeFalse("Account is valid");
        }
    }
}
