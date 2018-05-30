﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using SwarmClientVS.Domain.Service;

namespace SwarmClientVS
{
    public class DocumentModelBuilder
    {
        public static DocumentModel Build(string file, int fileLine, int fileColumn)
        {
            DocumentModel documentModel = new DocumentModel
            {
                CurrentLine = "Fail to get line",
                CurrentLineNumber = -1,
                Namespace = "Fail to get namespace",
                FilePath = file
            };

            documentModel.CurrentLineNumber = fileLine;

            string activeFilePath = Path.Combine(file);

            documentModel.CurrentLine = TryGetCurrentLineCode(activeFilePath, fileLine);

            string namespaceLine = File.ReadLines(activeFilePath).Where(p => p.IndexOf("namespace") >= 0).FirstOrDefault();
            if (String.IsNullOrEmpty(namespaceLine))
                documentModel.Namespace += ", namespace word not found.";

            documentModel.Namespace = TryGetCurrentNameSpace(namespaceLine);
            documentModel.EndLineText = fileColumn + documentModel.CurrentLine.Length;

            return documentModel;
        }

        public static DocumentModel Build(Document currentDocument)
        {
            DocumentModel documentModel = new DocumentModel
            {
                CurrentLine = "Fail to get line",
                CurrentLineNumber = -1,
                Namespace = "Fail to get namespace",
                FilePath = "Failt to get file path"
            };

            if (currentDocument == null)
            {
                documentModel.CurrentLine += ", document null.";
                documentModel.Namespace += ", document null.";
                documentModel.FilePath += ", document null.";
                return documentModel;
            }

            documentModel.FilePath = currentDocument.Path;

            TextSelection textSelection = (TextSelection)currentDocument.Selection;

            if (textSelection == null)
            {
                documentModel.CurrentLine += ", textselecion null.";
                documentModel.Namespace += ", textselecion null.";
                return documentModel;
            }

            documentModel.CurrentLineNumber = textSelection.ActivePoint.Line;

            string activeFilePath = Path.Combine(currentDocument.Path, currentDocument.Name);

            documentModel.CurrentLine = TryGetCurrentLineCode(activeFilePath, textSelection.ActivePoint.Line);

            string namespaceLine = File.ReadLines(activeFilePath).Where(p => p.IndexOf("namespace") >= 0).FirstOrDefault();
            if (String.IsNullOrEmpty(namespaceLine))
                documentModel.Namespace += ", namespace word not found.";

            documentModel.Namespace = TryGetCurrentNameSpace(namespaceLine);
            documentModel.StartLineText = textSelection.CurrentColumn;
            documentModel.EndLineText = textSelection.ActivePoint.LineLength;

            return documentModel;
        }

        private static string TryGetCurrentNameSpace(string namespaceLine)
        {
            try
            {
                int namespaceWordPosition = namespaceLine.IndexOf("namespace");
                return namespaceLine.Substring(namespaceWordPosition + 9, namespaceLine.Length - (namespaceWordPosition + 9)).Trim();
            }
            catch
            {
                return "Fatal error to get namespace in file.";
            }
        }

        private static string TryGetCurrentLineCode(string activeFilePath, int pointLine)
        {
            try
            {
                return File.ReadLines(activeFilePath).Skip(pointLine - 1).Take(1).First().Trim();
            }
            catch
            {
                return "Fatal error to get line code in file.";
            }
        }
    }
}
