using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;

namespace KitchenSink.Tests.Utilities
{
    public class WebDriverManager
    {
        public static IWebDriver StartDriver(Config.Browser browser, TimeSpan timeout, Uri remoteWebDriverUri)
        {
            IWebDriver driver = null;
            switch (browser)
            {
                case Config.Browser.Chrome:
                    {
                        driver = new RemoteWebDriver(remoteWebDriverUri, DesiredCapabilities.Chrome());
                        break;
                    }
                case Config.Browser.Edge:
                    {
                        driver = new RemoteWebDriver(remoteWebDriverUri, DesiredCapabilities.Edge());
                        break;
                    }
                case Config.Browser.Firefox:
                    {
                        var profile = new FirefoxProfile();
                        profile.SetPreference("dom.file.createInChild", true); //needed for file upload in Selenium 3.5.3/3.6.0 and FF 55
                        DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
                        string prof = profile.ToBase64String();
                        capabilities.SetCapability(FirefoxDriver.ProfileCapabilityName, prof); //Selenium must be started with -enablePassThrough false
                        driver = new RemoteWebDriver(remoteWebDriverUri, capabilities);
                        break;
                    }
            }

            IWebDriver eventDriver = new KitchenSinkTestEventListener(driver);
            driver = eventDriver;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = timeout;
            driver.Manage().Timeouts().AsynchronousJavaScript = timeout;
            return driver;
        }
    }
}