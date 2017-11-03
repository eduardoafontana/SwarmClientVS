using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.DataLog.FileLog
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

        //TODO: notes for next requirement that logs on literal text file.
        //Old Session log way
        //String.Format("Started new session, {0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString())
    }
}
