// Required for xUnit tests
using Xunit;
namespace Tests
{
    public class ExampleTests
    {
        [Fact]
        public void SimpleAddition_Works()
        {
            int a = 2;
            int b = 3;
            int sum = a + b;
            Assert.Equal(5, sum);
        }
    }
}
