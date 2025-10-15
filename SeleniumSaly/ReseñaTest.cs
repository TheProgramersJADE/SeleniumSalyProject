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
        public void TestLoginCLiente()
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
        public void CrearReseñaTest()
        {
            // Configuración de Espera Inteligente
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // Iniciar sesión primero
            TestLoginAdmin();

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

        [TestMethod]
        public void buscarReseña() 
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
            IWebElement BtnReseña = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnResenna")));
            BtnReseña.Click();

            // Buscar la reseña creada
            IWebElement InputReseña = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InputBuscar")));
            InputReseña.Clear();
            InputReseña.SendKeys("25");

            IWebElement BtnBuscar = driver.FindElement(By.Id("btnBuscar")); 
            BtnBuscar.Click();

            try
            {
                // 5.1. Verificar que el ID '25' es visible en la primera columna de resultados.
                // Usamos un XPath que busque el texto exacto '25' dentro de la columna ID (o en la fila).
                string idReseña = "25";

                // 📢 XPath para encontrar la fila que contiene el ID 25
                By selectorFilaID = By.XPath($"//table//tr[td[text()='{idReseña}'] and td[text()='Marvin']]");


                IWebElement filaReseña = wait.Until(ExpectedConditions.ElementIsVisible(selectorFilaID));

                // 5.2. Verificar un segundo campo (ej. Comentario) para confirmar que es la reseña correcta.
                // XPath para encontrar el comentario 'Excelente servicio...' dentro de la misma fila.
                By selectorComentario = By.XPath($"//table//tr[td[text()='{idReseña}']]/td[contains(text(), 'servicio, muy buena')]");
                IWebElement comentarioVerificado = wait.Until(ExpectedConditions.ElementIsVisible(selectorComentario));


                // ASSERT Final
                Assert.IsTrue(filaReseña.Displayed, "Fallo: La reseña con ID 25 fue encontrada, pero el contenido no se verificó correctamente.");
                TestContext.WriteLine($"Éxito: La reseña con ID {idReseña} fue encontrada y sus detalles son correctos.");
            }
            catch (WebDriverTimeoutException)
            {
                // Si el elemento no aparece en 10s, la búsqueda falló.
                TestContext.WriteLine("Fallo: El URL actual es: " + driver.Url);
                Assert.Fail("Fallo de Búsqueda: La reseña con ID 25 no apareció en los resultados de la tabla en el tiempo límite.");
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
