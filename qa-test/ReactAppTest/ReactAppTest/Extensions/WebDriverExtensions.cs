using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace ReactAppTest.Extensions
{
    public static class WebDriverExtensions
    {

        public static void NavigateTo(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);           
        }

        public static void Type(this IWebDriver driver, string keys)
        {
            Actions action = new Actions(driver);
            action.SendKeys(keys).Perform();
        }


        public static IWebElement Find(this IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds)) { PollingInterval = TimeSpan.FromMilliseconds(Config.PollingIntervalMilliseconds) };
                return wait.Until(drv => drv.FindElement(@by));
            }
            return driver.FindElement(@by);
        }

        /// <summary>
        /// Try to find an element within timeout
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="element"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns>True if element was found, or False if not</returns>
        public static bool TryFind(this IWebDriver driver, By by, out IWebElement element, int timeoutInSeconds = 10)
        {
            try
            {
                if (timeoutInSeconds > 0)
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds)) { PollingInterval = TimeSpan.FromMilliseconds(Config.PollingIntervalMilliseconds) };
                    element = wait.Until(drv => drv.FindElement(@by));
                }
                element = driver.FindElement(@by);
                return true;
            }
            catch (NoSuchElementException)
            {
                element = null;
                return false;
            }
        }
    }
}