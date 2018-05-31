using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.Service
{
    public class Base64StringZip
    {
        public static string ZipString(string text)
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

        public static string UnZipString(string compressedText)
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
    }
}
