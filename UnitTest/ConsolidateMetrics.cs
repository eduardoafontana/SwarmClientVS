using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using SwarmClientVS.Domain.DataModel;
using SwarmClientVS.Domain.Service;

namespace UnitTest
{
    [TestClass]
    public class ConsolidateMetrics
    {
        public ConsolidateMetrics()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        public List<CodeMetricData> CodeMetrics { get; set; } = new List<CodeMetricData>();

        [TestMethod]
        public void TestMethod1()
        {
            //-------- Read code metrics

            string fileCodeMetrics = @"C:\Users\EduardoAFontana\Downloads\Coleta\Consolidado-2\CodeMetrics\FastCodeMetrics.txt";

            string objJsonDataCodeMetrics = String.Empty;

            using (StreamReader file = new StreamReader(fileCodeMetrics, Encoding.UTF8))
            {
                objJsonDataCodeMetrics = file.ReadToEnd();
            }

            List<CodeMetricData> codeMetrics = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CodeMetricData>>(objJsonDataCodeMetrics);

            //--------- Read Sessions files

            string[] fileEntries = Directory.GetFiles(@"C:\Users\EduardoAFontana\Downloads\Coleta\Consolidado-2");

            foreach (string fileName in fileEntries)
            {
                if (fileName.Contains("swarm-input-data"))
                    continue;

                if (fileName.Contains("total-tasks-time"))
                    continue;

                if (fileName.Contains("desktop.ini"))
                    continue;

                string objJsonData = String.Empty;

                using (StreamReader file = new StreamReader(fileName, Encoding.UTF8))
                {
                    objJsonData = file.ReadToEnd();
                }

                SessionData session = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionData>(objJsonData);

                //----- Pass entire pathnode, search respective code metric and add

                foreach(PathNodeData node in session.PathNodes)
                {
                    node.Hash = PathNodeItemModel.GetHash(session.GetCleanProjectName(), node.Namespace, node.Type, node.Method);
                    
                    CodeMetricData codeMetric = codeMetrics.FirstOrDefault(x => x.Hash.ToLower().Equals(node.Hash.ToLower()));

                    if (codeMetric != null)
                        node.MethodCodeMetric = codeMetric;
                }

                //----- Out put new session data

                string objJsonDataSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(session, Newtonsoft.Json.Formatting.Indented);

                using (StreamWriter file = new StreamWriter(@"C:\Users\EduardoAFontana\Downloads\Coleta\Consolidado-2\CodeMetrics\" + Path.GetFileName(fileName), false, Encoding.UTF8))
                {
                    file.Write(objJsonDataSerialized);
                }
            }
        }
    }
}
