// Required for xUnit tests
using Xunit;
namespace Esavi.Common.API.Tests
{
    public class EsaviApplicationTests
    {
        [Fact]
        public void GetUser_ReturnsCorrectUserId()
        {
            var id = "testId";
            var user = new EsaviUser(id);
            var application = EsaviApplication.from(user);

            Assert.Equal(id, application.CurrentUser.Id);
        }
    }
}
