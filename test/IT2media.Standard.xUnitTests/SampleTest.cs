using Xunit;

namespace IT2media.Standard.xUnitTests
{
    public class SampleTest
    {
        [Fact]
        public void StringPropertyTest()
        {
            ISample sample = new Sample();

            Assert.Null(sample.StringProperty);
        }
    }
}
