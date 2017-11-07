using System;

namespace SwarmClientVS.Domain.Service
{
    public class DocumentModel
    {
        public string CurrentLine { get; set; }
        public int CurrentLineNumber { get; set; }
        public string Namespace { get; set; }
    }
}