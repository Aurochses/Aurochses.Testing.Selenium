using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Aurochses.Xunit.Selenium.Tests
{
    public class SeleniumWebDriverTypeTests
    {
        [Fact]
        public void IsEnum()
        {
            // Arrange & Act & Assert
            Assert.True(typeof(SeleniumWebDriverType).GetTypeInfo().IsEnum);
        }

        [Fact]
        public void Validate_Values()
        {
            // Arrange & Act & Assert
            Assert.Equal(
                new[]
                {
                    SeleniumWebDriverType.Edge,
                    SeleniumWebDriverType.Firefox,
                    SeleniumWebDriverType.GoogleChrome,
                    SeleniumWebDriverType.InternetExplorer
                },
                Enum.GetValues(typeof(SeleniumWebDriverType)).Cast<SeleniumWebDriverType>()
            );
        }
    }
}