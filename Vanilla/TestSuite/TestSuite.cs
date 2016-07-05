using NUnit.Framework;
using System;
using System.Collections;
using System.IO;
using Vanilla.Common;
using Vanilla.TestCases;

namespace Vanilla.TestSuite
{
   
        public class TestSuite
        {

        AllureOperationsPage allureOperationsPage;
        [TestFixtureSetUp]
        public void suiteSetup()
        {
            var logDirectoryPath = Path.GetDirectoryName(Path.GetDirectoryName(
            System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\Logs";
            logDirectoryPath = logDirectoryPath.Remove(0, 6);
            string[] allFile = Directory.GetFiles(logDirectoryPath);
            foreach(var file in allFile)
            {
                if(File.Exists(file))
                {
                    File.Delete(file);
                }
            }          
        }

        [Suite]
        public static IEnumerable Suite
        {
            get
            {
                ArrayList suite = new ArrayList();
                suite.Add(new V_TC1());             
                return suite;
            }
        }
       
          [TestFixtureTearDown]
          public void cleanUp()
          {
            try
            {
                // REPORTING
                allureOperationsPage = new AllureOperationsPage();
                allureOperationsPage.updateAllureXMLFiles();
            }
            catch(Exception exception)
            {
                throw new Exception("Cannot generate Allure report."+exception.Message);
            }
          }
        

}
}
