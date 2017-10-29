using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using SwarmClientVS;

namespace UnitTest
{
    [TestClass]
    public class SCLogTest
    {
        [TestMethod]
        public void TestFileSave()
        {
            string fileName = @".\testSaveFile.txt";
            string lines = "First line.\r\nSecond line.\r\nThird line.";

            using (StreamWriter file = new StreamWriter(fileName, true, Encoding.UTF8))
            {
                file.WriteLine(lines);
            }

            Assert.IsTrue(File.Exists(fileName));
        }

        [TestMethod]
        public void TestFileRead()
        {
            string fileName = @".\testRead.txt";
            string lines = "First line.\r\nSecond line.\r\nThird line.";

            using (StreamWriter file = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                file.WriteLine(lines);
            }

            lines += "\r\n";

            Assert.IsTrue(File.Exists(fileName));

            string outLines = String.Empty;
            using (StreamReader file = new StreamReader(fileName))
            {
                outLines = file.ReadToEnd();
            }

            Assert.AreEqual(outLines, lines);
        }

        [TestMethod]
        public void TestSaveMessage()
        {
            string logContent = "Line of log data.";

            SCLog.WriteLog(logContent);

            Assert.IsTrue(File.Exists(SCLog.FileName));

            long fileSize = 0L;

            using (FileStream fileStream = File.Open(SCLog.FileName, FileMode.Open))
            {
                fileSize = fileStream.Length;
            }

            SCLog.WriteLog(logContent);

            using (FileStream fileStream = File.Open(SCLog.FileName, FileMode.Open))
            {
                Assert.IsTrue(fileStream.Length > fileSize);
            }
        }
    }
}
