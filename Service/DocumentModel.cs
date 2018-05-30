using System;

namespace SwarmClientVS.Domain.Service
{
    public class DocumentModel
    {
        public string CurrentLine { get; set; }
        public int CurrentLineNumber { get; set; }
        public string Namespace { get; set; }
        public int StartLineText { get; set; }
        public int EndLineText { get; set; }
        public string FilePath { get; set; }
        public string FileText { get; set; }
    }
}