using Vanilla.Common;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Vanilla.Development
{
    public class BasePage
    {

        Configuration configuration = new Configuration();
        public static IWebDriver driver = null;
        public static string testCase = string.Empty;

        public IWebDriver Driver
        {
            get { return driver; }
            set { driver = value; }
        }

        /// <summary>
        /// Constructor of BasePage
        /// </summary>
        /// <param name="isVerify">LocateControl verification flag</param>
        public BasePage(bool isVerify = true)
            {
            try
            {
                if (isVerify)
                {
                    string browserName = configuration.Browser;
                    switch (browserName)
                    {
                        case "chrome":
                            Driver = new ChromeDriver(configuration.CurrentDirectory + "\\Driver");
                            Driver.Manage().Window.Maximize();

                            break;
                        case "firefox":
                            FirefoxProfile firefoxProfile = (new FirefoxProfileManager()).GetProfile("default");
                            firefoxProfile.SetPreference("webdriver.load.strategy", "unstable");
                            Driver = new FirefoxDriver(firefoxProfile);
                            Driver.Manage().Window.Maximize();
                            break;
                        case "ie":
                            Driver = new InternetExplorerDriver(configuration.CurrentDirectory + "\\Driver");
                            Driver.Manage().Window.Maximize();
                            break;
                    }
                    driver = this.Driver;
                }
            }
            catch(Exception exception)
            {
                throw new Exception("Can not initialize webdriver."+Configuration.browser+ exception.Message);
            }
            }

        /// <summary>
        /// Function to close the browser
        /// </summary>
            public void close()
            {
                if (Driver != null)
                {
                    Driver.Quit();
                }
            }
           
            /// <summary>
            /// Finds the webelement on webpage
            /// </summary>
            /// <param name="elementType">Type of element ID/ XPATH etc.</param>
            /// <param name="elementValue">name of attribute</param>
            /// <returns></returns>
            public IWebElement FindControl(string elementType, string elementValue)
            {
                try
                {
                  
                    IWebElement element = null;
                    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5)); ;
                   
                        switch (elementType)
                        {
                            case "xpath":
                            try
                            {
                                element = wait.Until<IWebElement>((d) =>
                                {
                                    return d.FindElement(By.XPath(elementValue));

                                });
                            }
                            catch { }

                                break;
                            case "id":
                            try
                            {
                                element = wait.Until<IWebElement>((d) =>
                                {
                                    return d.FindElement(By.Id(elementValue));

                                });
                            }
                            catch { }
                                break;
                            case "class":
                            try
                            {
                                element = wait.Until<IWebElement>((d) =>
                                {
                                    return d.FindElement(By.ClassName(elementValue));

                                });
                            }
                            catch { }
                                break;
                        }
                 return element;
            }
                catch (Exception exception)
                {
                    //Creates an instance of ExceptionInfo class.
                    ExceptionInfo ex = new ExceptionInfo(exception.GetType().ToString(), exception.Message, exception.StackTrace, exception.InnerException.ToString());
                    //throws exception if encountered inside code
                    throw new Exception("Exception while locating controls." + exception.Message);

                }

            }

        


        /// <summary>
        /// Function to fetch the test data
        /// </summary>
        /// <param name="testCaseName">Test case Name</param>
        /// <returns></returns>
            public Dictionary<string, string> fetchTestData(string testCaseName)
            {
                try
                {
                    string path = configuration.buildConfigFilePath(testCaseName);
                    Dictionary<string, string> testdata = configuration.LoadConfigData(path);
                    return testdata;
                }
                catch (Exception exception)
                {
                    //Creates an instance of ExceptionInfo class.
                    ExceptionInfo ex = new ExceptionInfo(exception.GetType().ToString(), exception.Message, exception.StackTrace, exception.InnerException.ToString());
                    //logPage.writeLogs("Exception while fetching test data.", "Fail", configuration.TestCaseName, ex.ToString(), DateTime.Now);
                    ////throws exception if encountered inside code
                    throw new Exception("Exception while fetching test data." + exception.Message);

                }
            }
     


    }
    }



