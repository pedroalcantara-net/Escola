using EscolaClient.Test.Orderer;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EscolaClient.Test.Tests
{
    public class AlunoTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private const string _baseUrl = "https://localhost:7160";

        public AlunoTests()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Fact, TestPriority(1)]
        public void AlunoTests_ShouldCreateAlunoWithValidData()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/Aluno/Create");

            _driver.FindElement(By.Name("Nome")).SendKeys("Aluno");
            _driver.FindElement(By.Name("Usuario")).SendKeys($"usuario_{DateTime.Now:HH:mm_ss-fff}");
            _driver.FindElement(By.Name("Senha")).SendKeys("SenhaS3gura!");
            _driver.FindElement(By.Name("SenhaConfirmacao")).SendKeys("SenhaS3gura!");

            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            Assert.Equal($"{_baseUrl}/", _driver.Url);
        }

        [Fact, TestPriority(2)]
        public void AlunoTests_ShouldEditAlunoWithValidData()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/");

            _driver.FindElement(By.Id("edit")).Click();

            _driver.FindElement(By.Name("Nome")).Clear();
            _driver.FindElement(By.Name("Nome")).SendKeys("Aluno Atualizado");
            _driver.FindElement(By.Name("Senha")).SendKeys("SenhaS3gura!");
            _driver.FindElement(By.Name("SenhaConfirmacao")).SendKeys("SenhaS3gura!");

            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            Assert.Equal($"{_baseUrl}/", _driver.Url);
        }

        [Fact, TestPriority(3)]
        public void AlunoTests_ShouldDeleteAlunoWithValidData()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/");

            _driver.FindElements(By.Id("delete")).Last().Click();

            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            Assert.Equal($"{_baseUrl}/", _driver.Url);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
