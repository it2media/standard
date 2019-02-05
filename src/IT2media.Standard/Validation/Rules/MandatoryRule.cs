namespace IT2media.Standard.Validation.Rules
{
    public class MandatoryRule<TType> : IValidationRule<TType>
    {
        public MandatoryRule(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public bool Validate(TType value)
        {
            if (value == null)
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(value as string);
        }
    }
}