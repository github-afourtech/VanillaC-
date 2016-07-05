
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vanilla.Development;
using Vanilla.Common;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Core;
using System;
using System.IO;

namespace Vanilla.TestCases
{
    [TestClass]
    public class V_TC1
    {
        #region Variables
        #region Pages required
        /// <summary>
        /// MyDeskLoginPage to access methods on this page
        /// </summary>
        MyDeskLoginPage loginpage;

        /// <summary>
        /// MyDeskHomePage to access methods on this page
        /// </summary>
        MyDeskHomePage myDeskHomePage;
        /// <summary>
        /// Basepage to access methods on this page
        /// </summary>
        BasePage basepage;

        /// <summary>
        /// LoggerPage to access methods on this page
        /// </summary>
        LoggerPage loggerPage;

        /// <summary>
        /// ExceptionInfo to add exception details
        /// </summary>
        ExceptionInfo ef;
        #endregion

        // testCaseLogs: To add step details with exception
        List<string> testCaseLogs = new List<string>();
        // getTestdata: To get testdata for the test
        Dictionary<string, string> getTestdata = new Dictionary<string, string>();
        // startTime: startTime for the test
        string startTime = "";
        // testCaseName: Name of test case
        string testCaseName = "";
        // stepResult: Result of step
        string stepResult = "Pass";
       
        #endregion 

        [SetUp]
        public void setUp()
        {
            try
            {
                // Get a test case name           
                testCaseName = this.GetType().Name.ToString();
                
                // Create instance of LoggerPage which creates a log file for the current test case.
                loggerPage = new LoggerPage(testCaseName);
                
                // Create instance of BasePage to initialize the browser mentioned in config.json file.
                basepage = new BasePage();
                
                // Start time for the test case
                startTime = DateTime.Now.ToString();
                
                // Get testdata for test case from <TestCaseName>.json
                getTestdata = basepage.fetchTestData(testCaseName);
                
                // Create login page instance to access login functions
                // Pass parameter as false: No need to locate the controls on login page
                // e.g. new MyDeskLoginPage() calls the LocateControl() on login page.
                loginpage = new MyDeskLoginPage(false);

                // Create mydeskhomepage instance to access login functions
                // Pass parameter as false: No need to locate the controls on home page
                // e.g. new MyDeskHomePage() calls the LocateControl() on home page.
                // e.g. new MyDeskHomePage(false) skip the LocateControl() on home page.
                myDeskHomePage = new MyDeskHomePage(false);

                // Navigate to URL 
                // URL details are provided in config.json. Application URL            
                loginpage.navigateToURL();

            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                NUnit.Framework.Assert.Fail("Error : " + exception.Message);

            }
        }

        [Test]
        public void V_TC1_Test()
        {
            try
            {
                // Login to the application 
                // login(string username,string password)
                // getTestData["ParameterName"]
                // ParameterName: In <TestCaseName.json> parameters are present.
                loginpage.login(getTestdata["emailID"], getTestdata["password"]);

                // Add step log to the dictionary testCaseLogs 
                // stepResult: Pass
                testCaseLogs.Add("Step 1: Login to the application "+stepResult);

                // Logout from application
                myDeskHomePage.logout();
                testCaseLogs.Add("Step 2: Logout from application " + stepResult);


            }
            catch (Exception exception)
            {
                // Change stepResult to Fail
                stepResult = "Fail";
                // Create instance of ExceptionInfo class
                // Parameter passed is exception details
                ef = new ExceptionInfo(exception);
                // Add step log to the dictionary testCaseLogs 
                // stepResult: Fail
                testCaseLogs.Add("Step 1: Login to the application "+ stepResult);
                NUnit.Framework.Assert.Fail("Error : " + exception.Message);
                
            }
            finally
            {
                // Add logs to the log file
                // ProjectDirectory\Logs\<testcasename>.log
                loggerPage.WriteLogs(testCaseLogs);
            }

        }

        [TearDown]
        public void cleanUp()
        {
            try
            {
                // Close the driver
                basepage = new BasePage(false);
                basepage.close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                NUnit.Framework.Assert.Fail("Error : " + exception.Message);
            }
        }
    }
}
