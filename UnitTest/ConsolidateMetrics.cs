using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

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

        public class TaskData
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Action { get; set; }
            public DateTime Created { get; set; }
            public ProjectData Project { get; set; }
        }

        public class ProjectData
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class PathNodeParameterData
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public class PathNodeData
        {
            public string Namespace { get; set; }
            public string Type { get; set; }
            public string Method { get; set; }
            public string Parent { get; set; }
            public string Origin { get; set; }
            public string ReturnType { get; set; }
            public List<PathNodeParameterData> Parameters { get; set; } = new List<PathNodeParameterData>();
            public DateTime Created { get; set; }
            public CodeMetric MethodCodeMetric { get; set; }
        }

        public class Session
        {
            public string Label { get; set; }
            public string Description { get; set; }
            public string Purpose { get; set; }
            public DateTime Started { get; set; }
            public DateTime Finished { get; set; }
            public List<PathNodeData> PathNodes { get; set; } = new List<PathNodeData>();
            public TaskData Task { get; set; }
        }

        public class CodeMetric
        {
            public string Hash { get; set; }
            public string MaintainabilityIndex { get; set; }
            public string CyclomaticComplexity { get; set; }
            public string ClassCoupling { get; set; }
            public string LineOfCode { get; set; }
        }

        public List<CodeMetric> CodeMetrics { get; set; } = new List<CodeMetric>();

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

            List<CodeMetric> codeMetrics = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CodeMetric>>(objJsonDataCodeMetrics);

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

                Session session = Newtonsoft.Json.JsonConvert.DeserializeObject<Session>(objJsonData);


                //----- Out put new session data

                string objJsonDataSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(session, Newtonsoft.Json.Formatting.Indented);

                using (StreamWriter file = new StreamWriter(@"C:\Users\EduardoAFontana\Downloads\Coleta\Consolidado-2\CodeMetrics\" + Path.GetFileName(fileName), false, Encoding.UTF8))
                {
                    file.Write(objJsonDataSerialized);
                }
            }


            //foreach (Task task in Tasks)
            //{
            //    List<Session> taskSessions = Sessions.Where(x => x.Task.Name.Equals(task.Name)).ToList();

            //    double totalMiliTask = 0;

            //    foreach (Session session in taskSessions)
            //    {
            //        TimeSpan diff = session.Finished - session.Started;
            //        totalMiliTask += diff.TotalMilliseconds;
            //    }

            //    task.Total = TimeSpan.FromMilliseconds(totalMiliTask);
            //}

            //string objJsonDataSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(Tasks, Newtonsoft.Json.Formatting.Indented);

            //using (StreamWriter file = new StreamWriter(@"C:\Users\EduardoAFontana\Downloads\Coleta\Consolidado\total-tasks-time.txt", false, Encoding.UTF8))
            //{
            //    file.Write(objJsonDataSerialized);
            //}
        }
    }
}
