using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
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
                        driver = new RemoteWebDriver(remoteWebDriverUri, new ChromeOptions());
                        break;
                    }
                case Config.Browser.Edge:
                    {
                        driver = new RemoteWebDriver(remoteWebDriverUri, new EdgeOptions());
                        break;
                    }
                case Config.Browser.Firefox:
                    {
                        driver = new RemoteWebDriver(remoteWebDriverUri, new FirefoxOptions());
                        break;
                    }
            }

            IWebDriver eventDriver = new KitchenSinkTestEventListener(driver);
            driver = eventDriver;
            driver.Manage().Timeouts().PageLoad = timeout;
            driver.Manage().Timeouts().AsynchronousJavaScript = timeout;
            return driver;
        }
    }
}