using FakeItEasy;
using FluentAssertions;
using IT2media.Standard.Validation;
using Xunit;

namespace IT2media.Standard.xUnitTests.Validation
{
    public class ValidatableObjectTests
    {
        [Fact]
        public void Validationlist_should_be_empty()
        {
            ValidatableObject<string> stringObject = new ValidatableObject<string>();
            stringObject.Validations.Should().BeEmpty("Validationlist is not empty");
        }

        [Fact]
        public void ErrorMessage_should_be_equal_to_given_message()
        {
            string message = "The error message of the rule";
            IValidationRule<string> mockValidationRule = A.Fake<IValidationRule<string>>();
            A.CallTo(() => mockValidationRule.Message).Returns(message);
            A.CallTo(() => mockValidationRule.Validate(A<string>._)).Returns(true);
            ValidatableObject<string> stringObject = new ValidatableObject<string>(mockValidationRule);

            stringObject.Messages.Should().Contain(message);
        }

        [Fact]
        public void ErrorMessages_should_be_equal_to_given_messages()
        {
            string messageMail = "The mail message of the rule";
            string messageLength = "The length message of the rule";
            IValidationRule<string> mockMailRule = A.Fake<IValidationRule<string>>();
            A.CallTo(() => mockMailRule.Message).Returns(messageMail);
            IValidationRule<string> mockLengthRule = A.Fake<IValidationRule<string>>();
            A.CallTo(() => mockLengthRule.Message).Returns(messageLength);
            ValidatableObject<string> stringObject = new ValidatableObject<string>(mockMailRule, mockLengthRule);

            stringObject.Messages.Should().Contain(messageMail);
            stringObject.Messages.Should().Contain(messageLength);
        }

        [Fact]
        public void Validation_should_be_true()
        {
            IValidationRule<string> mockRule = A.Fake<IValidationRule<string>>();
            A.CallTo(() => mockRule.Validate(A<string>._)).Returns(true);
            ValidatableObject<string> stringObject = new ValidatableObject<string>(mockRule);
            stringObject.Validate();
            stringObject.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validation_should_be_false()
        {
            IValidationRule<string> mockRule = A.Fake<IValidationRule<string>>();
            A.CallTo(() => mockRule.Validate(A<string>._)).Returns(false);
            ValidatableObject<string> stringObject = new ValidatableObject<string>(mockRule);
            stringObject.Validate();
            stringObject.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validation_after_construct_should_be_true()
        {
            ValidatableObject<string> stringObject = new ValidatableObject<string>();
            stringObject.Value = "42";
            stringObject.IsValid.Should().BeTrue();
        }
    }
}