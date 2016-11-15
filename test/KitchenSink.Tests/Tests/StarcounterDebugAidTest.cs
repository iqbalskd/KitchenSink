using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using KitchenSink.Tests;
using OpenQA.Selenium.Interactions;

namespace KitchenSink.Tests.Tests
{
    [TestFixture("firefox")]
    [TestFixture("chrome")]
    [TestFixture("edge")]
    public class StarcounterDebugAidTest : BaseTest
    {
        public StarcounterDebugAidTest(string browser) : base(browser)
        {
        }

        [Test]
        public void StarcounterDebugAidTest_OpenAndClose()
        {
            driver.Navigate().GoToUrl(baseURL + "/Text");
            this.WaitUntil(ExpectedConditions.PresenceOfAllElementsLocatedBy(ByHelper.StarcounterIncludeWithInputText));

            var debugAidElement = driver.FindElement(By.ClassName("starcounter-debug-aid"));
            Assert.IsFalse(debugAidElement.Displayed);

            if (browser == "firefox")
            {
                OpenAndCloseWithJavaScript();
            }
            else
            {
                OpenAndCloseWithActionsBuilder();
            }
        }

        private void OpenAndCloseWithJavaScript()
        {
            (driver as OpenQA.Selenium.Remote.RemoteWebDriver).ExecuteScript(
                string.Format("{0}{1}{2}",
                "var keyEvent = document.createEvent('KeyboardEvent');",
                "keyEvent.initKeyEvent(\"keydown\", true, true, null, true, false, false, false, 220, 0);",
                "document.dispatchEvent(keyEvent);")
            );

            this.WaitUntil(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("html body starcounter-debug-aid")));
            var debugAidElement = driver.FindElement(By.ClassName("starcounter-debug-aid"));
            Assert.IsTrue(debugAidElement.Displayed);

            (driver as OpenQA.Selenium.Remote.RemoteWebDriver).ExecuteScript(
                string.Format("{0}{1}{2}",
                "var keyEvent = document.createEvent('KeyboardEvent');",
                "keyEvent.initKeyEvent(\"keydown\", true, true, null, false, false, false, false, 27, 0);",
                "document.dispatchEvent(keyEvent);")
            );

            debugAidElement = driver.FindElement(By.ClassName("starcounter-debug-aid"));
            Assert.IsFalse(debugAidElement.Displayed);
        }

        private void OpenAndCloseWithActionsBuilder()
        {
            // Open debug-aid
            Actions action = new Actions(driver).KeyDown(Keys.Control).SendKeys("`").KeyUp(Keys.Control);
            action.Build().Perform();

            var debugAidElement = driver.FindElement(By.ClassName("starcounter-debug-aid"));
            Assert.IsTrue(debugAidElement.Displayed);

            // Close debug-aid
            new Actions(driver).SendKeys(Keys.Escape).Build().Perform();

            debugAidElement = driver.FindElement(By.ClassName("starcounter-debug-aid"));
            Assert.IsFalse(debugAidElement.Displayed);
        }
    }
}