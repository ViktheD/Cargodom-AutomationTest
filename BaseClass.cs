using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Expect = SeleniumExtras.WaitHelpers.ExpectedConditions;
using System.Diagnostics;
using OpenQA.Selenium.DevTools.V108.Page;
using System.Threading;

namespace CargodomProject
{
    public class BaseClass
    {
        public IWebDriver Driver;
        public WebDriverWait wait;
        public static Random random = new Random();

        public const string homeURL = "http://18.156.17.83:9095/";
        public const string userURL = "http://18.156.17.83:9095/client/home";
        public const string transporterURL = "http://18.156.17.83:9095/provider/home";
        public const string createRequestUrl = "http://18.156.17.83:9095/client/create-request";
        public const string moiPonudiTransporterUrl = "http://18.156.17.83:9095/provider/my-offers/active";
        public const string moiBaranjaUserUrl = "http://18.156.17.83:9095/client/my-requests/active";
        public const string prifateniPonudiUrl = "http://18.156.17.83:9095/client/accepted-offers/list";
        public const string loginSubmitButtonCssLocator = "button[translate = 'login.form.button']";
        public const string expectedLoginMessageCssLocator = "Добредојдовте на cargodom.com";
        public const string expectedMessageDobredojdovte = "ДОБРЕДОЈДОВТЕ!";
        public const string loginMessageCssLocator = "h2[translate = 'provider.welcomeMessage']";
        public const string logOutButtonLocatorID = "logout2";
        public const string emptyClickCssLocatorAtProviderHome = "div[class='main__content']";
        public const string offerName = "Potatos and beans";
        public string randomEmailGenerator = RandomGenerateLetters(10) + "@gmail.com";
        public string randomPasswordGenerator = RandomGenerateSymbolswithCharAndInt(15);
        public string randomIme = RandomGenerateLetters(8);
        public string randomPrezime = RandomGenerateLetters(8);
        public string randomImeNaFirma = RandomGenerateLetters(8);
        public string randomAdresa = RandomGenerateLettersAndInt(10);
        public string randomGrad = RandomGenerateLetters(9);
        public string danokBroj = RandomGenerateNumbers(7);
        public string telNo = RandomGenerateNumbers(8);
         


        [OneTimeSetUp]
        public void OneTimeSetUp() 
        {
        }


        [SetUp] 
        public void Setup() 
        {
            Driver = new ChromeDriver();
            wait = new WebDriverWait(Driver,TimeSpan.FromSeconds(30));

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            Driver.Manage().Window.Maximize();

            Driver.Navigate().GoToUrl("http://18.156.17.83:9095/");
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Close();
            Driver.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }





