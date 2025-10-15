using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumSaly
{
    [TestClass]
    public class AsignacionTrabajadorTest
    {
        private IWebDriver driver;
        private const string AppUrl = "http://frontend-beautysaly.somee.com/";

        // Propiedad para el contexto de la prueba
        public TestContext TestContext { get; set; }

        // Inicializa el controlador del navegador
        [TestInitialize]
        public void Setup()
        {
            driver = new EdgeDriver();

        }

        [TestMethod]
        public void TestLoginAdmin()
        {
            //Abre el navegador y navega a la URL de la aplicación
            driver.Navigate().GoToUrl(AppUrl);

            System.Threading.Thread.Sleep(1000);

            //Agrega las credenciales para poder iniciar sesión y acceder a la aplicación
            IWebElement InputEmail = driver.FindElement(By.Id("inputemail"));
            InputEmail.Clear();
            InputEmail.SendKeys("marvinadmin@gmail.com");

            IWebElement InputPassword = driver.FindElement(By.Id("inputpassword"));
            InputPassword.Clear();
            InputPassword.SendKeys("12345");

            IWebElement BtnLogin = driver.FindElement(By.Id("btnIngresar"));
            BtnLogin.Click();

            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                bool LoginExitoso = wait.Until(ExpectedConditions.UrlContains("/sobrenosotros"));

                Assert.IsTrue(LoginExitoso);

                TestContext.WriteLine("Éxito: El inicio de sesión fue exitoso y se redirigió a la vista 'Sobre Nosotros'.");

            }
            catch (Exception ex)
            {
                Assert.Fail($"Fallo: Ocurrió una excepción inesperada al iniciar sesión: {ex.Message}");

            }
        }


        [TestMethod]
        public void TestLoginTrabajador()
        {
            //Abre el navegador y navega a la URL de la aplicación
            driver.Navigate().GoToUrl(AppUrl);

            System.Threading.Thread.Sleep(1000);

            //Agrega las credenciales para poder iniciar sesión y acceder a la aplicación
            IWebElement InputEmail = driver.FindElement(By.Id("inputemail"));
            InputEmail.Clear();
            InputEmail.SendKeys("antonio@gmail.com");

            IWebElement InputPassword = driver.FindElement(By.Id("inputpassword"));
            InputPassword.Clear();
            InputPassword.SendKeys("12345");

            IWebElement BtnLogin = driver.FindElement(By.Id("btnIngresar"));
            BtnLogin.Click();

            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                bool LoginExitoso = wait.Until(ExpectedConditions.UrlContains("/sobrenosotros"));

                Assert.IsTrue(LoginExitoso);

                TestContext.WriteLine("Éxito: El inicio de sesión fue exitoso y se redirigió a la vista 'Sobre Nosotros'.");

            }
            catch (Exception ex)
            {
                Assert.Fail($"Fallo: Ocurrió una excepción inesperada al iniciar sesión: {ex.Message}");

            }
        }

        [TestCleanup]
        public void Clear()
        {
            // Cierra el navegador y libera los recursos
            driver.Quit();
        }

    }
}
