﻿using NUnit.Framework;

namespace KitchenSink.Test.Object
{
    [TestFixture]
    class NestedPartialsPageTest : BaseTest
    {
        private NestedPartialsPage _nestedPartialsPage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mainPage = new MainPage(Driver);
            _nestedPartialsPage = mainPage.GoToNestedPartialsPage();
        }

        [Test]
        public void NestedPartialsPage_AddNewChild()
        {
            WaitUntil(x => _nestedPartialsPage.CountChildDivs() > 0);
            var divsBefore = _nestedPartialsPage.CountChildDivs();
            _nestedPartialsPage.AddChild();
            WaitUntil(x => _nestedPartialsPage.CountChildDivs() > divsBefore);
            var divsAfter = _nestedPartialsPage.CountChildDivs();

            Assert.Greater(divsAfter, divsBefore);
        }
    }
}