        public static string RandomGenerateLetters(int lenght)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrsqtuvwxyz";
            return new string(Enumerable.Repeat(letters, lenght).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomGenerateNumbers(int lenght)
        {
            const string numbers = "123456789";
            return new string(Enumerable.Repeat(numbers, lenght).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomGenerateSymbolswithCharAndInt(int lenght)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrsqtuvwxyz0123456789!@#$%^&*(_-";
            return new string(Enumerable.Repeat(characters, lenght).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomGenerateLettersAndInt(int lenght)
        {
            const string lettersAndNum = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrsqtuvwxyz123456789";
            return new string(Enumerable.Repeat(lettersAndNum, lenght).Select(s => s[random.Next(s.Length)]).ToArray());
        }


      


        ///<summary> 
        ///Ovaa metoda e za Logiranje na Cargodom kako user
        /// </summary>
        public void LogMeIn()
        {
            IWebElement signInButton = Driver.FindElement(By.Id("login"));
            signInButton.Click();

            IWebElement userNameInput = Driver.FindElement(By.Id("username"));
            userNameInput.Clear();
            userNameInput.SendKeys("viktoreve_eve@hotmail.com");

            IWebElement passwordInput = Driver.FindElement(By.Id("password"));
            passwordInput.Clear();
            passwordInput.SendKeys("Tamara123");

            IWebElement rememberMeCheckBox = Driver.FindElement(By.Id("rememberMe"));
            rememberMeCheckBox.Click();

            IWebElement loginSubmitButton = Driver.FindElement(By.CssSelector(loginSubmitButtonCssLocator));
            loginSubmitButton.Click();

            wait.Until(Expect.UrlToBe(userURL));
        }


        ///<summary>
        ///Ovaa metoda e za Odjavuvanje na Cargodom
        /// </summary>
        public void LogMeOut()
        {
            IWebElement logOutButton = Driver.FindElement(By.Id("logout2"));
            Assert.IsTrue(logOutButton.Displayed);

            IWebElement signOutButton = Driver.FindElement(By.Id("logout2"));
            signOutButton.Click();
            wait.Until(Expect.UrlToBe(homeURL));

            Assert.AreEqual(homeURL, Driver.Url);
        }

        
        ///<summary>    
        ///Ovaa metoda e za Registracija Jas sum Transporter na Cargodom
        /// </summary>
        public void RegisterAsIamTransporter()
        {
            IWebElement registracija = Driver.FindElement(By.CssSelector("a[ui-sref='register']"));
            registracija.Click();

            IWebElement jasSumTransporterButton = Driver.FindElement(By.CssSelector("button[class='btn btn-green account-type__button']"));
            jasSumTransporterButton.Click();

            
            IWebElement imeInputField = Driver.FindElement(By.CssSelector("input[ng-model='vm.providerPerson.user.firstName']"));
            imeInputField.SendKeys(randomIme);

            
            IWebElement prezimeInputField = Driver.FindElement(By.CssSelector("input[ng-model='vm.providerPerson.user.lastName']"));
            prezimeInputField.SendKeys(randomPrezime);

            
            IWebElement imeNaFirmaInputField = Driver.FindElement(By.CssSelector("input[ng-model='vm.providerPerson.providerCompany.name']"));
            imeNaFirmaInputField.SendKeys(randomImeNaFirma);

            
            IWebElement adresaInputField = Driver.FindElement(By.CssSelector("input[ng-model='vm.providerPerson.providerCompany.address.address']"));
            adresaInputField.SendKeys(randomAdresa);

            
            IWebElement gradInputField = Driver.FindElement(By.CssSelector("input[ng-model='vm.providerPerson.providerCompany.address.city']"));
            gradInputField.SendKeys(randomGrad);

            string randomPost = RandomGenerateNumbers(6);
            IWebElement poshtenskiKodInputField = Driver.FindElement(By.CssSelector("input[ng-model='vm.providerPerson.providerCompany.address.postalCode']"));
            poshtenskiKodInputField.SendKeys(randomPost);


            IWebElement drzavaDropDownMenu = Driver.FindElement(By.ClassName("ui-select-match"));
            drzavaDropDownMenu.Click();

            wait.Until(Expect.ElementIsVisible(By.Id("ui-select-choices-row-0-0")));

            IWebElement macedoniaButton = Driver.FindElement(By.Id("ui-select-choices-row-0-0"));
            macedoniaButton.Click();

            wait.Until(Expect.ElementExists(By.Name("companyTaxNumber")));

            
            IWebElement danocenBrojInputField = Driver.FindElement(By.Name("companyTaxNumber"));
            danocenBrojInputField.SendKeys(danokBroj);

            
            IWebElement telefonskiBrojInputField = Driver.FindElement(By.Name("phoneNumber"));
            telefonskiBrojInputField.SendKeys(telNo);


            IWebElement emailInputField = Driver.FindElement(By.Name("email"));
            emailInputField.SendKeys(randomEmailGenerator);

            IWebElement passwordInputField = Driver.FindElement(By.Id("password"));
            passwordInputField.SendKeys(randomPasswordGenerator);

            IWebElement confirmPasswordInputField = Driver.FindElement(By.Id("confirmPassword"));
            confirmPasswordInputField.SendKeys(randomPasswordGenerator);

            IWebElement prifakjamCheckButton = Driver.FindElement(By.Id("acceptTerms"));
            prifakjamCheckButton.Click();

            IWebElement registrirajseButton = Driver.FindElement(By.CssSelector($"body > div.main > div.main-wrapper > div > div > div.col-sm-10.col-sm-offset-1 > form > div:nth-child(10) > input"));
            registrirajseButton.Click();

            string expectedUrl = ("http://18.156.17.83:9095/account-type/register-provider/provider-successful-registration");

            wait.Until(Expect.UrlToBe(expectedUrl));

            Assert.AreEqual(expectedUrl, Driver.Url, "URL is not the same");

        }


        ///<summary>
        ///Ovaa metoda e za Registracija baram transporter (fizicko lice) na Cargodon
        /// </summary>
        public void RegisterAsBaramTransporter()
        {
            IWebElement registracija = Driver.FindElement(By.CssSelector("a[ui-sref='register']"));
            registracija.Click();

            IWebElement baramTransporterButton = Driver.FindElement(By.CssSelector("button[ui-sref='register-client']"));
            baramTransporterButton.Click();

            IWebElement imeInputField = Driver.FindElement(By.Id("firstName"));
            imeInputField.SendKeys(RandomGenerateLetters(6));

            IWebElement prezimeInputField = Driver.FindElement(By.Name("lastName"));
            prezimeInputField.SendKeys(RandomGenerateLetters(9));

            IWebElement adresaInputField = Driver.FindElement(By.Name("clientPersonAddress"));
            adresaInputField.SendKeys(RandomGenerateLettersAndInt(8));

            IWebElement gradInputField = Driver.FindElement(By.Name("clientPersonCity"));
            gradInputField.SendKeys(RandomGenerateLetters(8));

            IWebElement postenskiInputField = Driver.FindElement(By.Name("clientPersonPostalCode"));
            postenskiInputField.SendKeys(RandomGenerateNumbers(7));




            IWebElement countryDropDownMenyButton = Driver.FindElement(By.CssSelector("span[ng-click='$select.activate()']"));
            countryDropDownMenyButton.Click();

            List<IWebElement> listaNaDrzavi = Driver.FindElements(By.CssSelector("div[ng-attr-id='ui-select-choices-row-{{ $select.generatedId }}-{{$index}}']")).ToList();

            int countryCount = listaNaDrzavi.Count();

            listaNaDrzavi[random.Next(0, countryCount)].Click();



            IWebElement telefonInputField = Driver.FindElement(By.Name("phoneNumber"));
            telefonInputField.SendKeys(RandomGenerateNumbers(9));

            IWebElement emailaddInputField = Driver.FindElement(By.CssSelector("input[ng-model='vm.clientPerson.user.email']"));
            emailaddInputField.SendKeys(randomEmailGenerator);

            IWebElement passInputField = Driver.FindElement(By.CssSelector("input[ng-model='vm.clientPerson.user.password']"));
            passInputField.SendKeys(randomPasswordGenerator);

            IWebElement confirmPassInputField = Driver.FindElement(By.CssSelector("input[ng-model='vm.confirmPassword']"));
            confirmPassInputField.SendKeys(randomPasswordGenerator);

            IWebElement prifakjamCheckBox = Driver.FindElement(By.Id("acceptTerms"));
            prifakjamCheckBox.Click();

            IWebElement regDoneButton = Driver.FindElement(By.CssSelector("input[ng-click='form.$valid && vm.register()']"));
            regDoneButton.Click();

            wait.Until(Expect.ElementIsVisible(By.ClassName("successful-registration__main-title")));
            IWebElement txtDobredojde = Driver.FindElement(By.ClassName("successful-registration__main-title"));

            Assert.AreEqual(expectedMessageDobredojdovte, txtDobredojde.Text);
        }




        ///<summary>
        ///Ovaa metoda e za da Objavish Baranje kako user
        /// </summary>
        public void ObjaviBaranje()
        {
            IWebElement objaviBaranjeButton = Driver.FindElement(By.CssSelector("a[ui-sref='client-create-request']"));
            objaviBaranjeButton.Click();

            IWebElement naslovNaBaraneInputField = Driver.FindElement(By.CssSelector("input[ng-model='vm.request.title']"));
            naslovNaBaraneInputField.SendKeys(offerName); //ime na baranje deklarirano

            IWebElement kategorijaDropDown = Driver.FindElement(By.CssSelector("input[ng-model='vm.request.title']"));
            kategorijaDropDown.Click();

            wait.Until(Expect.ElementIsVisible(By.CssSelector("option[translate='cargoApp.CategoryType.MOTORBIKE']")));
            IWebElement motorcikleSelect = Driver.FindElement(By.CssSelector("option[translate='cargoApp.CategoryType.MOTORBIKE']"));
            motorcikleSelect.Click();

            IWebElement pickUpAddress = Driver.FindElement(By.Name("pickUpAddress"));
            IWebElement adress = pickUpAddress.FindElement(By.CssSelector("input[ng-value='vm.address.formattedAddress']"));
            adress.SendKeys("Ukraine");
            List<IWebElement> autoPickUpAddress = Driver.FindElements(By.CssSelector("span[class='pac-matched']")).ToList();
            autoPickUpAddress[0].Click();

            wait.Until(Expect.ElementIsVisible(By.Name("deliveryAddress")));

            IWebElement toAddress = Driver.FindElement(By.Name("deliveryAddress"));
            IWebElement address = toAddress.FindElement(By.CssSelector("input[ng-value='vm.address.formattedAddress']"));
            address.SendKeys("Bitola, North Macedonia");
            List<IWebElement> autoPickUpAddress2 = Driver.FindElements(By.CssSelector("span[class='pac-matched']")).ToList();
            autoPickUpAddress2[0].Click();



            IWebElement removeButton = Driver.FindElement(By.CssSelector("a[ng-click='vm.removeDimension(dimension);']"));
            removeButton.Click();

            IWebElement secondCheckBox = Driver.FindElement(By.Id("cacheDelivery"));
            secondCheckBox.Click();

            wait.Until(Expect.ElementToBeClickable(By.CssSelector("input[class='btn btn-green center-block']")));           
            IWebElement objaviBaranje = Driver.FindElement(By.CssSelector("input[class='btn btn-green center-block']"));
            objaviBaranje.Click();
            objaviBaranje.Click();
            


            wait.Until(Expect.ElementIsVisible(By.CssSelector("pre[ng-bind-html='alert.msg']")));
            IWebElement uspesnoKreiranoBaranje = Driver.FindElement(By.CssSelector("pre[ng-bind-html='alert.msg']"));

            string expektiranTekst = "Успешно е креирано ново барање";
            Assert.AreEqual(expektiranTekst, uspesnoKreiranoBaranje.Text);

            IWebElement myRequestButton = Driver.FindElement(By.CssSelector("li[class='tabs-wrapper__item active']"));
            myRequestButton.Click();

            IWebElement moiBaranjaTable = Driver.FindElement(By.ClassName("table-body"));
            IWebElement rowMoiBaranjaTable = moiBaranjaTable.FindElement(By.ClassName("table-body__row"));
            IWebElement colomnMoiBaranjaTable = rowMoiBaranjaTable.FindElement(By.CssSelector("td[class='table-body__cell column1']"));
            IWebElement elementMoiBaranjaTable = colomnMoiBaranjaTable.FindElement(By.TagName("a"));
            elementMoiBaranjaTable.Click();

            IWebElement productName = Driver.FindElement(By.CssSelector("div[class='details-panel__content-wrapper-row']"));

            string expectedProductName = offerName; 

            Assert.AreEqual(expectedProductName, productName.Text);


        }



        /// <summary>
        /// Ovaa metoda e za Logiranje kako Transporter
        /// </summary>
        public void LogMeInTransporter()
        {
            IWebElement signInButton = Driver.FindElement(By.Id("login"));
            signInButton.Click();

            IWebElement userNameInput = Driver.FindElement(By.Id("username"));
            userNameInput.Clear();
            userNameInput.SendKeys("viktorbt@gmail.com");

            IWebElement passwordInput = Driver.FindElement(By.Id("password"));
            passwordInput.Clear();
            passwordInput.SendKeys("Tamara123!");

            IWebElement rememberMeCheckBox = Driver.FindElement(By.Id("rememberMe"));
            rememberMeCheckBox.Click();

            IWebElement loginSubmitButton = Driver.FindElement(By.CssSelector(loginSubmitButtonCssLocator));
            loginSubmitButton.Click();

            wait.Until(Expect.UrlToBe(transporterURL));
            Assert.AreEqual(transporterURL, Driver.Url);
        }




        /// <summary>
        /// Ovaa metoda e za da ja najdeme ponudata i da ja prifatime
        /// </summary>
        public void findRequestAndMakeOffer ()
        {
           Driver.Navigate().Refresh();

            wait.Until(Expect.UrlToBe(transporterURL));
            IWebElement odDrzavaSearchButton = Driver.FindElement(By.CssSelector("span[class='btn btn-default form-control ui-select-toggle']"));
            odDrzavaSearchButton.Click();

            List<IWebElement> UkraineDrzava = Driver.FindElements(By.CssSelector("div[ng-attr-id='ui-select-choices-row-{{ $select.generatedId }}-{{$index}}']")).ToList();
            UkraineDrzava[69].Click();


            IWebElement emptyClick = Driver.FindElement(By.CssSelector(emptyClickCssLocatorAtProviderHome));
            emptyClick.Click();
            wait.Until(Expect.ElementToBeClickable(By.CssSelector("a[ng-click='vm.clickSearch()']")));
            IWebElement prebaruvajButton = Driver.FindElement(By.CssSelector("a[ng-click='vm.clickSearch()']"));
            prebaruvajButton.Click();

            Driver.Navigate().Refresh();
            wait.Until(Expect.ElementIsVisible(By.ClassName("table-body")));
            
            IWebElement tableActiveRequests = Driver.FindElement(By.ClassName("table-body"));
            wait.Until(Expect.ElementIsVisible(By.CssSelector(".table-body__row > .column1 > a")));
            List<IWebElement> activeRequestsName = tableActiveRequests.FindElements(By.CssSelector(".table-body__row > .column1 > a")).ToList();
            
            foreach (IWebElement element in activeRequestsName)
            {
                
                if (element.Text == offerName) 
                {
                    
                    wait.Until(Expect.ElementToBeClickable(element));
                    element.Click();
                    
                    break;
                }
                             
            }


                     
             
            IWebElement objaviPonudaButton = Driver.FindElement(By.ClassName("details-panel__make-offer-btn"));
            objaviPonudaButton.Click();

            wait.Until(Expect.ElementIsVisible(By.CssSelector("div[class='table-body']")));
            IWebElement tableZaPonuda = Driver.FindElement(By.CssSelector("div[class='table-body']"));
            IWebElement firstRowTablePonuda = tableZaPonuda.FindElement(By.CssSelector("tr[class='table-body__row']"));
            IWebElement inputFieldinRow = firstRowTablePonuda.FindElement(By.CssSelector("input[ng-model='paymentType.price']"));
            inputFieldinRow.SendKeys(RandomGenerateNumbers(1));

            IWebElement vremeNaPrezemanjeButton = Driver.FindElement(By.CssSelector("input[ng-click='vm.openPickUpTimePicker()']"));
            vremeNaPrezemanjeButton.Click();


            IWebElement tableZaVremeNaPrezemanje = Driver.FindElement(By.CssSelector("table[class='uib-daypicker']"));
            List<IWebElement> datePicker = tableZaVremeNaPrezemanje.FindElements(By.TagName("td")).ToList();
            datePicker[40].Click();


            IWebElement prazenClick = Driver.FindElement(By.ClassName("main__content"));
            prazenClick.Click();



            IWebElement tableZaDostava = Driver.FindElement(By.CssSelector("input[ng-click='vm.openDeliveryTimePicker()']"));
            tableZaDostava.Click();

            wait.Until(Expect.ElementIsVisible(By.CssSelector("table[class='uib-daypicker']")));
            IWebElement tablezaVremeNaDostava = Driver.FindElement(By.CssSelector("table[class='uib-daypicker']"));
            IWebElement moveMonthButton = Driver.FindElement(By.CssSelector("button[ng-click='move(1)']"));
            moveMonthButton.Click();
            List<IWebElement> najdocnaDo = tablezaVremeNaDostava.FindElements(By.TagName("td")).ToList();
            najdocnaDo[39].Click();

            prazenClick.Click();




            IWebElement ponudataVaziDoInput = Driver.FindElement(By.CssSelector("input[ng-click='vm.openExpirationDatePicker()']"));
            ponudataVaziDoInput.Click();

            IWebElement tablezaPonudataVaziDo = Driver.FindElement(By.CssSelector("table[class='uib-daypicker']"));
            List<IWebElement> vaznostDo = tablezaPonudataVaziDo.FindElements(By.TagName("td")).ToList();
            vaznostDo[38].Click();

            prazenClick.Click();


            wait.Until(Expect.ElementIsVisible(By.ClassName("make-offer__btn-create")));
            IWebElement makeOfferButton = Driver.FindElement(By.ClassName("make-offer__btn-create"));
            makeOfferButton.Click();

            wait.Until(Expect.ElementIsVisible(By.CssSelector("button[ng-click='vm.saveOffer()']")));
            IWebElement makeOfferInformButton = Driver.FindElement(By.CssSelector("button[ng-click='vm.saveOffer()']"));
            makeOfferInformButton.Click();
               
        }





        /// <summary>
        /// Ovaa metoda e za da potvrdime deka ponudata e pratena, preku My Offers
        /// </summary>
        public void confirmOfferWasSend()
        {
             Driver.Navigate().Refresh();
             IWebElement moiPonudiButton = Driver.FindElement(By.CssSelector("a[ui-sref='provider-my-active-offers']"));
             moiPonudiButton.Click();
           

            wait.Until(Expect.UrlToBe(moiPonudiTransporterUrl));

            IWebElement tableAktivniPonudi = Driver.FindElement(By.ClassName("table-body"));
            List<IWebElement> naslovNaBaranje = tableAktivniPonudi.FindElements(By.CssSelector(".table-body__row > .column1 > a")).ToList();
            naslovNaBaranje[0].Click();

            wait.Until(Expect.ElementIsVisible(By.CssSelector("div[class='details-panel__content-wrapper-row']")));
            IWebElement imeNaPonudataCss = Driver.FindElement(By.CssSelector("div[class='details-panel__content-wrapper-row']"));

            string expectedtxttNaPonuda = offerName; 

            Assert.AreEqual(expectedtxttNaPonuda,imeNaPonudataCss.Text);
            
        }





        /// <summary>
        /// Ovaa metoda potvrduvame deka ponudata e prifatena i requestot e zatvoren
        /// </summary>
        public void confirmOffer()
        {
            Driver.Navigate().Refresh();
            wait.Until(Expect.ElementIsVisible(By.CssSelector("a[href='/client/my-requests']")));
            IWebElement moiteBaranjaButton = Driver.FindElement(By.CssSelector("a[href='/client/my-requests']"));
            moiteBaranjaButton.Click();


            wait.Until(Expect.UrlToBe(moiBaranjaUserUrl));



            IWebElement tableMoiBaranja = Driver.FindElement(By.ClassName("table-body"));
            List<IWebElement> tableRowMoiBaranja = tableMoiBaranja.FindElements(By.CssSelector(".table-body__row > .column1 > a")).ToList();
            tableRowMoiBaranja[0].Click();

            wait.Until(Expect.ElementIsVisible(By.ClassName("flex-table")));
            IWebElement ponudiZaBaranjeTable = Driver.FindElement(By.ClassName("flex-table"));
            IWebElement column8 = ponudiZaBaranjeTable.FindElement(By.ClassName("flex-table__body-row"));
            IWebElement colum8Inside = column8.FindElement(By.CssSelector("div[class='flex-table__body-row-item flex-column8']"));
            IWebElement colum8link = colum8Inside.FindElement(By.TagName("a"));
            colum8link.Click();
            

            IWebElement ponuda1RadioButton = Driver.FindElement(By.CssSelector("input[ng-model='offersSet.acceptedOffer']"));
            ponuda1RadioButton.Click();





            IWebElement rowDetailsinTableponudiZaBaranje = ponudiZaBaranjeTable.FindElement(By.ClassName("flex-table__body-row-details"));
            IWebElement prifatiPonudaRowinTablePonudiZabaranje = rowDetailsinTableponudiZaBaranje.FindElement(By.ClassName("offers-set-details__cta-btn"));
            IWebElement prifatiPonudaButtonCssLink = prifatiPonudaRowinTablePonudiZabaranje.FindElement(By.TagName("input"));
            prifatiPonudaButtonCssLink.Click();





                        

            wait.Until(Expect.UrlToBe(prifateniPonudiUrl));

            IWebElement prifateniPonudiTable = Driver.FindElement(By.ClassName("table-body"));
            IWebElement prifateniPonudiRowTable = prifateniPonudiTable.FindElement(By.ClassName("table-body__row"));
            IWebElement Column1 = prifateniPonudiRowTable.FindElement(By.CssSelector("td[class='table-body__cell column1']"));
            IWebElement nameInColum1 = Column1.FindElement(By.TagName("a"));

            string expectedNameInColum1 = offerName; 

            Assert.AreEqual(expectedNameInColum1, nameInColum1.Text);








            
            moiteBaranjaButton.Click();

            wait.Until(Expect.UrlToBe(moiBaranjaUserUrl));


            IWebElement txtForNoActiveRequests = Driver.FindElement(By.CssSelector("div[ng-show='!vm.requests.length && vm.requests']"));
            
            string expectedTextInNoActiveRequests = "Извинете! Во оваа листа нема барања.";

            Assert.AreEqual(expectedTextInNoActiveRequests, txtForNoActiveRequests.Text);




        }



    }




}
