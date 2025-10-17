using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace SeleniumSaly
{
    [TestClass]
    public class ReseñaTestIndi
    {
        private IWebDriver driver;
        private const string AppUrl = "http://frontend-beautysaly.somee.com/";
        private const string idReseñaEdit = "30"; // ID de la reseña que se va a buscar y editar

        // Propiedad para el contexto de la prueba
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            driver = new EdgeDriver();

        }

        [TestMethod]
        public void TestLoginCLiente()
        {
            try
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

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                bool LoginExitoso = wait.Until(ExpectedConditions.UrlContains("/sobrenosotros"));

                Assert.IsTrue(LoginExitoso);

                TestContext.WriteLine("Éxito: El inicio de sesión como cliente fue exitoso y se redirigió a la vista 'Sobre Nosotros'.");

            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada al iniciar sesión");
                Assert.Fail(ex.Message);

            }
        }

        [TestMethod]
        public void CrearReseñaTest()
        {
            TestLoginCLiente(); // Asegura que el usuario esté logueado antes de crear una reseña
            try
            {
                // Configuración de Espera Inteligente
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

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
                InputComentario.SendKeys("Excelente servicio, muy buena atención al cliente, otra reseña XD");

                // Envía el formulario de reseña
                IWebElement BtnEnviar = driver.FindElement(By.Id("btnEnviar"));
                BtnEnviar.Click();

                // Espera hasta que la URL contenga "/sobrenosotros" para confirmar que la reseña fue enviada exitosamente
                bool ReseñaExitosa = wait.Until(ExpectedConditions.UrlContains("/sobrenosotros"));

                // Assert: Verifica la condición
                Assert.IsTrue(ReseñaExitosa);

                // Mensaje de Éxito
                TestContext.WriteLine("Éxito: La reseña fue creada y enviada exitosamente por el cliente y se redirigió a la vista 'Sobre Nosotros'.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada durante la creacion de la reseña");
                Assert.Fail(ex.Message);

            }

        }

        [TestMethod]
        public void TestLoginAdmin()
        {
            try
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

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                bool LoginExitoso = wait.Until(ExpectedConditions.UrlContains("/sobrenosotros"));

                Assert.IsTrue(LoginExitoso);

                TestContext.WriteLine("Éxito: El inicio de sesión fue exitoso y se redirigió a la vista 'Sobre Nosotros'.");

            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada al iniciar sesión como Admin");
                Assert.Fail(ex.Message);

            }
        }

        [TestMethod]
        public void BuscarReseñaTest()
        {
            TestLoginAdmin(); // Asegura que el usuario esté logueado como Admin antes de buscar una reseña
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Convertir el driver a IJavaScriptExecutor
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                // Disminuir el zoom al 50% para alejar la página
                js.ExecuteScript("document.body.style.zoom = '0.5'");

                // Navega a la vista de gestión de reseñas
                IWebElement BtnReseña = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnResenna")));
                BtnReseña.Click();

                // Busca la reseña
                IWebElement InputReseña = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InputBuscar")));
                InputReseña.Clear();
                InputReseña.SendKeys(idReseñaEdit);

                IWebElement BtnBuscar = driver.FindElement(By.Id("btnBuscar"));
                BtnBuscar.Click();

                System.Threading.Thread.Sleep(1000);

                // XPath para buscar en la tabla por ID 25 y Cliente Marvin
                By selectorFilaID = By.XPath($"//table//tr[td[text()='{idReseñaEdit}'] and td[text()='Marvin']]");
                IWebElement filaReseña = wait.Until(ExpectedConditions.ElementIsVisible(selectorFilaID));

                Assert.IsTrue(filaReseña.Displayed);
                TestContext.WriteLine($"Éxito: La reseña con ID {idReseñaEdit} fue encontrada.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Fallo de Búsqueda: La reseña con ID 25 no apareció en los resultados");
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void EditarReseñaTest()
        {
            BuscarReseñaTest(); // Asegura que la reseña a editar esté visible en la tabla
            try
            {
                // Configuración de Espera Inteligente
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Clic en el botón 'Editar' (Asumimos que el clic ocurre exitosamente ahora)
                By selectorBotonVer = By.XPath($"//tr[td[text()='{idReseñaEdit}']]/td/button[text()='Editar']");
                IWebElement BtnEditar = wait.Until(ExpectedConditions.ElementToBeClickable(selectorBotonVer));
                BtnEditar.Click();

                IWebElement InputComentario = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InputComentarioEdit")));
                InputComentario.Clear();
                InputComentario.SendKeys("Excelente servicio, muy buena atención al cliente, reseña editada quinta vez");

                IWebElement BtnGuardarCambios = driver.FindElement(By.Id("btnGuardarEdit"));
                BtnGuardarCambios.Click();

                System.Threading.Thread.Sleep(1000);

                // Espera hasta que la URL contenga "/resennas" para confirmar que la reseña fue editada exitosamente
                bool EditExitoso = wait.Until(ExpectedConditions.UrlContains("/resennas"));
                Assert.IsTrue(EditExitoso);
                TestContext.WriteLine("Éxito: La reseña fue editada exitosamente y se redirigió a la vista 'resennas'.");

            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada al editar la reseña");
                Assert.Fail(ex.Message);

            }
        }


        [TestMethod]
        public void EliminarReseñaTest()
        {
            BuscarReseñaTest();
            try
            {
                // Configuración de Espera Inteligente
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                By selectorBotonVer = By.XPath($"//tr[td[text()='{idReseñaEdit}']]/td/button[text()='Eliminar']");
                IWebElement BtnEditar = wait.Until(ExpectedConditions.ElementToBeClickable(selectorBotonVer));
                BtnEditar.Click();

                System.Threading.Thread.Sleep(1000);

                IWebElement BtnGuardarCambios = driver.FindElement(By.Id("btnEliminarConfirmar"));
                BtnGuardarCambios.Click();

                System.Threading.Thread.Sleep(100);

                // Espera hasta que la URL contenga "/resennas" para confirmar que la reseña fue eliminada exitosamente
                bool DeleteExitoso = wait.Until(ExpectedConditions.UrlContains("/resennas"));
                Assert.IsTrue(DeleteExitoso);

                TestContext.WriteLine($"Éxito: La reseña {idReseñaEdit} fue eliminada exitosamente y se redirigió a la vista 'resennas'.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada al eliminar la reseña");
                Assert.Fail(ex.Message);
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
