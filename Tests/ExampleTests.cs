// Required for NUnit tests
using NUnit.Framework;
namespace Tests
{
    [TestFixture]
    public class ExampleTests
    {
        [Test]
        public void SimpleAddition_Works()
        {
            int a = 2;
            int b = 3;
            int sum = a + b;
            Assert.AreEqual(5, sum);
        }
    }
}
