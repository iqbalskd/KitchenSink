using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KitchenSink.Tests.Test
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    internal class UrlPageTest : BaseTest
    {
        private UrlPage _UrlPage;
        private MainPage _mainPage;

        public UrlPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _UrlPage = _mainPage.GoToUrlPage();
        }

        private string getIframeCurrentURL()
        {
            IJavaScriptExecutor JSExecuter = (IJavaScriptExecutor)Driver;
            return (string)JSExecuter.ExecuteScript("return document.querySelector('#link-target').contentWindow.location.href");
        }

        [TearDown]
        public void TeadDown()
        {
            _UrlPage = _mainPage.GoToUrlPage();
        }

        [Test]
        public void UrlPage_ClickSimpleLink()
        {
            IJavaScriptExecutor JSExecuter = (IJavaScriptExecutor)Driver;
            WaitUntil(x => _UrlPage.SimpleMorphableLink.Displayed);

            // leave a foot print in the window object
            JSExecuter.ExecuteScript("window.footprintExists = true");

            // control test 
            Assert.AreEqual("http://localhost:8080/KitchenSink/Url", Driver.Url);
            Assert.AreEqual(true, JSExecuter.ExecuteScript("return window.footprintExists"));

            _UrlPage.ClickSimpleMorphableLink();

            System.Threading.Thread.Sleep(2000);

            Assert.AreEqual(Driver.Url, "http://localhost:8080/KitchenSink");

            // if the foot print still exists, we can infer that the page was actually morphed, not fully loaded
            Assert.AreEqual(true, JSExecuter.ExecuteScript("return window.footprintExists"));

        }
        
        [Test]
        public void UrlPage_ClickBlankTargettedLink()
        {
            WaitUntil(x => _UrlPage.BlankTargettedLink.Displayed);

            //control test 
            Assert.AreEqual(Driver.WindowHandles.Count, 1);

            _UrlPage.ClickBlankTargettedLink();

            System.Threading.Thread.Sleep(500);

            Assert.AreEqual(Driver.WindowHandles.Count, 2);

            //close pop up
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
            Driver.Close();

            // use the original page
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
        }

        [Test]
        public void UrlPage_ClickIframeTargettedLink()
        {
            WaitUntil(x => _UrlPage.IframeTargettedLink != null && _UrlPage.IframeTargettedLink.Displayed);

            //control test 
            Assert.AreEqual(getIframeCurrentURL(), "about:blank");

            _UrlPage.ClickIframeTargettedLink();

            System.Threading.Thread.Sleep(500);

            Assert.AreEqual(getIframeCurrentURL(), "http://localhost:8080/KitchenSink");
        }
    }
}
