using EscolaClient.Test.Orderer;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EscolaClient.Test.Tests
{
    public class TurmaTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private const string _baseUrl = "https://localhost:7160";

        public TurmaTests()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Fact, TestPriority(1)]
        public void TurmaTests_ShouldCreateTurmaWithValidData()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/Turma/Create");

            _driver.FindElement(By.Name("CursoId")).SendKeys("123");
            _driver.FindElement(By.Name("Nome")).SendKeys($"Turma_{DateTime.Now:HH:mm:ss:fff}");
            _driver.FindElement(By.Name("Ano")).SendKeys(DateTime.Now.Year.ToString());

            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            Assert.Equal($"{_baseUrl}/Turma", _driver.Url);
        }

        [Fact, TestPriority(2)]
        public void TurmaTests_ShouldEditAlunoWithValidData()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/Turma");

            _driver.FindElement(By.Id("edit")).Click();

            _driver.FindElement(By.Name("CursoId")).Clear();
            _driver.FindElement(By.Name("CursoId")).SendKeys("2");

            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            Assert.Equal($"{_baseUrl}/Turma", _driver.Url);
        }

        [Fact, TestPriority(3)]
        public void TurmaTests_ShouldDeleteTurmaWithValidData()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/Turma");

            _driver.FindElements(By.Id("delete")).Last().Click();

            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            Assert.Equal($"{_baseUrl}/Turma", _driver.Url);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
