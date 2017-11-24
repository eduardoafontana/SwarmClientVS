using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.Service
{
    public class PathNodeModel
    {
        public CurrentCommandStep CurrentCommandStep { get; set; }
        public List<string> StackTrace { get; set; }

        public static string GetMethodName(string stackTracePath)
        {
            string[] pieces = stackTracePath.Split('.');

            if (pieces.Length == 0)
                return stackTracePath;

            if (pieces.Length == 1)
                return stackTracePath;

            if (pieces.Length > 1)
                return pieces.Last();

            return stackTracePath;
        }

        public static string GeTypeName(string stackTracePath)
        {
            string[] pieces = stackTracePath.Split('.');

            if (pieces.Length == 0)
                return String.Empty;

            if (pieces.Length == 1)
                return String.Empty;

            if (pieces.Length > 1)
                return pieces[pieces.Length - 2];//last but one

            return String.Empty;
        }

        public static string GeNamespaceName(string stackTracePath)
        {
            string[] pieces = stackTracePath.Split('.');

            if (pieces.Length == 0)
                return String.Empty;

            if (pieces.Length == 1)
                return String.Empty;

            if (pieces.Length == 2)
                return String.Empty;

            if (pieces.Length > 2)
                return String.Join(".", pieces, 0, pieces.Length - 2);

            return String.Empty;
        }
    }
}
