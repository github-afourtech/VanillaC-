using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Vanilla.Common;

namespace Vanilla.Development
{
    public class MyDeskLoginPage : BasePage
    {
        Configuration configuration = new Configuration();
        Dictionary<string, string> objData = new Dictionary<string, string>();
        public MyDeskLoginPage(bool isVerify = true) : base(false)
        {

            objData = configuration.GetValuesFromXML(configuration.CurrentDirectory + "\\ObjectRepository\\" + this.GetType().Name + ".xml");
            if (isVerify)
            {
                LocateControl();
            }
        }
        #region Function to locate controls on Login page
        /// <summary>
        /// Function to Locate Controls on Login Page
        /// </summary>
        public void LocateControl()
        {
            try
            {
                IWebElement emailElement = this.FindControl("id", objData["username"]);
                if (emailElement == null)
                {
                    throw new Exception("Can not locate element UserName");
                }
                IWebElement pwdElement = this.FindControl("id", objData["password"]);
                if (pwdElement == null)
                {
                    throw new Exception("Can not locate element Password");
                }
                IWebElement signIn = this.FindControl("xpath", objData["signInBtn"]);
                if (emailElement == null)
                {
                    throw new Exception("Can not locate element SignIn Button");
                }
            }
            catch (Exception exception)
            {
                //Creates an instance of ExceptionInfo class.
                // ExceptionInfo ex = new ExceptionInfo(exception.GetType().ToString(), exception.Message, exception.StackTrace, exception.InnerException.ToString());
                ////throws exception if encountered inside code
                throw new Exception("Exception while locating controls on Login page. " + exception.Message);

            }
        }
        #endregion

        #region Function to navigate to URL
        /// <summary>
        /// Function to navigate to URL
        /// </summary>
        public void navigateToURL()
        {
            try
            {
                driver.Navigate().GoToUrl(configuration.AplicationUrl);
                LocateControl();

            }
            catch (Exception exception)
            {
                //Creates an instance of ExceptionInfo class.
                ExceptionInfo ex = new ExceptionInfo(exception.GetType().ToString(), exception.Message, exception.StackTrace, exception.InnerException.ToString());
                ////throws exception if encountered inside code
                throw new Exception("Exception in Open URL " + exception.Message);

            }

        }
        #endregion

        #region Function to login to application
        /// <summary>
        /// Function to login to application
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        public MyDeskLoginPage login(string userName, string password)
        {
            try
            {
                IWebElement emailElement = this.FindControl("id", objData["username"]);
                if (emailElement != null)
                {
                    emailElement.SendKeys(userName);

                    // Enter Password
                    IWebElement passwordElement = this.FindControl("id", objData["password"]);
                    if (passwordElement != null)
                    {
                         passwordElement.SendKeys(password);

                            // Click on Sign In
                            IWebElement signinElement = this.FindControl("xpath", objData["signInBtn"]);
                            if (signinElement != null)
                            {
                                signinElement.Click();
                                IWebElement searchTextEle = this.FindControl("xpath", objData["searchTextBox"]);
                                if (searchTextEle != null)
                                {
                                    return new MyDeskLoginPage(false);
                                }
                                throw new Exception("Cannot locate search text box.");
                            }
                        }
                        throw new Exception("Textbox for password not found.");
                    }
                    throw new Exception("Username text box not found.");
            }
            catch (Exception exception)
            {
                //throws exception if encountered inside code
                throw new Exception("Can not login with given credentials. " + exception.Message);
            }
            #endregion
        }

      
    }
}
