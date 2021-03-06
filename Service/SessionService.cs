﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SwarmClientVS.Domain.IRepository;
using SwarmClientVS.Domain.DataModel;
using SwarmClientVS.DataLog.FileLog;
using System.IO.Compression;

namespace SwarmClientVS.Domain.Service
{
    public static class SessionService
    {
        private static IRepository<IData> Repository = new RepositoryLog();
        private static SessionData CurrentSession { get; set; }

        private static List<BreakpointModel> currentBreakpointsList = new List<BreakpointModel>();
        private static List<BreakpointModel> dataBreakpointsList = new List<BreakpointModel>();
        private static List<CodeFileModel> codeFilesList = new List<CodeFileModel>();
        private static bool addedBreakpoints = false;
        private static bool alreadyAddedFirstPathnode = false;


        public static void RegisterAlreadyAddedBreakpoints(List<BreakpointModel> breakpoints)
        {
            if (CurrentSession == null)
                return;

            if (addedBreakpoints)
                return;

            foreach (BreakpointModel item in breakpoints)
            {
                currentBreakpointsList.Add(item);
                dataBreakpointsList.Add(item);

                EventData eventData = new EventData
                {
                    Id = Guid.NewGuid(),
                    EventKind = EventKind.BreakpointAdd.ToString(),
                    Detail = item.Name,
                    Namespace = PathNodeItemModel.GeNamespaceName(item.FunctionName),
                    Type = PathNodeItemModel.GeTypeName(item.FunctionName),
                    TypeFullPath = "TODO",
                    Method = PathNodeItemModel.GetMethodName(item.FunctionName),
                    MethodKey = String.Empty,
                    MethodSignature = item.FunctionName,
                    CharStart = item.StartLineText,
                    CharEnd = item.DocumentModel.EndLineText,
                    CodeFilePath = item.DocumentModel.FilePath,
                    LineNumber = item.DocumentModel.CurrentLineNumber,
                    LineOfCode = item.DocumentModel.CurrentLine,
                    Created = DateTime.Now
                };

                BreakpointData breakpointData = new BreakpointData
                {
                    Id = Guid.NewGuid(),
                    BreakpointKind = BreakpointKind.Line.ToString(),
                    Namespace = PathNodeItemModel.GeNamespaceName(item.FunctionName),
                    Type = PathNodeItemModel.GeTypeName(item.FunctionName),
                    CodeFilePath = item.DocumentModel.FilePath,
                    LineNumber = item.FileLine,
                    LineOfCode = item.DocumentModel.CurrentLine,
                    Origin = BreakpointOrigin.AddedBeforeDebug.ToString(),
                    Created = DateTime.Now
                };

                CurrentSession.Events.Add(eventData);
                CurrentSession.Breakpoints.Add(breakpointData);

                Repository.Save(CurrentSession);

                addedBreakpoints = true;
            }

            RegisterCodeFile(breakpoints.GroupBy(b => b.DocumentModel.FilePath).Select(b => b.First()).Select(b => new CodeFileModel
            {
                Path = b.DocumentModel.FilePath,
                Text = b.DocumentModel.FileText
            }).ToList());
        }

