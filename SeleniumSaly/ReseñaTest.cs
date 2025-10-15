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

            System.Threading.Thread.Sleep(1000);

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
        public void BuscarReseñaTest()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            string idReseña = "25";

            // 2. ACCIONES
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.body.style.zoom = '0.5'");

            // Navega a la vista de gestión de reseñas
            IWebElement BtnReseña = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnResenna")));
            BtnReseña.Click();

            // Busca la reseña
            IWebElement InputReseña = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InputBuscar")));
            InputReseña.Clear();
            InputReseña.SendKeys(idReseña);

            IWebElement BtnBuscar = driver.FindElement(By.Id("btnBuscar"));
            BtnBuscar.Click();

            System.Threading.Thread.Sleep(1000);

            try
            {
                // XPath para buscar en la tabla por ID 25 y Cliente Marvin
                By selectorFilaID = By.XPath($"//table//tr[td[text()='{idReseña}'] and td[text()='Marvin']]");
                IWebElement filaReseña = wait.Until(ExpectedConditions.ElementIsVisible(selectorFilaID));

                Assert.IsTrue(filaReseña.Displayed, "Fallo: La reseña con ID 25 no es visible después de la búsqueda.");
                TestContext.WriteLine($"Éxito: La reseña con ID {idReseña} fue encontrada y verificada.");
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.WriteLine("Fallo: El URL actual es: " + driver.Url);
                Assert.Fail("Fallo de Búsqueda: La reseña con ID 25 no apareció en los resultados.");
            }
        }

        [TestMethod]
        public void EditarReseñaTest()
        {
            // Configuración de Espera Inteligente
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string idReseña = "25";

            // Clic en el botón 'Editar' (Asumimos que el clic ocurre exitosamente ahora)
            By selectorBotonVer = By.XPath($"//tr[td[text()='{idReseña}']]/td/button[text()='Editar']");
            IWebElement BtnEditar = wait.Until(ExpectedConditions.ElementToBeClickable(selectorBotonVer));
            BtnEditar.Click();

            IWebElement InputComentario = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InputComentarioEdit")));
            InputComentario.Clear();
            InputComentario.SendKeys("Excelente servicio, muy buena atención al cliente, reseña editada coño");

            IWebElement BtnGuardarCambios = driver.FindElement(By.Id("btnGuardarEdit"));
            BtnGuardarCambios.Click();

            System.Threading.Thread.Sleep(1000);

            try
            {
                // Espera hasta que la URL contenga "/resennas" para confirmar que la reseña fue editada exitosamente
                bool EditExitoso = wait.Until(ExpectedConditions.UrlContains("/resennas"));
                Assert.IsTrue(EditExitoso);
                TestContext.WriteLine("Éxito: La reseña fue editada exitosamente y se redirigió a la vista 'resennas'.");

            }
            catch(Exception ex)
            {
                Assert.Fail($"Fallo: Ocurrió una excepción inesperada al iniciar sesión: {ex.Message}");
            }
        }


        [TestMethod]
        public void EliminarReseñaTest()
        {
            // Configuración de Espera Inteligente
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string idReseña = "6";

            System.Threading.Thread.Sleep(1000);

            // Busca la reseña
            IWebElement InputReseña = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InputBuscar")));
            InputReseña.Clear();
            InputReseña.SendKeys(idReseña);

            IWebElement BtnBuscar = driver.FindElement(By.Id("btnBuscar"));
            BtnBuscar.Click();

            System.Threading.Thread.Sleep(1000);

            By selectorBotonVer = By.XPath($"//tr[td[text()='{idReseña}']]/td/button[text()='Eliminar']");
            IWebElement BtnEditar = wait.Until(ExpectedConditions.ElementToBeClickable(selectorBotonVer));
            BtnEditar.Click();

            System.Threading.Thread.Sleep(1000);

            IWebElement BtnGuardarCambios = driver.FindElement(By.Id("btnEliminarConfirmar"));
            BtnGuardarCambios.Click();

            System.Threading.Thread.Sleep(2000);

            try
            {
                // Espera hasta que la URL contenga "/resennas" para confirmar que la reseña fue eliminada exitosamente
                bool DeleteExitoso = wait.Until(ExpectedConditions.UrlContains("/resennas"));
                Assert.IsTrue(DeleteExitoso);
                TestContext.WriteLine("Éxito: La reseña fue eliminada exitosamente y se redirigió a la vista 'resennas'.");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Fallo: Ocurrió una excepción inesperada al eliminar la reseña: {ex.Message}");
            }



        }

        //[TestMethod]
        //public void VerReseñaTest()
        //{
        //    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        //    string idReseña = "25";
        //    string clienteReseña = "Marvin";

        //    // 3. ACCIÓN: Clic en el botón 'Ver' (Asumimos que el clic ocurre exitosamente ahora)
        //    By selectorBotonVer = By.XPath($"//tr[td[text()='{idReseña}']]/td/button[text()='Ver']");
        //    IWebElement BtnVer = wait.Until(ExpectedConditions.ElementToBeClickable(selectorBotonVer));
        //    BtnVer.Click();


        //}


        [TestMethod]
        public void probarTodo(){
            TestLoginAdmin();

            BuscarReseñaTest();

            EditarReseñaTest();

            EliminarReseñaTest();
        }

        [TestCleanup]
        public void Clear()
        {
            // Cierra el navegador y libera los recursos
            driver.Quit();
        }

    }
}
