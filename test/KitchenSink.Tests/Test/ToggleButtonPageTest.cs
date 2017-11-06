﻿using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;

namespace KitchenSink.Tests.Test
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    [Ignore("For test purposes")]

    class ToggleButtonPageTest : BaseTest
    {
        private ToggleButtonPage _toggleButtonPage;
        private MainPage _mainPage;

        public ToggleButtonPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _toggleButtonPage = _mainPage.GoToToggleButtonPage();
        }

        [Test]
        public void ToggleButtonPage_CheckboxUncheckedAndCheckedAgain()
        {
            WaitUntil(x => _toggleButtonPage.ToogleButton.Displayed);
            WaitUntil(x => _toggleButtonPage.InfoLabel.Displayed);
            Assert.AreEqual("I accept terms and conditions", _toggleButtonPage.InfoLabel.Text);
            _toggleButtonPage.ChangeToggleButtonState();
            Assert.IsTrue(WaitForText(_toggleButtonPage.InfoLabel, "I don't accept terms and conditions", 5));
            _toggleButtonPage.ChangeToggleButtonState();
            Assert.IsTrue(WaitForText(_toggleButtonPage.InfoLabel, "I accept terms and conditions", 5));
        }
    }
}
