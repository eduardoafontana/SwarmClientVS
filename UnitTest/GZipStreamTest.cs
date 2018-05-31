using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class GZipStreamTest
    {
        [TestMethod]
        public void ZipTest()
        {
            string string1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            string string2 = "In sollicitudin rutrum nulla. Ut eget libero finibus, ultrices turpis et, consequat ex. ";

            string stringZipped1 = ZipString(string1);
            string stringZipped2 = ZipString(string2);

            Assert.AreNotEqual(stringZipped1, stringZipped2);

            string stringUnzipped1 = UnZipString(stringZipped1);
            string stringUnzipped2 = UnZipString(stringZipped2);

            Assert.AreEqual(string1, stringUnzipped1);
            Assert.AreEqual(string2, stringUnzipped2);
        }

        private string ZipString(string text)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(text);

            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;

            byte[] compressedBuffer = new byte[ms.Length];
            ms.Read(compressedBuffer, 0, compressedBuffer.Length);

            byte[] gzBuffer = new byte[compressedBuffer.Length + 4];
            Buffer.BlockCopy(compressedBuffer, 0, gzBuffer, 4, compressedBuffer.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);

            return Convert.ToBase64String(gzBuffer);
        }

        private string UnZipString(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);

            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                return Encoding.Unicode.GetString(buffer, 0, buffer.Length);
            }
        }

        private string ZipString2(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            var msi = new MemoryStream(bytes);
            var mso = new MemoryStream();

            var gs = new GZipStream(mso, CompressionMode.Compress);
            msi.CopyTo(gs);

            return Convert.ToBase64String(mso.ToArray());
        }
    }
}
