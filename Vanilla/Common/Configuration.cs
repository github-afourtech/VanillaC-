using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using NUnit.Framework;
using Vanilla.Development;
using System.Drawing.Imaging;
using OpenQA.Selenium;

namespace Vanilla.Common
{
    public class Configuration
    {
        # region Variables
        public static string applicationUrl = string.Empty;
        public static string currentDirectory = string.Empty;
        public static string browser = string.Empty;
        private static int timeout = 0;
        

        public string AplicationUrl
        {
            get { return applicationUrl; }
            set { applicationUrl = value; }
        }
        public int TimeOut
        {
            get { return timeout; }
            set {timeout = value; }
        }
     

        public string CurrentDirectory
        {
            get { return currentDirectory; }
            set { currentDirectory = value; }
        }

        public string Browser
        {
            get { return browser; }
            set { browser = value; }
        }
        # endregion

        # region Constructor
        public Configuration()
        {
                LoadConfigData();
        }
        # endregion

        # region Function to build Config file path for test case data
        /// <summary>
        /// Function to build Config file path for test case data
        /// </summary>
        /// <param name="testCaseName">testCaseName</param>
        /// <returns>string testCaseName.json</returns>
        public string buildConfigFilePath(string testCaseName)
        {
            string configFilePath = CurrentDirectory+"\\TestData\\TestCaseData\\"+testCaseName+".json";
            return configFilePath;
        }
        # endregion

        # region Function to load test data for Test case
        /// <summary>
        /// Function to load test case data from json
        /// </summary>
        /// <param name="path">JSON path for testcase.json</param>
        /// <returns>Dictionary jsonDictionary </returns>
        public Dictionary<string,string> LoadConfigData(string path)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string streamReaderString = streamReader.ReadToEnd();
                    var jsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(streamReaderString);
                    return jsonDictionary;
                }
            }
            catch(Exception exception)
            {
                //throws exception if encountered inside code
                throw new Exception("Can not login with given credentials. " + exception.Message);
            }
        }
        # endregion

        /// <summary>
        /// Function to get test case name
        /// </summary>
        /// <returns>Test case name</returns>
        public string GetTestCaseName()
        {
            return this.GetType().Name;
        }

        #region Function to load Config data 
        /// <summary>
        /// Function to load configdata
        /// </summary>
        public void LoadConfigData()
            {
           
                CurrentDirectory = this.GetProjectLocation();
                //string configFilePath =requiredPath+"\\TestData\\Config\\Config.json";
                string configFilePath =CurrentDirectory+"\\TestData\\Config\\Config.json";

               //Get values from JSON config file
                applicationUrl = getValuesFromJSON("applicationURL", configFilePath);
                browser = getValuesFromJSON("browser", configFilePath);
                timeout =Convert.ToInt32(getValuesFromJSON("timeout", configFilePath));
        }
        # endregion

        #region Function to get values from JSON file for Config data
        /// <summary>
        /// Function to get values from JSON file
        /// </summary>
        /// <param name="JsonAttributeName">Name of attribute</param>
        /// <param name="configFilePath">Config file path</param>
        /// <returns>Value of the key JsonAttributeName </returns>
        public string getValuesFromJSON(string JsonAttributeName,string configFilePath)
            {
                dynamic jsonDynamicVar = null;
                try
                {
                    using(StreamReader sr=new StreamReader(configFilePath))
                    {
                        string jsonString=sr.ReadToEnd();
                        jsonDynamicVar = JsonConvert.DeserializeObject(jsonString);
                        foreach (var x in jsonDynamicVar)
                        {
                            var key = ((JProperty)(x)).Name;
                            if (key.ToString().Equals(JsonAttributeName))
                            {
                                var jvalue = ((JProperty)(x)).Value;
                                return jvalue.ToString();
                            }

                        }
                        throw new Exception("Failed to fetch JSON attributes.");
                
                    }
                    throw new Exception("Failed to read JSON.");
                }
                catch(Exception exception)
                {
                    throw new Exception("Failed to get a values rom JSON file."+exception.Message);
                }
            }
        # endregion

        #region Function to get project location
        /// <summary>
        /// Functio to get a project location
        /// </summary>
        /// <returns>Project path</returns>
        public string GetProjectLocation()
        {
            string projectPath = null;
            try
            {
                //  var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
                //System.IO.Path.GetDirectoryName(
                //      System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));



                projectPath = System.IO.Directory.GetCurrentDirectory();
                System.IO.DirectoryInfo newProjectPath = System.IO.Directory.GetParent(projectPath).Parent;
                return newProjectPath.FullName.ToString();
               // return requiredPath;
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to run GetProjectLocation " + exception.Message);
            }
        }
        # endregion

        #region Function to get value from XML file for Object repository
        /// <summary>
        /// Function to get a values from XML
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetValuesFromXML(string xmlFilePath)
        {
            try
            {
              
                System.IO.File.SetAttributes(xmlFilePath, System.IO.FileAttributes.Normal);
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFilePath);
                XmlElement root = doc.DocumentElement;
                XmlNodeList nodes= root.ChildNodes;
                Dictionary<string, string> allXmlNodes = new Dictionary<string, string>();

                foreach (XmlNode node in nodes)
                {
                    allXmlNodes.Add(node.Name, node.ChildNodes[0].InnerText);
                }
                
                return allXmlNodes;
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to run GetValuesFromXML " + exception.Message);
            }
        }
        #endregion
                  
        #region Function to capture screenshot
        /// <summary>
        /// Function to capture screenshot
        /// </summary>
        public void captureScreenshot()
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)BasePage.driver).GetScreenshot();
                screenshot.SaveAsFile(this.CurrentDirectory + "\\Screenshots\\" + TestContext.CurrentContext.Test.Name + "_Captured" + ".png", ImageFormat.Png);
            }
            catch(Exception exception)
            {
                throw new Exception("Unable to capture screenshot."+exception.Message);
            }
        }
        #endregion
    }
}
