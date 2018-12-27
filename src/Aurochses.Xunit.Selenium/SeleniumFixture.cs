using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Aurochses.Xunit.Selenium
{
    /// <summary>
    /// Selenium Fixture
    /// </summary>
    /// <seealso cref="Aurochses.Xunit.ConfigurationFixture" />
    /// <seealso cref="System.IDisposable" />
    public class SeleniumFixture : ConfigurationFixture, IDisposable
    {
        private readonly IDictionary<SeleniumWebDriverType, IWebDriver> _webDrivers;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumFixture"/> class.
        /// </summary>
        public SeleniumFixture()
        {
            _webDrivers = new Dictionary<SeleniumWebDriverType, IWebDriver>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            foreach (var item in _webDrivers)
            {
                item.Value.Quit();
                item.Value.Dispose();
            }
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public string GetUrl(string path = null)
        {
            return $"{Configuration["Domain"]}{path}";
        }

        /// <summary>
        /// Gets the web driver.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public IWebDriver GetWebDriver(SeleniumWebDriverType type)
        {
            if (!_webDrivers.TryGetValue(type, out var webDriver))
            {
                webDriver = CreateWebDriver(type);

                _webDrivers.Add(type, webDriver);
            }

            return webDriver;
        }

        private static IWebDriver CreateWebDriver(SeleniumWebDriverType type)
        {
            IWebDriver webDriver;

            switch (type)
            {
                case SeleniumWebDriverType.Edge:
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddAdditionalCapability(CapabilityType.AcceptSslCertificates, true);

                    var edgeDriverService = EdgeDriverService.CreateDefaultService(AppContext.BaseDirectory);
                    webDriver = new ThreadLocal<IWebDriver>(() => new EdgeDriver(edgeDriverService, edgeOptions)).Value;
                    break;
                case SeleniumWebDriverType.Firefox:
                    var firefoxProfile = new FirefoxProfile
                    {
                        AcceptUntrustedCertificates = true
                    };

                    var firefoxOptions = new FirefoxOptions
                    {
                        Profile = firefoxProfile
                    };

                    var firefoxDriverService = FirefoxDriverService.CreateDefaultService(AppContext.BaseDirectory);
                    webDriver = new ThreadLocal<IWebDriver>(() => new FirefoxDriver(firefoxDriverService, firefoxOptions, TimeSpan.FromSeconds(60))).Value;
                    break;
                case SeleniumWebDriverType.GoogleChrome:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--ignore-certificate-errors");

                    var chromeDriverService = ChromeDriverService.CreateDefaultService(AppContext.BaseDirectory);
                    webDriver = new ThreadLocal<IWebDriver>(() => new ChromeDriver(chromeDriverService, chromeOptions)).Value;
                    break;
                case SeleniumWebDriverType.InternetExplorer:
                    var internetExplorerOptions = new InternetExplorerOptions();
                    internetExplorerOptions.AddAdditionalCapability(CapabilityType.AcceptSslCertificates, true);

                    var internetExplorerDriverService = InternetExplorerDriverService.CreateDefaultService(AppContext.BaseDirectory);
                    webDriver = new ThreadLocal<IWebDriver>(() => new InternetExplorerDriver(internetExplorerDriverService, internetExplorerOptions)).Value;
                    break;
                default:
                    throw new NullReferenceException("WebDriver is null.");
            }

            webDriver.Manage().Window.Maximize();

            return webDriver;
        }
    }
}