using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace UnitTest
{
    [TestClass]
    public class ReadOds
    {
        public ReadOds()
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

        public class CodeMetric
        {
            public string Scope { get; set; }
            public string Member { get; set; }
            public string MaintainabilityIndex { get; set; }
            public string CyclomaticComplexity { get; set; }
            public string ClassCoupling { get; set; }
            public string LineOfCode { get; set; }
        }

        public List<CodeMetric> CodeMetrics { get; set; } = new List<CodeMetric>();

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

        [TestMethod]
        public void TestMethod1()
        {
            string filePath = @"C:\Users\EduardoAFontana\Downloads\Coleta\Systems Code Metrics\PROMOVE.ods";

            using (FileStream fileStream = File.Open(filePath, FileMode.Open))
            {
                string contentXml = GetContentXml(fileStream);

                XDocument documentXml = XDocument.Parse(contentXml);

                List<XElement> rows = documentXml.Descendants("{urn:oasis:names:tc:opendocument:xmlns:table:1.0}table-row").Skip(1).ToList();

                for (int i = 0; i < rows.Count - 1; i++)//count - 1 => skip footer
                {
                    try
                    {
                        if (!rows[i].Elements().ToList()[0].Value.Equals("Member"))
                            continue;

                        CodeMetrics.Add(new CodeMetric
                        {
                            Scope = rows[i].Elements().ToList()[0].Value,
                            Member = rows[i].Elements().ToList()[4].Value,
                            MaintainabilityIndex = rows[i].Elements().ToList()[5].Value,
                            CyclomaticComplexity = rows[i].Elements().ToList()[6].Value,
                            ClassCoupling = rows[i].Elements().ToList()[8].Value,
                            LineOfCode = rows[i].Elements().ToList()[9].Value
                        });
                    }
                    catch (Exception ex)
                    {
                        int ii = i;
                    }
                }
            }

            string fileJsonOut = @"C:\Users\EduardoAFontana\Downloads\Coleta\Systems Code Metrics\PromoveCodeMetrics.txt";

            string objJsonDataSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(CodeMetrics, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter file = new StreamWriter(fileJsonOut, false, Encoding.UTF8))
            {
                file.Write(objJsonDataSerialized);
            }
        }

        private static string GetContentXml(Stream fileStream)
        {
            var contentXml = "";

            using (var zipInputStream = new ZipInputStream(fileStream))
            {
                ZipEntry contentEntry = null;
                while ((contentEntry = zipInputStream.GetNextEntry()) != null)
                {
                    if (!contentEntry.IsFile)
                        continue;
                    if (contentEntry.Name.ToLower() == "content.xml")
                        break;
                }

                if (contentEntry.Name.ToLower() != "content.xml")
                {
                    throw new Exception("Cannot find content.xml");
                }

                var bytesResult = new byte[] { };
                var bytes = new byte[2000];
                var i = 0;

                while ((i = zipInputStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    var arrayLength = bytesResult.Length;
                    Array.Resize<byte>(ref bytesResult, arrayLength + i);
                    Array.Copy(bytes, 0, bytesResult, arrayLength, i);
                }

                contentXml = Encoding.UTF8.GetString(bytesResult);
            }
            return contentXml;
        }
    }
}
