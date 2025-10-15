using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace SeleniumSaly
{
    [TestClass]
    public class ReseñaTestCliente
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
        public void TestLoginCLiente()
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
        public void CrearReseñaTest()
        {
            // Configuración de Espera Inteligente
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // Iniciar sesión primero
            TestLoginCLiente();

            // Convertir el driver a IJavaScriptExecutor
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            // Disminuir el zoom al 50% para alejar la página
            js.ExecuteScript("document.body.style.zoom = '0.5'");

            // Espera a que el botón de reseña sea clickeable en la vista "/sobrenosotros"
            IWebElement BtnReseña = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnCalificar")));
            BtnReseña.Click();

            // Espera a que el formulario de reseña aparezca (ej: InputNombre)
            IWebElement InputNombre = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InputNombre")));
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
            InputComentario.SendKeys("Excelente servicio, muy buena atención al cliente,nuevas reseña");

            // Envía el formulario de reseña
            IWebElement BtnEnviar = driver.FindElement(By.Id("btnEnviar"));
            BtnEnviar.Click();

            try
            {
                // Espera hasta que la URL contenga "/sobrenosotros" para confirmar que la reseña fue enviada exitosamente
                bool ReseñaExitosa = wait.Until(ExpectedConditions.UrlContains("/sobrenosotros"));

                // Assert: Verifica la condición
                Assert.IsTrue(ReseñaExitosa);

                // Mensaje de Éxito
                TestContext.WriteLine("Éxito: La reseña fue enviada exitosamente y se redirigió a la vista 'Sobre Nosotros'.");
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada
                TestContext.WriteLine($"Fallo:ocurrio un error al momento de crear la reseña");
                Assert.Fail($"Fallo: Ocurrió una excepción inesperada durante la creacion de la reseña: {ex.Message}");
            }

        }
    }
}
