using Xunit;

namespace Aurochses.Xunit.Selenium.Tests
{
    public class SeleniumFixtureTests
    {
        [Fact]
        public void GetUrl_Success()
        {
            // Arrange
            const string path = "/test";

            // Act
            var fixture = new SeleniumFixture();

            // Assert
            Assert.Equal(fixture.Configuration["Domain"] + path, fixture.GetUrl(path));
        }

        // todo: test Dispose

        // todo: test GetWebDriver
    }
}