        public static void VerifyBreakpointAddedOne(List<BreakpointModel> breakpoints)
        {
            if (CurrentSession == null)
                return;

            List<BreakpointModel> newBreakpointsList = breakpoints.Where(n => !currentBreakpointsList.Any(o => o.Name == n.Name)).ToList();

            foreach (BreakpointModel item in newBreakpointsList)
            {
                currentBreakpointsList.Add(item);

                EventData eventData = new EventData
                {
                    Id = Guid.NewGuid(),
                    EventKind = EventKind.BreakpointAdd.ToString(),
                    Detail = item.Name,
                    Namespace = PathNodeItemModel.GeNamespaceName(item.FunctionName),
                    Type = PathNodeItemModel.GeTypeName(item.FunctionName),
                    TypeFullPath = "TODO",
                    Method = PathNodeItemModel.GetMethodName(item.FunctionName),
                    MethodKey = String.Empty,
                    MethodSignature = item.FunctionName,
                    CharStart = item.StartLineText,
                    CharEnd = item.DocumentModel.EndLineText,
                    CodeFilePath = item.DocumentModel.FilePath,
                    LineNumber = item.DocumentModel.CurrentLineNumber,
                    LineOfCode = item.DocumentModel.CurrentLine,
                    Created = DateTime.Now
                };

                CurrentSession.Events.Add(eventData);
                Repository.Save(CurrentSession);
            }

            List<BreakpointModel> newBreakpointsListData = breakpoints.Where(n => !dataBreakpointsList.Any(o => o.Name == n.Name)).ToList();

            foreach (BreakpointModel item in newBreakpointsListData)
            {
                dataBreakpointsList.Add(item);

                BreakpointData breakpointData = new BreakpointData
                {
                    Id = Guid.NewGuid(),
                    BreakpointKind = BreakpointKind.Line.ToString(),
                    Namespace = PathNodeItemModel.GeNamespaceName(item.FunctionName),
                    Type = PathNodeItemModel.GeTypeName(item.FunctionName),
                    CodeFilePath = item.DocumentModel.FilePath,
                    LineNumber = item.FileLine,
                    LineOfCode = item.DocumentModel.CurrentLine,
                    Origin = BreakpointOrigin.AddedDuringDebug.ToString(),
                    Created = DateTime.Now
                };

                CurrentSession.Breakpoints.Add(breakpointData);
                Repository.Save(CurrentSession);
            }

            RegisterCodeFile(breakpoints.Where(n => !codeFilesList.Any(o => o.Path == n.DocumentModel.FilePath)).Select(b => new CodeFileModel
            {
                Path = b.DocumentModel.FilePath,
                Text = b.DocumentModel.FileText
            }).ToList());
        }

        private static void RegisterCodeFile(List<CodeFileModel> codeFileModelList)
        {
            foreach (CodeFileModel item in codeFileModelList)
            {
                codeFilesList.Add(item);

                CodeFileData codeFileData = new CodeFileData
                {
                    Id = Guid.NewGuid(),
                    Path = item.Path,
                    Content = Base64StringZip.ZipString(item.Text),
                    Created = DateTime.Now
                };

                CurrentSession.CodeFiles.Add(codeFileData);
                Repository.Save(CurrentSession);
            }
        }

        public static void VerifyBreakpointRemovedOne(List<BreakpointModel> breakpoints)
        {
            if (CurrentSession == null)
                return;

            List<BreakpointModel> newBreakpointsList = currentBreakpointsList.Where(n => !breakpoints.Any(o => o.Name == n.Name)).ToList();

            foreach (BreakpointModel item in newBreakpointsList)
            {
                currentBreakpointsList.Remove(item);

                EventData eventData = new EventData
                {
                    Id = Guid.NewGuid(),
                    EventKind = EventKind.BreakpointRemove.ToString(),
                    Detail = item.Name,
                    Namespace = PathNodeItemModel.GeNamespaceName(item.FunctionName),
                    Type = PathNodeItemModel.GeTypeName(item.FunctionName),
                    TypeFullPath = "TODO",
                    Method = PathNodeItemModel.GetMethodName(item.FunctionName),
                    MethodKey = String.Empty,
                    MethodSignature = item.FunctionName,
                    CharStart = item.StartLineText,
                    CharEnd = item.DocumentModel.EndLineText,
                    CodeFilePath = item.DocumentModel.FilePath,
                    LineNumber = item.DocumentModel.CurrentLineNumber,
                    LineOfCode = item.DocumentModel.CurrentLine,
                    Created = DateTime.Now
                };

                CurrentSession.Events.Add(eventData);
                Repository.Save(CurrentSession);
            }
        }

