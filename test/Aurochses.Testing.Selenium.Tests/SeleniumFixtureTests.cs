using System;
using Xunit;

namespace Aurochses.Testing.Selenium.Tests
{
    public class SeleniumFixtureTests
    {
        [Fact]
        public void GetUrl_IsValid_WhenEnvironmentIsNull()
        {
            // Arrange & Act
            var fixture = new SeleniumFixture();

            // Assert
            Assert.Equal("http://localhost:8000/test", fixture.GetUrl("/test"));
        }

        [Fact]
        public void GetUrl_IsValid_WhenEnvironmentIsTest()
        {
            // Arrange
            var currentAspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

            // Act
            var fixture = new SeleniumFixture();

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", currentAspNetCoreEnvironment);

            // Assert
            Assert.Equal("http://www.example.com/test", fixture.GetUrl("/test"));
        }

        // todo: test Dispose

        // todo: test GetWebDriver
    }
}