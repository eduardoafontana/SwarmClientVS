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
    public class TasksTime
    {
        public TasksTime()
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

        public class Task
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public TimeSpan Total { get; set; }
        }

        public class Session
        {
            public string Label { get; set; }
            public string Description { get; set; }
            public string Purpose { get; set; }
            public DateTime Started { get; set; }
            public DateTime Finished { get; set; }
            public Task Task { get; set; }
        }

        public List<Session> Sessions { get; set; } = new List<Session>();
        public List<Task> Tasks { get; set; } = new List<Task>();

        [TestMethod]
        public void TestMethod1()
        {
            string[] fileEntries = Directory.GetFiles(@"C:\Users\EduardoAFontana\Downloads\Coleta\Consolidado");

            foreach (string fileName in fileEntries)
            {
                if (fileName.Contains("swarm-input-data"))
                    continue;

                if (fileName.Contains("total-tasks-time"))
                    continue;

                string objJsonData = String.Empty;

                using (StreamReader file = new StreamReader(fileName, Encoding.UTF8))
                {
                    objJsonData = file.ReadToEnd();
                }

                Session session = Newtonsoft.Json.JsonConvert.DeserializeObject<Session>(objJsonData);

                Sessions.Add(session);

                if (Tasks.FirstOrDefault(x => x.Name.Equals(session.Task.Name)) == null)
                    Tasks.Add(session.Task);
            }

            foreach (Task task in Tasks)
            {
                List<Session> taskSessions = Sessions.Where(x => x.Task.Name.Equals(task.Name)).ToList();

                double totalMiliTask = 0;

                foreach (Session session in taskSessions)
                {
                    TimeSpan diff = session.Finished - session.Started;
                    totalMiliTask += diff.TotalMilliseconds;
                }

                task.Total = TimeSpan.FromMilliseconds(totalMiliTask);
            }

            string objJsonDataSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(Tasks, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter file = new StreamWriter(@"C:\Users\EduardoAFontana\Downloads\Coleta\Consolidado\total-tasks-time.txt", false, Encoding.UTF8))
            {
                file.Write(objJsonDataSerialized);
            }
        }
    }
}
