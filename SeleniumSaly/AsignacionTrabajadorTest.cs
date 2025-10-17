using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumSaly
{
    [TestClass]
    public class AsignacionTrabajadorTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private const string AppUrl = "http://frontend-beautysaly.somee.com/";

        // Propiedad para el contexto de la prueba
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            driver = new EdgeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }

        [TestMethod]
        public void EjecutarTodoTest()
        {
            TestLoginTrabajador();
            CrearAsignacionServicioTrabajadorTest();
            CerrarSesionTest();
            TestLoginAdmin();
            BuscarAsignacionTrabajadorTest();
            EditarAsignacionTest();
            EliminarAsignacionTest();
        }

        [TestMethod]
        public void TestLoginTrabajador()
        {
            try
            {
                //Abre el navegador y navega a la URL de la aplicación
                driver.Navigate().GoToUrl(AppUrl);

                System.Threading.Thread.Sleep(2000);

                //Agrega las credenciales para poder iniciar sesión y acceder a la aplicación
                IWebElement InputEmail = driver.FindElement(By.Id("inputemail"));
                InputEmail.Clear();
                InputEmail.SendKeys("trabajadorroci@gmail.com");

                IWebElement InputPassword = driver.FindElement(By.Id("inputpassword"));
                InputPassword.Clear();
                InputPassword.SendKeys("12345");

                IWebElement BtnLogin = driver.FindElement(By.Id("btnIngresar"));
                BtnLogin.Click();

                bool LoginExitoso = wait.Until(ExpectedConditions.UrlContains("/sobrenosotros"));
                Assert.IsTrue(LoginExitoso);
                TestContext.WriteLine("Éxito: El inicio de sesión como trabajador fue exitoso y se redirigió a la vista 'Sobre Nosotros'.");

            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada al iniciar sesión");
                Assert.Fail(ex.Message);

            }
        }

        [TestMethod]
        public void CrearAsignacionServicioTrabajadorTest()
        {
            System.Threading.Thread.Sleep(2000);
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("document.body.style.zoom = '0.5'");

                // Navega a la vista de gestión de asignaciones de trabajadores
                IWebElement BtnServiceTrabajador = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnTrabajador")));
                BtnServiceTrabajador.Click();

                IWebElement btnCrearNuevo = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnCrearAsignacion")));
                btnCrearNuevo.Click();

                System.Threading.Thread.Sleep(1000);

                // Seleccionar una opción aleatoria del ComboBox
                By selectorComboBox = By.Id("btnCombobox");
                wait.Until(ExpectedConditions.ElementIsVisible(selectorComboBox));

                // Ejecuta la selección aleatoria con la nueva función
                string nuevoServicio = SeleccionarServicio(driver, wait, selectorComboBox);

                System.Threading.Thread.Sleep(1000);

                IWebElement btnCrearNuevoAsignacion = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnGuardarAsignacion")));
                btnCrearNuevoAsignacion.Click();

                System.Threading.Thread.Sleep(1000);

                bool AsignacionExitosa = wait.Until(ExpectedConditions.UrlContains("/servicio-trabajador"));
                Assert.IsTrue(AsignacionExitosa);
                TestContext.WriteLine("Éxito: Se Asigno nuevo Servicio al Trabajador y se redirigió a la vista 'servicio-trabajador'.");

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada al crear nueva Asignacion al Trabajador");

            }
        }

        [TestMethod]
        public void CerrarSesionTest()
        {
            System.Threading.Thread.Sleep(2000);
            try
            {
                IWebElement BtnCerrarSesion = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnCerrarSesion")));
                BtnCerrarSesion.Click();

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

                System.Threading.Thread.Sleep(2000);

                //Agrega las credenciales para poder iniciar sesión y acceder como Admin a la aplicación
                IWebElement InputEmail = driver.FindElement(By.Id("inputemail"));
                InputEmail.Clear();
                InputEmail.SendKeys("marvinadmin@gmail.com");

                IWebElement InputPassword = driver.FindElement(By.Id("inputpassword"));
                InputPassword.Clear();
                InputPassword.SendKeys("12345");

                IWebElement BtnLogin = driver.FindElement(By.Id("btnIngresar"));
                BtnLogin.Click();

                bool LoginExitoso = wait.Until(ExpectedConditions.UrlContains("/sobrenosotros"));
                Assert.IsTrue(LoginExitoso);
                TestContext.WriteLine("Éxito: El inicio de sesión como trabajador fue exitoso y se redirigió a la vista 'Sobre Nosotros'.");

            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada al iniciar sesión");
                Assert.Fail(ex.Message);

            }
        }

        [TestMethod]
        public void BuscarAsignacionTrabajadorTest()
        {
            System.Threading.Thread.Sleep(2000);
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("document.body.style.zoom = '0.5'");

                // navega a la vista de gestión de asignaciones de trabajadores
                IWebElement BtnAsignacionTrabajador = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnAsignacion")));
                BtnAsignacionTrabajador.Click();

                System.Threading.Thread.Sleep(2000);

                By selectorPrimerID = By.XPath("//tbody/tr[1]/td[1]");
                IWebElement primerIDElemento = wait.Until(ExpectedConditions.ElementIsVisible(selectorPrimerID));

                // Captura el ID de la primera fila
                string idAsingnacion = primerIDElemento.Text.Trim();
                Assert.IsFalse(string.IsNullOrEmpty(idAsingnacion));

                TestContext.WriteLine($"ID capturado para búsqueda: {idAsingnacion}");

                IWebElement InputBuscarAsignacion = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inputBuscarAsingId")));
                InputBuscarAsignacion.Clear();
                InputBuscarAsignacion.SendKeys(idAsingnacion);

                // Clic en Buscar
                IWebElement BtnBuscarAsignacion = driver.FindElement(By.Id("btnBuscarAsingId"));
                wait.Until(ExpectedConditions.ElementToBeClickable(BtnBuscarAsignacion)).Click();

                // Verifica que la tabla muestre solo la fila con el ID buscado
                By selectorFilaFiltrada = By.XPath($"//table//tr[td[text()='{idAsingnacion}']]");
                IWebElement filaReseña = wait.Until(ExpectedConditions.ElementIsVisible(selectorFilaFiltrada));

                Assert.IsTrue(filaReseña.Displayed);
                TestContext.WriteLine($"Éxito: La Asingacion Trabajador con ID '{idAsingnacion}' fue encontrada correctamente después de la búsqueda.");

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada al buscar la asignación del trabajador");
            }
        }

        [TestMethod]
        public void EditarAsignacionTest()
        {
            System.Threading.Thread.Sleep(1000);
            try
            {
                // Clic en el botón 'Editar'
                IWebElement BtnEditarAsignacion = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnEditarAsign")));
                BtnEditarAsignacion.Click();

                System.Threading.Thread.Sleep(1000);

                // Seleccionar una opción aleatoria del ComboBox
                By selectorComboBox = By.Id("selectServicio");
                wait.Until(ExpectedConditions.ElementIsVisible(selectorComboBox));

                // Ejecuta la selección aleatoria con la nueva función
                string nuevoServicio = SeleccionarServicio(driver, wait, selectorComboBox);

                System.Threading.Thread.Sleep(1000);

                // Guardar la nueva asignacion editada
                IWebElement BtnComfirmarEdicion = driver.FindElement(By.Id("btnActualizarAsign"));
                BtnComfirmarEdicion.Click();

                System.Threading.Thread.Sleep(1000);

                // Espera hasta que la URL contenga "/listaServiTrabajadorAdmin" para confirmar que la asignacion fue editada exitosamente
                bool DeleteExitoso = wait.Until(ExpectedConditions.UrlContains("/listaServiTrabajadorAdmin"));
                Assert.IsTrue(DeleteExitoso);
                TestContext.WriteLine("Éxito: La Asignacion Trabjador fue editada exitosamente y se redirigió a la vista 'listaServiTrabajadorAdmin'.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada al editar la Asignacion Trabajador");
                Assert.Fail(ex.Message);
            }
        }

        private string SeleccionarServicio(IWebDriver driver, WebDriverWait wait, By selectorComboBox)
        {
            IWebElement comboBoxElement = wait.Until(ExpectedConditions.ElementIsVisible(selectorComboBox));

            IList<IWebElement> todasLasOpciones = comboBoxElement.FindElements(By.TagName("option"));

            IList<IWebElement> opcionesValidas = todasLasOpciones
                .Where(opt => !opt.Text.StartsWith("--") && !string.IsNullOrEmpty(opt.Text.Trim()))
                .ToList();

            if (opcionesValidas.Count == 0)
            {
                throw new Exception("No se encontraron opciones válidas (no-placeholder) para seleccionar en el ComboBox.");
            }

            Random random = new Random();
            int indiceAleatorio = random.Next(opcionesValidas.Count);

            IWebElement opcionSeleccionada = opcionesValidas[indiceAleatorio];
            string textoSeleccionado = opcionSeleccionada.Text.Trim();

            opcionSeleccionada.Click();
            return textoSeleccionado;
        }

        [TestMethod]
        public void EliminarAsignacionTest()
        {
            System.Threading.Thread.Sleep(1000);
            try
            {
                // Clic en el botón 'Eliminar'
                IWebElement BtnEliminarAsignacion = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnEliminarAsign")));
                BtnEliminarAsignacion.Click();

                System.Threading.Thread.Sleep(1000);

                IWebElement BtnComfirmarEliminar = driver.FindElement(By.Id("btneliminarAsignacion"));
                BtnComfirmarEliminar.Click();

                System.Threading.Thread.Sleep(1000);

                // Espera hasta que la URL contenga "/listaServiTrabajadorAdmin" para confirmar que la asignacion fue eliminada exitosamente
                bool DeleteExitoso = wait.Until(ExpectedConditions.UrlContains("/listaServiTrabajadorAdmin"));
                Assert.IsTrue(DeleteExitoso);
                TestContext.WriteLine("Éxito: La Asignacion Trabjador fue eliminada exitosamente y se redirigió a la vista 'listaServiTrabajadorAdmin'.");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Fallo: Ocurrió una excepción inesperada al eliminar la Asignacion Trabajador");
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