        public static void RegisterHitted(StepModel sessionModel, PathNodeModel pathNodeModel)
        {
            if (CurrentSession == null)
                return;

            EventData eventData = new EventData
            {
                Id = Guid.NewGuid(),
                EventKind = EventKind.BreakpointHitted.ToString(),
                Detail = sessionModel.BreakpointLastHitName,
                Namespace = PathNodeItemModel.GeNamespaceName(sessionModel.CurrentStackFrameFunctionName),
                Type = PathNodeItemModel.GeTypeName(sessionModel.CurrentStackFrameFunctionName),
                TypeFullPath = "TODO",
                Method = PathNodeItemModel.GetMethodName(sessionModel.CurrentStackFrameFunctionName),
                MethodKey = String.Empty,
                MethodSignature = sessionModel.CurrentStackFrameFunctionName,
                CharStart = sessionModel.CurrentDocument.StartLineText,
                CharEnd = sessionModel.CurrentDocument.EndLineText,
                CodeFilePath = sessionModel.CurrentDocument.FilePath,
                LineNumber = sessionModel.CurrentDocument.CurrentLineNumber,
                LineOfCode = sessionModel.CurrentDocument.CurrentLine,
                Created = DateTime.Now
            };

            CurrentSession.Events.Add(eventData);
            Repository.Save(CurrentSession);

            foreach (var item in pathNodeModel.StackTraceItems)
            {
                item.Event_Id = eventData.Id;
            }

            RegisterFirstAndBreakpointPathNode(pathNodeModel);
        }

        public static void RegisterStep(StepModel sessionModel, PathNodeModel pathNodeModel)
        {
            if (CurrentSession == null)
                return;

            EventData eventData = new EventData
            {
                Id = Guid.NewGuid(),
                EventKind = ((EventKind)sessionModel.CurrentCommandStep).ToString(),
                Detail = "TODO",
                Namespace = PathNodeItemModel.GeNamespaceName(sessionModel.CurrentStackFrameFunctionName),
                Type = PathNodeItemModel.GeTypeName(sessionModel.CurrentStackFrameFunctionName),
                TypeFullPath = "TODO",
                Method = PathNodeItemModel.GetMethodName(sessionModel.CurrentStackFrameFunctionName),
                MethodKey = String.Empty,
                MethodSignature = sessionModel.CurrentStackFrameFunctionName,
                CharStart = sessionModel.CurrentDocument.StartLineText,
                CharEnd = sessionModel.CurrentDocument.EndLineText,
                CodeFilePath = sessionModel.CurrentDocument.FilePath,
                LineNumber = sessionModel.CurrentDocument.CurrentLineNumber,
                LineOfCode = sessionModel.CurrentDocument.CurrentLine,
                Created = DateTime.Now
            };

            CurrentSession.Events.Add(eventData);
            Repository.Save(CurrentSession);

            if (!codeFilesList.Any(o => o.Path == sessionModel.CurrentDocument.FilePath))
                RegisterCodeFile(new List<CodeFileModel>
                { new CodeFileModel()
                    {
                        Path = sessionModel.CurrentDocument.FilePath,
                        Text = sessionModel.CurrentDocument.FileText
                    }
                });

            foreach (var item in pathNodeModel.StackTraceItems)
            {
                item.Event_Id = eventData.Id;
            }

            RegisterStepIntoPathNode(pathNodeModel);
        }

        private static void RegisterFirstAndBreakpointPathNode(PathNodeModel pathNodeModel)
        {
            if (alreadyAddedFirstPathnode)
            {
                RegisterPathNode(pathNodeModel.StackTraceItems, PathNodeOrigin.Breakpoint);
            }
            else
            {
                AddNodeAsOf(0, pathNodeModel.StackTraceItems, PathNodeOrigin.Breakpoint);

                alreadyAddedFirstPathnode = true;
            }
        }

        private static void RegisterStepIntoPathNode(PathNodeModel pathNodeModel)
        {
            if (pathNodeModel.CurrentCommandStep == null)
                return;

            //TODO: remove later
            //if (pathNodeModel.CurrentCommandStep != CurrentCommandStep.StepInto)
            //    return;

            //TODO: improve this code
            PathNodeOrigin pathNodeOrigin = PathNodeOrigin.StepInto;

            if (pathNodeModel.CurrentCommandStep == CurrentCommandStep.StepOver)
                pathNodeOrigin = PathNodeOrigin.StepOver;

            if (pathNodeModel.CurrentCommandStep == CurrentCommandStep.StepOut)
                pathNodeOrigin = PathNodeOrigin.StepOut;
            //---

            RegisterPathNode(pathNodeModel.StackTraceItems, pathNodeOrigin);
        }

