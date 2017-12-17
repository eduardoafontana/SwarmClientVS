using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SwarmClientVS.Domain.DataModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace UnitTest
{
    /// <summary>
    /// Summary description for SessionPost
    /// </summary>
    [TestClass]
    public class SessionPost
    {
        public SessionPost()
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

        //TODO: note: It is recommended to instantiate one HttpClient for your application's lifetime and share it.
        private static readonly HttpClient client = new HttpClient();

        [TestMethod]
        public async Task TestMethod1()
        {
            //Mount session object from file
            string fileSession = @"C:\SwarmData\session-20171217154019374.txt";

            string objJsonDataSession = String.Empty;

            using (StreamReader file = new StreamReader(fileSession, Encoding.UTF8))
            {
                objJsonDataSession = file.ReadToEnd();
            }

            SessionData session = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionData>(objJsonDataSession);

            //Post session object
            string objJsonDataSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(session, Newtonsoft.Json.Formatting.None);

            var buffer = Encoding.UTF8.GetBytes(objJsonDataSerialized);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("http://swarmserver.azurewebsites.net/api/session", byteContent);

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equals(responseString, "Object created!");
        }
    }
}
