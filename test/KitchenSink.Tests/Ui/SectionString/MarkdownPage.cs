using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui.SectionString
{
    public class MarkdownPage : BasePage
    {
        public MarkdownPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        public bool CheckPreviewVisible()
        {
            var shadowRoot = ExpandShadowRoot(Driver.FindElement(By.XPath("//juicy-markdown")));
            return shadowRoot.FindElement(By.TagName("h1")).Displayed; //BUG in CHROME b.getElementsByTagName is not a function
        }

        public string GetHeaderText()
        {
            var shadowRoot = ExpandShadowRoot(Driver.FindElement(By.XPath("//juicy-markdown"))); 
            return shadowRoot.FindElement(By.TagName("h1")).Text; //BUG in CHROME b.getElementsByTagName is not a function
        }
    }
}
