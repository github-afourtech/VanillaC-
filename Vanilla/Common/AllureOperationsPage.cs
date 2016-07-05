using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Vanilla.Common
{

    class AllureOperationsPage
    {
        string logFilepath;
        bool flag = false;
        XmlDocument doc;
        XmlElement root;
        XmlNodeList nodes;
        XmlNodeList allTestCaseNodes;
        ArrayList allLines;
        string[] allTestCases;
      
        /// <summary>
        /// Function to update all XML files
        /// </summary>
        public void updateAllureXMLFiles()
        {
            allTestCases = new string[4];
            List<string> s = new List<string>();
            foreach (var f in TestSuite.TestSuite.Suite)
            {
                allTestCases = f.ToString().Split('.');
                s.Add(allTestCases[2].ToString());
                foreach (var tcname in s)
                {
                    var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
                    System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
                    requiredPath= requiredPath.Remove(0, 6);
                    logFilepath = requiredPath+ "\\Logs\\" + tcname.ToString() + ".log";
                    allLines = getLogDetails(logFilepath);
                    string[] allxmls = new string[4];
                    if (Directory.Exists(requiredPath + "\\TestLogResults"))
                    {
                        allxmls = Directory.GetFiles(requiredPath + "\\TestLogResults");
                    }
                    for (int i = 0; i < Convert.ToInt32(allxmls.Length); i++)
                    {

                        System.IO.File.SetAttributes(allxmls[i].ToString(), System.IO.FileAttributes.Normal);
                        doc = new XmlDocument();
                        doc.Load(allxmls[i].ToString());
                        root = doc.DocumentElement;
                        nodes = root.ChildNodes;
                        allTestCaseNodes = root.GetElementsByTagName("test-case");
                        foreach (XmlNode tcnode in allTestCaseNodes)
                        {
                            if (tcnode.InnerText.Contains(f.ToString()))
                            {
                                updateXML(allLines, allxmls[i]);
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Function to get logs from the log file
        /// </summary>
        /// <param name="filePath">FilePath</param>
        /// <returns>All logs added to the file</returns>
        public ArrayList getLogDetails(string filePath)
        {
            try
            {
             
                string[] lines = System.IO.File.ReadAllLines(filePath);
                ArrayList alllines = new ArrayList();
                string[] test = new string[100];

                foreach (var line in lines)
                {
                    test = line.Split(':');
                    alllines.Add(test[1]);
                }
                return alllines;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        // Update XMLs generated Nunit console
        public void updateXML(ArrayList testStepDescription, string xmlFilePath)
        {
            try
            {
                System.IO.File.SetAttributes(xmlFilePath, System.IO.FileAttributes.Normal);
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFilePath);
                XmlElement root = doc.DocumentElement;
                XmlNodeList nodes = root.ChildNodes;
                XmlNodeList listsss = root.GetElementsByTagName("test-case");
                XmlElement newnode1 = doc.CreateElement("steps");
                listsss[0].AppendChild(newnode1);
                foreach (var innerNode in testStepDescription)
                {
                    XmlElement newnode = doc.CreateElement("step");
                    newnode1.AppendChild(newnode);
                    XmlElement stepNameNode = doc.CreateElement("name");
                    stepNameNode.InnerText = innerNode.ToString();
                    newnode.AppendChild(stepNameNode);
                }
                doc.Save(xmlFilePath);
            }
            catch(Exception exception)
            {
                throw new Exception("Can not update the XML." + exception.Message);
            }
        }
    }

}

