using FluentAssertions;
using IT2media.Standard.Validation.Rules;
using Xunit;

namespace IT2media.Standard.xUnitTests.Validation.Rules
{
    public class EmailRuleTests
    {
        [Fact]
        public void Empty_Email_should_be_valid()
        {
            string email = string.Empty;
            EmailRule rule = new EmailRule("");
            bool actual = rule.Validate(email);
            actual.Should().BeTrue("EMail is not empty");
        }

        [Fact]
        public void Wrong_Email_should_notBe_valid()
        {
            string email = "1234@ddd";
            EmailRule rule = new EmailRule("");
            bool actual = rule.Validate(email);
            actual.Should().BeFalse("EMail Pattern <> match should be wrong");
        }

        [Fact]
        public void Email_should_be_valid()
        {
            string email = "name@test.de";
            EmailRule rule = new EmailRule("");
            bool actual = rule.Validate(email);
            actual.Should().BeTrue("EMail Pattern <> match should be wrong");
        }

        [Fact]
        public void ErrorMessage_should_be_equal_to_given_message()
        {
            string message = "Wrong Email";
            EmailRule rule = new EmailRule(message);
            string actualMessage = rule.Message;
            actualMessage.Should().BeEquivalentTo(message, "Wrong error-message");
        }
    }
}