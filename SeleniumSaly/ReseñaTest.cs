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
        private WebDriverWait wait;
        private const string AppUrl = "http://frontend-beautysaly.somee.com/";

        // Propiedad para el contexto de la prueba
        public TestContext TestContext { get; set; }

        // Inicializa el controlador del navegador
        [TestInitialize]
        public void Setup()
        {
            driver = new EdgeDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }

        [TestMethod]
        public void TestEjecutarTodo()
        {
            TestLoginCLiente();

            CrearReseñaTest();

            CerrarSesionTest();

            TestLoginAdmin();

            BuscarReseñaTest();

            EditarReseñaTest();

            EliminarReseñaTest();
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
                InputEmail.SendKeys("profemarvin@gmail.com");

                IWebElement InputPassword = driver.FindElement(By.Id("inputpassword"));
                InputPassword.Clear();
                InputPassword.SendKeys("12345");

                IWebElement BtnLogin = driver.FindElement(By.Id("btnIngresar"));
                BtnLogin.Click();

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
            try
            {
                // Configuración de Espera Inteligente


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
                InputComentario.SendKeys("Excelente servicio, muy buena atención al cliente,nuevas reseña otra coño");

                // Envía el formulario de reseña
                IWebElement BtnEnviar = driver.FindElement(By.Id("btnEnviar"));
                BtnEnviar.Click();

                System.Threading.Thread.Sleep(1000);


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
        public void CerrarSesionTest()
        {
            try
            {
                // Espera a que el botón de cerrar sesión sea clickeable
                IWebElement BtnCerrarSesion = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnCerrarSesion")));
                BtnCerrarSesion.Click();

                // Espera hasta que la URL contenga "/home/index" para confirmar que la sesión fue cerrada exitosamente
                bool CerrarSesionExitoso = wait.Until(ExpectedConditions.UrlContains("/"));
                Assert.IsTrue(CerrarSesionExitoso);
                TestContext.WriteLine("Éxito: La sesión fue cerrada exitosamente y se redirigió a la vista 'Login'.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada al cerrar sesión");
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
                IWebElement InputEmail = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputemail")));
                InputEmail.Clear();
                InputEmail.SendKeys("marvinadmin@gmail.com");

                IWebElement InputPassword = driver.FindElement(By.Id("inputpassword"));
                InputPassword.Clear();
                InputPassword.SendKeys("12345");

                IWebElement BtnLogin = driver.FindElement(By.Id("btnIngresar"));
                BtnLogin.Click();

                System.Threading.Thread.Sleep(1000);

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
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("document.body.style.zoom = '0.5'");

                // 1. NAVEGACIÓN A LA TABLA
                IWebElement BtnReseña = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnResenna")));
                BtnReseña.Click();

                By selectorPrimerID = By.XPath("//tbody/tr[1]/td[1]");
                IWebElement primerIDElemento = wait.Until(ExpectedConditions.ElementIsVisible(selectorPrimerID));

                // Captura el ID de la primera fila
                string idReseña = primerIDElemento.Text.Trim();
                Assert.IsFalse(string.IsNullOrEmpty(idReseña));

                TestContext.WriteLine($"ID capturado para búsqueda: {idReseña}");

                IWebElement InputReseña = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InputBuscar")));
                InputReseña.Clear();
                InputReseña.SendKeys(idReseña);

                // Clic en Buscar
                IWebElement BtnBuscar = driver.FindElement(By.Id("btnBuscar"));
                wait.Until(ExpectedConditions.ElementToBeClickable(BtnBuscar)).Click();

                // XPath para buscar la fila que contenga SOLO el ID capturado, asegurando que el filtro funcionó
                By selectorFilaFiltrada = By.XPath($"//table//tr[td[text()='{idReseña}']]");
                IWebElement filaReseña = wait.Until(ExpectedConditions.ElementIsVisible(selectorFilaFiltrada));

                Assert.IsTrue(filaReseña.Displayed);
                TestContext.WriteLine($"Éxito: La reseña con ID '{idReseña}' fue encontrada correctamente después de la búsqueda.");

            }
            catch (Exception ex)
            {
                TestContext.WriteLine("❌ Fallo: Ocurrió una excepción inesperada durante la búsqueda.");
                Assert.Fail(ex.Message);
            }
        }


        [TestMethod]
        public void EditarReseñaTest()
        {
            try
            {
                // Clic en el botón 'Editar' (Asumimos que el clic ocurre exitosamente ahora)
                By selectorBotonEditarPrimeraFila = By.XPath("//tbody/tr[1]/td/button[text()='Editar']");
                IWebElement BtnEditar = wait.Until(ExpectedConditions.ElementToBeClickable(selectorBotonEditarPrimeraFila));
                BtnEditar.Click();

                IWebElement InputComentario = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InputComentarioEdit")));
                InputComentario.Clear();
                InputComentario.SendKeys("Excelente servicio, muy buena atención al cliente, reseña editada otra vez");

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
            System.Threading.Thread.Sleep(2000);
            try
            {
                System.Threading.Thread.Sleep(1000);

                // Clic en el botón 'Editar' (Asumimos que el clic ocurre exitosamente ahora)
                By selectorBotonEditarPrimeraFila = By.XPath("//tbody/tr[1]/td/button[text()='Eliminar']");
                IWebElement BtnEliminarReseña = wait.Until(ExpectedConditions.ElementToBeClickable(selectorBotonEditarPrimeraFila));
                BtnEliminarReseña.Click();

                System.Threading.Thread.Sleep(1000);

                IWebElement BtnGuardarCambios = driver.FindElement(By.Id("btnEliminarConfirmar"));
                BtnGuardarCambios.Click();

                System.Threading.Thread.Sleep(2000);

                // Espera hasta que la URL contenga "/resennas" para confirmar que la reseña fue eliminada exitosamente
                bool DeleteExitoso = wait.Until(ExpectedConditions.UrlContains("/resennas"));
                Assert.IsTrue(DeleteExitoso);
                TestContext.WriteLine("Éxito: La reseña fue eliminada exitosamente y se redirigió a la vista 'resennas'.");
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
