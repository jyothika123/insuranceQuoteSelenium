using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

[TestFixture]
public class InsuranceQuoteTestTest
{
    private IWebDriver driver;
    public IDictionary<string, object> vars { get; private set; }
    private IJavaScriptExecutor js;

    [SetUp]
    public void SetUp()
    {
        driver = new FirefoxDriver();
        driver.Manage().Window.Maximize();
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<string, object>();
    }

    [TearDown]
    protected void TearDown()
    {
        driver.Quit();
    }

    [Test]
    public void insuranceQuote01ValidDataAge24Exp3NoAccidentsBaseRate()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.CssSelector(".btn")));
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("jyothika");
            driver.FindElement(By.Id("lastName")).SendKeys("ganga dayal");
            driver.FindElement(By.Id("address")).SendKeys("201,50 university avenue east");
            driver.FindElement(By.Id("city")).SendKeys("waterloo");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("437-559-9257");
            driver.FindElement(By.Id("email")).SendKeys("jyothika@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("24");
            driver.FindElement(By.Id("experience")).SendKeys("3");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("btnSubmit")).Click();
            string value = driver.FindElement(By.Id("finalQuote")).GetAttribute("value");
            Assert.That(value, Is.EqualTo("$5500"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote02ValidDataAge25Exp34AccidentsInsuranceRefused()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("JOEL");
            driver.FindElement(By.Id("lastName")).SendKeys("THOMAS");
            driver.FindElement(By.Id("address")).SendKeys("WEST");
            driver.FindElement(By.Id("city")).SendKeys("WATERLOO");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("437-987-8900");
            driver.FindElement(By.Id("email")).SendKeys("joel@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("25");
            driver.FindElement(By.Id("experience")).SendKeys("3");
            driver.FindElement(By.Id("accidents")).SendKeys("4");
            driver.FindElement(By.Id("btnSubmit")).Click();
            string value = driver.FindElement(By.Id("finalQuote")).GetAttribute("value");
            Assert.That(value, Is.EqualTo("No Insurance for you!!  Too many accidents - go take a course!"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote03ValidDataAge35Exp92AccidentsWithDiscount()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("abin");
            driver.FindElement(By.Id("lastName")).SendKeys("thomas");
            driver.FindElement(By.Id("address")).SendKeys("west");
            driver.FindElement(By.Id("city")).SendKeys("waterloo");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("438-890-9989");
            driver.FindElement(By.Id("email")).SendKeys("abin@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("35");
            driver.FindElement(By.Id("experience")).SendKeys("9");
            driver.FindElement(By.Id("accidents")).SendKeys("2");
            driver.FindElement(By.Id("btnSubmit")).Click();
            string value = driver.FindElement(By.Id("finalQuote")).GetAttribute("value");
            Assert.That(value, Is.EqualTo("$3905"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote04InvalidEmailFormatAge28Exp30AccShowsError()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("manju");
            driver.FindElement(By.Id("lastName")).SendKeys("mol");
            driver.FindElement(By.Id("address")).SendKeys("asd");
            driver.FindElement(By.Id("city")).SendKeys("waterloo");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("437-778-0987");
            driver.FindElement(By.Id("email")).SendKeys("as");
            driver.FindElement(By.Id("age")).SendKeys("28");
            driver.FindElement(By.Id("experience")).SendKeys("3");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("btnSubmit")).Click();
            Assert.That(driver.FindElement(By.Id("email-error")).Text, Is.EqualTo("Must be a valid email address"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote05InvalidPhoneFormatAge27Exp30AccShowsError()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("amal");
            driver.FindElement(By.Id("lastName")).SendKeys("davis");
            driver.FindElement(By.Id("address")).SendKeys("asd");
            driver.FindElement(By.Id("city")).SendKeys("wast");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 234");
            driver.FindElement(By.Id("phone")).SendKeys("437889995");
            driver.FindElement(By.Id("phone")).SendKeys(Keys.Down);
            driver.FindElement(By.Id("phone")).SendKeys("43788999567");
            driver.FindElement(By.Id("email")).SendKeys("amal@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("27");
            driver.FindElement(By.Id("experience")).SendKeys("3");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("btnSubmit")).Click();
            IWebElement phoneError = driver.FindElement(By.Id("phone-error"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", phoneError);
            Assert.That(phoneError.Text, Is.EqualTo("Phone Number must follow the patterns 111-111-1111 or (111)111-1111"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote06InvalidPostalCodeAge35Exp151AccShowsError()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("qwerty");
            driver.FindElement(By.Id("lastName")).SendKeys("asd");
            driver.FindElement(By.Id("address")).SendKeys("dgggd");
            driver.FindElement(By.Id("city")).SendKeys("sw");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J234");
            driver.FindElement(By.Id("phone")).SendKeys("437-778-9876");
            driver.FindElement(By.Id("email")).SendKeys("qwerty@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("35");
            driver.FindElement(By.Id("experience")).SendKeys("15");
            driver.FindElement(By.Id("accidents")).SendKeys("1");
            driver.FindElement(By.Id("btnSubmit")).Click();
            IWebElement postalCodeError = driver.FindElement(By.Id("postalCode-error"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", postalCodeError);
            Assert.That(postalCodeError.Text, Is.EqualTo("Postal Code must follow the pattern A1A 1A1"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote07InvalidAgeNoAgeExp50AccShowsError()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("asdfgf");
            driver.FindElement(By.Id("lastName")).SendKeys("jkl");
            driver.FindElement(By.Id("address")).SendKeys("shhhd");
            driver.FindElement(By.Id("city")).SendKeys("dbbbf");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("437-889-0987");
            driver.FindElement(By.Id("email")).SendKeys("asd@gmail.com");
            driver.FindElement(By.Id("experience")).SendKeys("5");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("btnSubmit")).Click();
            Assert.That(driver.FindElement(By.Id("age-error")).Text, Is.EqualTo("Age (>=16) is required"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote08InvalidAccAge37Exp8NoAccShowsError()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("sagvd");
            driver.FindElement(By.Id("lastName")).SendKeys("djb");
            driver.FindElement(By.Id("address")).SendKeys("db s");
            driver.FindElement(By.Id("city")).SendKeys("efb ");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("437-990-0987");
            driver.FindElement(By.Id("email")).SendKeys("sag@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("37");
            driver.FindElement(By.Id("experience")).SendKeys("8");
            driver.FindElement(By.Id("btnSubmit")).Click();
            IWebElement accError = driver.FindElement(By.Id("accidents-error"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", accError);
            Assert.That(accError.Text, Is.EqualTo("Number of accidents is required"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote09InvalidExpAge45NoExpAcc0ShowsError()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("erhf");
            driver.FindElement(By.Id("lastName")).SendKeys("erfjnd");
            driver.FindElement(By.Id("address")).SendKeys("rjf");
            driver.FindElement(By.Id("city")).SendKeys("jrkfd");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("435-908-9876");
            driver.FindElement(By.Id("email")).SendKeys("erh@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("45");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("btnSubmit")).Click();
            Assert.That(driver.FindElement(By.Id("experience-error")).Text, Is.EqualTo("Years of experience is required"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote10InvalidAgeAge15Exp1Acc0ShowsError()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("ekwfjn");
            driver.FindElement(By.Id("lastName")).SendKeys("enbfrs");
            driver.FindElement(By.Id("address")).SendKeys("erfb ");
            driver.FindElement(By.Id("city")).SendKeys("f n");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("435-098-0998");
            driver.FindElement(By.Id("email")).SendKeys("ekw@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("15");
            driver.FindElement(By.Id("experience")).SendKeys("1");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("btnSubmit")).Click();
            Assert.That(driver.FindElement(By.Id("age-error")).Text, Is.EqualTo("Please enter a value greater than or equal to 16."));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote11ExpAge16Age20Exp6Acc0()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("jh");
            driver.FindElement(By.Id("lastName")).SendKeys("erj");
            driver.FindElement(By.Id("address")).SendKeys("erhfbd");
            driver.FindElement(By.Id("city")).SendKeys("efrdjs");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("437-889-8907");
            driver.FindElement(By.Id("email")).SendKeys("jh@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("20");
            driver.FindElement(By.Id("experience")).SendKeys("6");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("btnSubmit")).Click();
            string value = driver.FindElement(By.Id("finalQuote")).GetAttribute("value");
            Assert.That(value, Is.EqualTo("No Insurance for you!! Driver Age / Experience Not Correct"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote12NoExpHighRateAge29Exp0Acc0()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("wbfaesdc");
            driver.FindElement(By.Id("lastName")).SendKeys("sbdc f");
            driver.FindElement(By.Id("address")).SendKeys("bsdaZ");
            driver.FindElement(By.Id("city")).SendKeys("jvhbsd");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("435-890-0987");
            driver.FindElement(By.Id("email")).SendKeys("jh@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("29");
            driver.FindElement(By.Id("experience")).SendKeys("0");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("btnSubmit")).Click();
            string value = driver.FindElement(By.Id("finalQuote")).GetAttribute("value");
            Assert.That(value, Is.EqualTo("$7000"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote13NoDiscountAge31Exp1Acc0()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("erafnkds");
            driver.FindElement(By.Id("lastName")).SendKeys("vnea s");
            driver.FindElement(By.Id("address")).SendKeys("nfd");
            driver.FindElement(By.Id("city")).SendKeys("en vrs,");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("438-098-8907");
            driver.FindElement(By.Id("email")).SendKeys("era@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("31");
            driver.FindElement(By.Id("experience")).SendKeys("1");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("btnSubmit")).Click();
            string value = driver.FindElement(By.Id("finalQuote")).GetAttribute("value");
            Assert.That(value, Is.EqualTo("$5500"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote14BaseRateAge29Exp9Acc2()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("n asvd,");
            driver.FindElement(By.Id("lastName")).SendKeys("fvn d");
            driver.FindElement(By.Id("address")).SendKeys("f vds");
            driver.FindElement(By.Id("city")).SendKeys("dsjvf");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("435-987-9876");
            driver.FindElement(By.Id("email")).SendKeys("asvd@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("29");
            driver.FindElement(By.Id("experience")).SendKeys("9");
            driver.FindElement(By.Id("accidents")).SendKeys("2");
            driver.FindElement(By.Id("btnSubmit")).Click();
            string value = driver.FindElement(By.Id("finalQuote")).GetAttribute("value");
            Assert.That(value, Is.EqualTo("$5500"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }

    [Test]
    public void insuranceQuote15EligibleforDiscountAge40Exp10Acc0()
    {
        try
        {
            driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("b ,mwrefv");
            driver.FindElement(By.Id("lastName")).SendKeys("jh3b4fr");
            driver.FindElement(By.Id("address")).SendKeys("erfbfw ");
            driver.FindElement(By.Id("city")).SendKeys("jwfrfb");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2J 2V8");
            driver.FindElement(By.Id("phone")).SendKeys("437-098-8978");
            driver.FindElement(By.Id("email")).SendKeys("mr@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("40");
            driver.FindElement(By.Id("experience")).SendKeys("10");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("btnSubmit")).Click();
            string value = driver.FindElement(By.Id("finalQuote")).GetAttribute("value");
            Assert.That(value, Is.EqualTo("$2840"));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Test Failed: {ex.Message}");
        }
    }
}