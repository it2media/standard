// ReSharper disable once CheckNamespace
namespace IT2media.Standard.Validation
{
    public interface IValidationRule<in TType>
    {
        string Message { get; }

        bool Validate(TType value);
    }
}