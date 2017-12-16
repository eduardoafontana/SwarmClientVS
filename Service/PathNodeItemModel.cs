using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.Service
{
    public class PathNodeItemModel
    {
        public string StackName { get; set; }
        public string ReturnType { get; set; }
        public List<PathNodeItemParameterModel> Parameters { get; set; } = new List<PathNodeItemParameterModel>();

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

        public static string GetTypeAndMethodName(string stackTracePath)
        {
            string[] pieces = stackTracePath.Split('.');

            if (pieces.Length == 0)
                return String.Empty;

            if (pieces.Length == 1)
                return stackTracePath;

            if (pieces.Length > 1)
                return String.Format("{0}.{1}", pieces[pieces.Length - 2], pieces.Last());//type.method

            return String.Empty;
        }

        public static string GetHash(string project, string stackTracePath)
        {
            return String.Format("{0}.{1}", project, stackTracePath);
        }

        public static string GetHash(string project, string pNamespace, string pType, string method)
        {
            return String.Format("{0}.{1}.{2}.{3}", project, pNamespace, pType, method);
        }
    }
}
