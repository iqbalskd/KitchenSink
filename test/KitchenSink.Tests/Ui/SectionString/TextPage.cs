using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui.SectionString
{
    public class TextPage : BasePage
    {
        public TextPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-name-input")]
        public IWebElement Input { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-name-input-dynamic")]
        public IWebElement InputDynamic { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-name-label")]
        public IWebElement InputInfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-name-label-dynamic")]
        public IWebElement InputInfoLabelDynamic { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-name-paper-input")]
        public IWebElement PaperInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-name-paper-input-dynamic")]
        public IWebElement PaperInputDynamic { get; set; }

        public void FillInput(IWebElement inputElement, string input)
        {
            inputElement.Clear();
            inputElement.SendKeys(input);
            inputElement.SendKeys(Keys.Enter);
        }

        public void ClearInput(IWebElement inputElement)
        {
            var inputLength = inputElement.GetAttribute("value").Length;

            for (var i = 0; i < inputLength; i++)
            {
                inputElement.SendKeys(Keys.Backspace);
            }

            inputElement.SendKeys(Keys.Enter);
        }
    }
}