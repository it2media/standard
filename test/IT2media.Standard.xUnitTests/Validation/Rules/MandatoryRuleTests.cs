using FluentAssertions;
using IT2media.Standard.Validation.Rules;
using Xunit;

namespace IT2media.Standard.xUnitTests.Validation.Rules
{
    public class MandatoryRuleTests
    {
        [Fact]
        public void Value_is_null_then_Valid_should_be_false()
        {
            MandatoryRule<string> rule = new MandatoryRule<string>(null);
            rule.Validate(null).Should().BeFalse("Value ist not null");
        }

        [Fact]
        public void Value_is_not_null_then_Valid_should_be_true()
        {
            MandatoryRule<string> rule = new MandatoryRule<string>(string.Empty);
            rule.Validate("Empty").Should().BeTrue("Value ist null");
        }

        [Fact]
        public void Value_is_empty_then_Valid_should_be_false()
        {
            MandatoryRule<string> rule = new MandatoryRule<string>(string.Empty);
            rule.Validate(string.Empty).Should().BeFalse("Value ist not empty");
        }

        [Fact]
        public void Value_is_not_empty_then_Valid_should_be_true()
        {
            MandatoryRule<string> rule = new MandatoryRule<string>(string.Empty);
            rule.Validate("todo").Should().BeTrue("Value ist empty");
        }

        [Fact]
        public void Value_is_a_nullable_int_then_Valid_should_be_false()
        {
            MandatoryRule<int?> rule = new MandatoryRule<int?>(string.Empty);
            rule.Validate(null).Should().BeFalse("Numeric value ist not null");
        }

        [Fact]
        public void Value_is_greater_than_zero_then_Valid_should_be_true()
        {
            MandatoryRule<int> rule = new MandatoryRule<int>(string.Empty);
            rule.Validate(42).Should().BeFalse("Numeric value ist not null");
        }

        [Fact]
        public void ErrorMessage_should_be_equal_to_given_message()
        {
            string message = "Empty value is not allowed";
            MandatoryRule<int?> rule = new MandatoryRule<int?>(message);
            rule.Message.Should().BeEquivalentTo(message);
        }
    }
}