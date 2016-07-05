using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Common;

namespace Vanilla.Development
{
    public class MyDeskHomePage:BasePage
    {
        Configuration configuration = new Configuration();
        Dictionary<string, string> objData = new Dictionary<string, string>();

        public MyDeskHomePage(bool isVerify=true):base(false)
        {
            objData = configuration.GetValuesFromXML(configuration.CurrentDirectory + "\\ObjectRepository\\" + this.GetType().Name + ".xml");
            if (isVerify)
            {
                LocateControl();
            }
        }
        public void LocateControl()
        {

        }
        /// <summary>
        /// Function to logout
        /// </summary>
        /// <returns></returns>
        public MyDeskLoginPage logout()
        {
            try
            {
                IWebElement logoutElement = this.FindControl("xpath", objData["logout"]);
                if (logoutElement != null)
                {
                    logoutElement.Click();
                    return new MyDeskLoginPage();
                }
                throw new Exception("Can not find logout button.");
            }
            catch (Exception exception)
            {
                //throws exception if encountered inside code
                throw new Exception("Can not logout. " + exception.Message);

            }

        }
    }
}