        private static void RegisterPathNode(List<PathNodeItemModel> stackTraceItems, PathNodeOrigin pathNodeOrigin)
        {
            if (CurrentSession == null)
                return;

            for (int i = 0; i < stackTraceItems.Count; i++)
            {
                if (i >= CurrentSession.PathNodes.Count)
                {
                    AddNodeAsOf(i, stackTraceItems, pathNodeOrigin);
                    break;
                }
                else if (!stackTraceItems[i].StackName.Equals(CurrentSession.PathNodes[i].GetStackTrace()))
                {
                    AddNodeAsOf(i, stackTraceItems, pathNodeOrigin);
                    break;
                }
                else if (i == stackTraceItems.Count - 1)
                {
                    AddNodeAsOf(i, stackTraceItems, pathNodeOrigin);
                    break;
                }
            }
        }

        private static void AddNodeAsOf(int start, List<PathNodeItemModel> stackTrace, PathNodeOrigin pathNodeOrigin)
        {
            if (CurrentSession == null)
                return;

            for (int i = start; i < stackTrace.Count; i++)
            {
                CurrentSession.PathNodes.Add(new PathNodeData
                {
                    Id = Guid.NewGuid(),
                    Hash = PathNodeItemModel.GetHash(CurrentSession.GetCleanProjectName(), stackTrace[i].StackName),
                    Method = PathNodeItemModel.GetMethodName(stackTrace[i].StackName),
                    Created = DateTime.Now,
                    Namespace = PathNodeItemModel.GeNamespaceName(stackTrace[i].StackName),
                    Parent = i == 0 ? null : PathNodeItemModel.GetHash(CurrentSession.GetCleanProjectName(), stackTrace[i - 1].StackName),
                    Parent_Id = i == 0 ? Guid.Empty : CurrentSession.PathNodes.Last(pn => pn.Hash == PathNodeItemModel.GetHash(CurrentSession.GetCleanProjectName(), stackTrace[i - 1].StackName)).Id,
                    Type = PathNodeItemModel.GeTypeName(stackTrace[i].StackName),
                    ReturnType = stackTrace[i].ReturnType,
                    Parameters = stackTrace[i].Parameters.Select(x => new PathNodeParameterData
                    {
                        Id = Guid.NewGuid(),
                        Type = x.Type,
                        Name = x.Name,
                        Value = x.Value
                    }).ToList(),
                    Origin = i == stackTrace.Count - 1 ? pathNodeOrigin.ToString() : PathNodeOrigin.Trace.ToString(),
                    Event_Id = stackTrace[i].Event_Id
                });

                Repository.Save(CurrentSession);
            }
        }

        public static void RegisterNewSession()
        {
            if (CurrentSession != null)
                return;

            SessionInputService sessionInputService = new SessionInputService(new RepositoryLog(), String.Empty);
            SessionInputData sessionInputData = sessionInputService.GetInputData();

            if (!sessionInputData.EnableMonitoring)
                return;

            CurrentSession = new SessionData
            {
                Id = Guid.NewGuid(),
                Description = String.Empty,
                Started = DateTime.Now
            };

            Repository.GenerateIdentifier();
            Repository.Save(CurrentSession);

            CurrentSession.TaskName = (sessionInputData.Task.LastOrDefault() ?? new TaskInputData { Name = String.Empty }).Name;
            CurrentSession.TaskDescription = (sessionInputData.Task.LastOrDefault() ?? new TaskInputData { Description = String.Empty }).Description;
            CurrentSession.TaskAction = (sessionInputData.Task.LastOrDefault() ?? new TaskInputData { Action = TaskAction.ResolvingBug.ToString() }).Action;
            CurrentSession.TaskCreated = (sessionInputData.Task.LastOrDefault() ?? new TaskInputData { Created = DateTime.Now }).Created;
            CurrentSession.ProjectName = sessionInputData.Project;
            CurrentSession.DeveloperName = sessionInputData.Developer;

            Repository.Save(CurrentSession);

            codeFilesList = new List<CodeFileModel>();
            addedBreakpoints = false;
            alreadyAddedFirstPathnode = false;
        }

        public static void EndCurrentSession()
        {
            if (CurrentSession == null)
                return;

            CurrentSession.Finished = DateTime.Now;

            Repository.Save(CurrentSession);

            CurrentSession = null;
            codeFilesList = new List<CodeFileModel>();
            addedBreakpoints = false;
            alreadyAddedFirstPathnode = false;
        }
    }
}