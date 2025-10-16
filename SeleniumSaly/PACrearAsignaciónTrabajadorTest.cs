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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Manage().Window.Maximize();
        }

        // 🔹 Login visual
        private void LoginTrabajador()
        {
            try
            {
                driver.Navigate().GoToUrl(AppUrl);

                // Espera explícita hasta que el input de email esté visible
                IWebElement inputEmail = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputemail")));
                inputEmail.Clear();
                inputEmail.SendKeys("antonio@gmail.com");

                IWebElement inputPassword = driver.FindElement(By.Id("inputpassword"));
                inputPassword.Clear();
                inputPassword.SendKeys("12345");

                IWebElement btnLogin = driver.FindElement(By.Id("btnIngresar"));
                btnLogin.Click();

                // Espera que la URL cambie a /sobrenosotros para confirmar login
                wait.Until(ExpectedConditions.UrlContains("/sobrenosotros"));
                TestContext.WriteLine("✅ Éxito: Login realizado correctamente.");
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("❌ Fallo: No se pudo iniciar sesión, elemento no encontrado o URL incorrecta.");
            }
        }

        [TestMethod]
        public void CrearAsignacionServicioTrabajadorTest()
        {
            try
            {
                // 1️ Login
                LoginTrabajador();

                // 2️ Navegar al módulo de asignaciones mediante clic (mantiene token)
                IWebElement btnModuloAsignaciones = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//a[contains(@href,'ServiciosTrabajador/servicio-trabajador')]")));
                btnModuloAsignaciones.Click();

                // 3️ Esperar título del módulo
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h3.form-title")));

                // 4️ Clic en "Crear Nuevo"
                IWebElement btnCrearNuevo = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//button[contains(text(),'Crear Nuevo')]")));
                btnCrearNuevo.Click();

                // 5️ Esperar combo de servicios y seleccionar la segunda opción
                wait.Until(driver =>
                {
                    try
                    {
                        var combo = driver.FindElement(By.Id("selectServicio"));
                        var opciones = combo.FindElements(By.TagName("option"));
                        if (opciones.Count > 1)
                        {
                            var select = new SelectElement(combo);
                            select.SelectByIndex(1); // segunda opción
                            return true;
                        }
                        return false;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });

                // 6️ Clic en "Guardar"
                IWebElement btnGuardar = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//button[contains(text(),'Guardar')]")));
                btnGuardar.Click();

                // 7️ Esperar mensaje de éxito
                IWebElement mensaje = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mensajeExito")));

                // 8️ Validar mensaje
                Assert.IsTrue(mensaje.Displayed && mensaje.Text.Contains("Registro creado correctamente"),
                    "❌ No se mostró el mensaje de éxito.");

                TestContext.WriteLine("✅ Éxito: La asignación servicio–trabajador fue creada correctamente.");
            }
            catch (Exception ex)
            {
                Assert.Fail($"❌ Fallo durante la creación de asignación: {ex.Message}");
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
