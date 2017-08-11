using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class UrlPage : BasePage
    {  
        public UrlPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'This a sample link']")]
        public IWebElement SimpleMorphableLink { get; set;  }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Targetted: This a sample link']")]
        public IWebElement BlankTargettedLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'iframe-targetted: This a sample link']")]
        public IWebElement IframeTargettedLink { get; set; }

        public void ClickSimpleMorphableLink()
        {
            ClickOn(SimpleMorphableLink);
        }
        public void ClickBlankTargettedLink()
        {
            ClickOn(BlankTargettedLink);
        }
        public void ClickIframeTargettedLink()
        {
            ClickOn(IframeTargettedLink);
        }

    }
}
