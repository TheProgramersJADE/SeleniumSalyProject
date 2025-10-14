using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace SeleniumSaly
{
    [TestClass]
    public class ReseñaTest
    {
        private IWebDriver driver;
        private const string AppUrl = "http://frontend-beautysaly.somee.com/";


        // Inicializa el controlador del navegador antes de cada prueba
        [TestInitialize]
        public void Setup()
        {
            driver = new EdgeDriver();
            
        }

        // Propiedad para el contexto de la prueba
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestReseña()
        {
            //Abre el navegador y navega a la URL de la aplicación
            driver.Navigate().GoToUrl(AppUrl);
            
            System.Threading.Thread.Sleep(1000);

            //Agrega las credenciales para poder iniciar sesión y acceder a la aplicación
            IWebElement InputEmail = driver.FindElement(By.Id("inputemail"));
            InputEmail.Clear();
            InputEmail.SendKeys("profemarvin@gmail.com");

            IWebElement InputPassword = driver.FindElement(By.Id("inputpassword"));
            InputPassword.Clear();
            InputPassword.SendKeys("12345");

            IWebElement BtnLogin = driver.FindElement(By.Id("btnIngresar"));
            BtnLogin.Click();

            System.Threading.Thread.Sleep(2000);

            // Convertir el driver a IJavaScriptExecutor
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            // Disminuir el zoom al 50% para alejar la página
            js.ExecuteScript("document.body.style.zoom = '0.5'");

            System.Threading.Thread.Sleep(1000);

            //navega a la vista "Sobre Nosotros"
            IWebElement BtnReseña = driver.FindElement(By.Id("btnCalificar"));
            BtnReseña.Click();

            System.Threading.Thread.Sleep(1000);

            // Completa el formulario de reseña con datos válidos
            IWebElement InputNombre =driver.FindElement(By.Id("InputNombre"));
            InputNombre.Clear();
            InputNombre.SendKeys("Marvin");

            IWebElement InputTrabajador = driver.FindElement(By.Id("InputTrabajador"));
            InputTrabajador.Clear();
            InputTrabajador.SendKeys("Antonio");

            IWebElement InputCalificacion = driver.FindElement(By.Id("InputCalificacion"));
            InputCalificacion.Clear();
            InputCalificacion.SendKeys("5");

            IWebElement InputComentario = driver.FindElement(By.Id("InputComentario"));
            InputComentario.Clear();
            InputComentario.SendKeys("Excelente servicio, muy buena atención al cliente, otra");

            System.Threading.Thread.Sleep(1000);

            // Envía el formulario de reseña
            IWebElement BtnEnviar = driver.FindElement(By.Id("btnEnviar"));
            BtnEnviar.Click();

            System.Threading.Thread.Sleep(1000);

            // Verifica que la reseña fue enviada exitosamente y se redirigió a la vista "Sobre Nosotros"
            try
            {
                // Espera hasta que la URL contenga "/sobrenosotros" para confirmar que la reseña fue enviada exitosamente
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                bool ReseñaExitosa = wait.Until(ExpectedConditions.UrlContains("/sobrenosotros"));
                Assert.IsTrue(ReseñaExitosa);
                TestContext.WriteLine("Éxito: La reseña fue enviada exitosamente y se redirigió a la vista 'Sobre Nosotros'.");
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada
                Assert.Fail($"Fallo: Ocurrió una excepción inesperada: {ex.Message}");
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
