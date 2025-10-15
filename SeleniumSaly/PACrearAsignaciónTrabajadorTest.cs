using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;


namespace SeleniumSaly
{
    [TestClass]
    public class PACrearAsignaciónTrabajadorTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private const string AppUrl = "http://frontend-beautysaly.somee.com/";
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            driver = new EdgeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }

        //TEST DE LOGIN TRABJADOR
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

        //TEST DE CREAR ASIGNACIÓN TRABAJADOR
        [TestMethod]
        public void CrearAsignacionServicioTrabajador()
        {
            try
            {
                // 1️⃣ Iniciar sesión
                TestLoginTrabajador();

                // 2️⃣ Navegar a la página de Asignaciones
                driver.Navigate().GoToUrl(AppUrl + "ServiciosTrabajador/servicio-trabajador");
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h3.form-title")));

                // 3️⃣ Clic en "Crear Nuevo"
                IWebElement btnCrearNuevo = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//button[contains(text(),'Crear Nuevo')]")));
                btnCrearNuevo.Click();

                // 4️⃣ Seleccionar un servicio del combo
                IWebElement selectServicio = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("select")));
                var select = new SelectElement(selectServicio);
                select.SelectByIndex(1); // selecciona el segundo servicio

                // 5️⃣ Clic en "Guardar"
                IWebElement btnGuardar = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//button[contains(text(),'Guardar')]")));
                btnGuardar.Click();

                // 6️⃣ Esperar mensaje de confirmación
                IWebElement mensaje = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//p[contains(text(),'Registro creado correctamente')]")));

                // 7️⃣ Validar resultado
                Assert.IsTrue(mensaje.Displayed, "No se mostró el mensaje de éxito.");
                TestContext.WriteLine("✅ Éxito: La asignación servicio-trabajador fue creada correctamente.");

            }
            catch (Exception ex)
            {
                Assert.Fail($"❌ Fallo en la prueba de creación de asignación: {ex.Message}");
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
