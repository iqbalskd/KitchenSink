using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Ui.SectionString;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KitchenSink.Tests.Test.SectionString
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    internal class TextPageTest : BaseTest
    {
        private TextPage _textPage;
        private MainPage _mainPage;

        public TextPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _textPage = _mainPage.GoToTextPage();
        }

        [Test]
        public void TextPage_TextPropagationOnUnfocus()
        {
            WaitUntil(x => _textPage.Input.Displayed);

            _textPage.FillInput(_textPage.Input, "Krystian");
            Assert.IsTrue(WaitForText(_textPage.InputInfoLabel, "Hi, Krystian!", 5));
            _textPage.ClearInput(_textPage.Input);
            WaitUntil(x => _textPage.Input.Text == string.Empty);
            Assert.AreEqual("What's your name?", _textPage.InputInfoLabel.Text);
        }

        [Test]
        public void TextPage_TextPropagationWhileTyping()
        {
            WaitUntil(x => _textPage.InputDynamic.Displayed);

            _textPage.FillInput(_textPage.InputDynamic, "K");
            Assert.IsTrue(WaitForText(_textPage.InputInfoLabelDynamic, "Hi, K!", 5));
            _textPage.ClearInput(_textPage.InputDynamic);
            WaitUntil(x => _textPage.InputDynamic.Text == string.Empty);
            Assert.AreEqual("What's your name?", _textPage.InputInfoLabelDynamic.Text);
        }

        [Test]
        public void TextPage_TextPropagationForPaperTextOnUnfocus()
        {
            // Make sure that paper element is loaded.
            WaitUntil(x => _textPage.PaperInput.Displayed);

            var shadowRoot = _textPage.GetShadowRootByXpath("//paper-input[1]");
            var shadowInput = _textPage.GetInputFromShadowRootById(shadowRoot, "input");
            var shadowInfoLabel = _textPage.GetLabelFromShadowRootById(shadowRoot, "paper-input-label-1");

            _textPage.FillInput(shadowInput, "Krystian");
            Assert.IsTrue(WaitForText(shadowInfoLabel, "Hi, Krystian!", 5));
            _textPage.ClearInput(shadowInput);
            WaitUntil(x => shadowInput.Text == string.Empty);
            Assert.AreEqual("What's your name?", shadowInfoLabel.Text);
        }

        [Test]
        public void TextPage_TextPropagationForPaperTextWhileTyping()
        {
            // Make sure that paper element is loaded.
            WaitUntil(x => _textPage.PaperInputDynamic.Displayed);

            var shadowRoot = _textPage.GetShadowRootByXpath("//paper-input[2]");
            var shadowInput = _textPage.GetInputFromShadowRootById(shadowRoot, "input");
            var shadowInfoLabel = _textPage.GetLabelFromShadowRootById(shadowRoot, "paper-input-label-2");

            _textPage.FillInput(shadowInput, "K");
            Assert.IsTrue(WaitForText(shadowInfoLabel, "Hi, K!", 5));
            _textPage.ClearInput(shadowInput);
            WaitUntil(x => shadowInput.Text == string.Empty);
            Assert.AreEqual("What's your name?", shadowInfoLabel.Text);
        }
    }
}