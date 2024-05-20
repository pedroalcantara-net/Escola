using EscolaClient.Test.Orderer;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace EscolaClient.Test.Tests
{
    public class AlunoTurmaTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private const string _baseUrl = "https://localhost:7160";

        public AlunoTurmaTests()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Fact, TestPriority(1)]
        public void AlunoTurmaTests_ShouldCreateAlunoTurmaWithValidData()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/AlunoTurma/Create");

            var aluno = new SelectElement(_driver.FindElement(By.Id("AlunoId")));
            var alunoId =
                aluno.Options
                .Select(option => int.Parse(option.GetAttribute("value")))
                .ToList()
                .Max();
            aluno.SelectByValue(alunoId.ToString());

            var turma = new SelectElement(_driver.FindElement(By.Id("TurmaId")));
            var turmaId =
                turma.Options
                .Select(option => int.Parse(option.GetAttribute("value")))
                .ToList()
                .Max();
            turma.SelectByValue(turmaId.ToString());

            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            Assert.Equal($"{_baseUrl}/AlunoTurma/Turma/{turmaId}", _driver.Url);
        }

        [Fact, TestPriority(2)]
        public void AlunoTurmaTests_ShouldDeleteAlunoTurma()
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/AlunoTurma");

            _driver.FindElements(By.Id("manage")).Last().Click();

            _driver.FindElements(By.Id("delete")).Last().Click();

            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            Assert.Contains($"{_baseUrl}/AlunoTurma/Turma/", _driver.Url);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
