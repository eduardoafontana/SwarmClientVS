using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS
{
    public class SCLog
    {
        public static string FileName { get { return @".\SCLog.txt"; } }

        public static void WriteLog(string logContent)
        {
            using (StreamWriter file = new StreamWriter(FileName, true, Encoding.UTF8))
            {
                file.WriteLine(logContent);

                Debug.WriteLine(logContent);
            }
        }
    }
}
