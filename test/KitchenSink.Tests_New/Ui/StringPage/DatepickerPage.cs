﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests_New.Ui.StringPage
{
    public class DatepickerPage : BasePage
    {

        [FindsBy(How = How.CssSelector, Using = ".automated-tests-pikaday__input")]
        public IWebElement DatePicker { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class = 'pika-lendar']//table//tbody//td[@class = 'is-selected']//button[@class = 'pika-button pika-day']")]
        public IWebElement SelectedDay { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".automated-tests-year__input")]
        public IWebElement YearInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".automated-tests-month__input")]
        public IWebElement MonthInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".automated-tests-day__input")]
        public IWebElement DayInput { get; set; }

        public DatepickerPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        public void SelectDate(string date)
        {
            DatePicker.Clear();
            DatePicker.SendKeys(date);
            DatePicker.SendKeys(Keys.Enter);
            ClickOn(SelectedDay);
        }

        public string GetYear()
        {
            var year = YearInput.GetAttribute("value");
            return year;
        }

        public string GetMonth()
        {
            var month = MonthInput.GetAttribute("value");
            return month;
        }

        public string GetDay()
        {
            var day = DayInput.GetAttribute("value");
            return day;
        }
    }
}