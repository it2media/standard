using System;
using System.Diagnostics.CodeAnalysis;
using IT2media.Standard.Samples.Validation;

namespace IT2media.Standard.Samples
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            var validation = new ValidationSample();
            validation.Validate();

            Console.ReadLine();
        }
    }
}
