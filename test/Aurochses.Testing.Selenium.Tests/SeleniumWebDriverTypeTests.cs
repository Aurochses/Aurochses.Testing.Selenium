using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Aurochses.Testing.Selenium.Tests
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
            foreach (var value in Enum.GetValues(typeof(SeleniumWebDriverType)).Cast<SeleniumWebDriverType>())
            {
                switch (value)
                {
                    case SeleniumWebDriverType.Edge:
                    {
                        Assert.Equal(0, (int) value);
                        break;
                    }
                    case SeleniumWebDriverType.Firefox:
                    {
                        Assert.Equal(1, (int) value);
                        break;
                    }
                    case SeleniumWebDriverType.GoogleChrome:
                    {
                        Assert.Equal(2, (int) value);
                        break;
                    }
                    case SeleniumWebDriverType.InternetExplorer:
                    {
                        Assert.Equal(3, (int) value);
                        break;
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid enum value.");
                    }
                }
            }
        }
    }
}