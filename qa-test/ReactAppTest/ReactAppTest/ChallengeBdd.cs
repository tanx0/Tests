using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using ReactAppTest.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.BDDfy;

namespace ReactAppTest
{
    [Story(
       AsA = "As an experienced Test Automation Engineer",
       IWant = "I want complete this challenge",
       SoThat = "So that I could prove my coding abilities")]
    [TestFixture]
    public class ChallengeBdd
    {

        IWebDriver _driver;
        private string _url;

        [SetUp]
        public void SetUp()
        {
            _url = Config.Url;
            Console.WriteLine(_url);
            _driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        string answer1 = string.Empty;
        string answer2 = string.Empty;
        string answer3 = string.Empty;

        [Test]
        public void CompleteChallengeBdd()
        {
            this.Given(s => s._I_open_Challenge_page())
            .When(s => s._I_Render_the_challenge())
            .Then(s => s.Table_is_displayed())
            .When(s => s._I_Find_Answers())
            .And(s => s._I_submit_answers())
            .Then(s => s._I_get_Congrats_message())
            .And(s => s.The_Challenge_Is_Complete())
            .BDDfy("Coding Challenge");
        }

        #region Steps

        private void _I_open_Challenge_page()
        {
            _driver.NavigateTo(_url);
            string title = _driver.Title;
            Assert.AreEqual("React App", title);
        }

        private void _I_Render_the_challenge()
        {
            RenderTheChallengeButton.Click();
        }

        private void Table_is_displayed()
        {
            tables = _driver.FindElements(By.TagName("table"));
            Assert.IsTrue(tables.Count > 0, "Tables were not displayed");
        }

        private void _I_Find_Answers()
        {
            int?[] answers = FindAnswers();
            Assert.AreEqual(3, answers.Length, "3 answers expected");
            answer1 = answers[0].Value.ToString();
            answer2 = answers[1].Value.ToString();
            answer3 = answers[2].Value.ToString();
        }

        private void _I_submit_answers()
        {

            FillInText(Answer1, answer1);
            FillInText(Answer2, answer2);
            FillInText(Answer3, answer3);
            FillInText(Name, "Tanya");
            SubmitButton.Click();
        }

        private void _I_get_Congrats_message()
        {
            Assert.IsTrue(IsCongratsMessageDisplayed(), "Congrats message was not displayed");

        }

        private void The_Challenge_Is_Complete()
        {
            CloseButton.Click();
        }

        #endregion

        #region Private methods


        private ReadOnlyCollection<IWebElement> tables = null;
        private int?[] FindAnswers()
        {

            return GetAnswers(tables).ToArray();
        }

        private static IEnumerable<int?> GetAnswers(ReadOnlyCollection<IWebElement> tables)
        {
            foreach (IWebElement table in tables)
            {
                ReadOnlyCollection<IWebElement> rows = table.FindElements(By.TagName("tr"));
                Console.WriteLine("Table has {0} rows", rows.Count());
                foreach (IWebElement row in rows)
                {
                    var cells = GetCells(row).ToArray();
                    yield return Utils.FindMiddleIndex(cells);
                }
            }
        }

        private static IEnumerable<int> GetCells(IWebElement row)
        {
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));
            foreach (IWebElement cell in cells)
                yield return int.Parse(cell.Text);
        }

        private void FillInText(IWebElement element, string text)
        {
            element.Click();
            _driver.Type(text);
        }

        private IWebElement Answer1
        {
            get
            {
                return _driver.Find(By.XPath("//input[@data-test-id='submit-1']"));
            }
        }

        private IWebElement Answer2
        {
            get
            {
                return _driver.Find(By.XPath("//input[@data-test-id='submit-2']"));
            }
        }

        private IWebElement Answer3
        {
            get
            {
                return _driver.Find(By.XPath("//input[@data-test-id='submit-3']"));
            }
        }

        private IWebElement Name
        {
            get
            {
                return _driver.Find(By.XPath("//input[@data-test-id='submit-4']"));
            }
        }

        private IWebElement SubmitButton
        {
            get
            {
                return _driver.Find(By.XPath("//span[contains(text(),'Submit Answers')]"));
            }
        }

        private bool IsCongratsMessageDisplayed()
        {
            IWebElement message;
            return _driver.TryFind(By.XPath("//div[contains(text(),'Congratulations you have succeeded. Please submit ')]"), out message);

        }


        private IWebElement CloseButton
        {
            get
            {
                return _driver.Find(By.XPath("//span[contains(text(),'Close')]"));
            }
        }

        private IWebElement RenderTheChallengeButton
        {
            get
            {
                return _driver.Find(By.XPath("//span[contains(text(),'Render the Challenge')]"));
            }
        }



        #endregion
    }
}
