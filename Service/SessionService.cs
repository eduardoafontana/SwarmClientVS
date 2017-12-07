using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SwarmClientVS.Domain.IRepository;
using SwarmClientVS.Domain.DataModel;
using SwarmClientVS.DataLog.FileLog;

namespace SwarmClientVS.Domain.Service
{
    public static class SessionService
    {
        private static IRepository<IData> Repository = new RepositoryLog();
        private static SessionData CurrentSession { get; set; }

        private static List<BreakpointModel> currentBreakpointsList = new List<BreakpointModel>();
        private static List<BreakpointModel> dataBreakpointsList = new List<BreakpointModel>();
        private static bool addedBreakpoints = false;
        private static bool addedPathNode = false;


        public static void RegisterAlreadyAddedBreakpoints(List<BreakpointModel> breakpoints)
        {
            if (addedBreakpoints)
                return;

            foreach (BreakpointModel item in breakpoints)
            {
                currentBreakpointsList.Add(item);
                dataBreakpointsList.Add(item);

                EventData eventData = new EventData
                {
                    EventKind = EventKind.BreakpointAdd.ToString(),
                    Detail = item.Name,
                    Namespace = PathNodeModel.GeNamespaceName(item.FunctionName),
                    Type = PathNodeModel.GeTypeName(item.FunctionName),
                    TypeFullPath = "TODO",
                    Method = PathNodeModel.GetMethodName(item.FunctionName),
                    MethodKey = String.Empty,
                    MethodSignature = item.FunctionName,
                    CharStart = item.StartLineText,
                    CharEnd = item.DocumentModel.EndLineText,
                    LineNumber = item.DocumentModel.CurrentLineNumber,
                    LineOfCode = item.DocumentModel.CurrentLine,
                    Created = DateTime.Now
                };

                BreakpointData breakpointData = new BreakpointData
                {
                    BreakpointKind = BreakpointKind.Line.ToString(),
                    Namespace = PathNodeModel.GeNamespaceName(item.FunctionName),
                    Type = PathNodeModel.GeTypeName(item.FunctionName),
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
                    EventKind = EventKind.BreakpointAdd.ToString(),
                    Detail = item.Name,
                    Namespace = PathNodeModel.GeNamespaceName(item.FunctionName),
                    Type = PathNodeModel.GeTypeName(item.FunctionName),
                    TypeFullPath = "TODO",
                    Method = PathNodeModel.GetMethodName(item.FunctionName),
                    MethodKey = String.Empty,
                    MethodSignature = item.FunctionName,
                    CharStart = item.StartLineText,
                    CharEnd = item.DocumentModel.EndLineText,
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
                    BreakpointKind = BreakpointKind.Line.ToString(),
                    Namespace = PathNodeModel.GeNamespaceName(item.FunctionName),
                    Type = PathNodeModel.GeTypeName(item.FunctionName),
                    LineNumber = item.FileLine,
                    LineOfCode = item.DocumentModel.CurrentLine,
                    Origin = BreakpointOrigin.AddedDuringDebug.ToString(),
                    Created = DateTime.Now
                };

                CurrentSession.Breakpoints.Add(breakpointData);
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
                    EventKind = EventKind.BreakpointRemove.ToString(),
                    Detail = item.Name,
                    Namespace = PathNodeModel.GeNamespaceName(item.FunctionName),
                    Type = PathNodeModel.GeTypeName(item.FunctionName),
                    TypeFullPath = "TODO",
                    Method = PathNodeModel.GetMethodName(item.FunctionName),
                    MethodKey = String.Empty,
                    MethodSignature = item.FunctionName,
                    CharStart = item.StartLineText,
                    CharEnd = item.DocumentModel.EndLineText,
                    LineNumber = item.DocumentModel.CurrentLineNumber,
                    LineOfCode = item.DocumentModel.CurrentLine,
                    Created = DateTime.Now
                };

                CurrentSession.Events.Add(eventData);
                Repository.Save(CurrentSession);
            }
        }

        public static void RegisterHitted(StepModel sessionModel)
        {
            EventData eventData = new EventData
            {
                EventKind = EventKind.BreakpointHitted.ToString(),
                Detail = sessionModel.BreakpointLastHitName,
                Namespace = PathNodeModel.GeNamespaceName(sessionModel.CurrentStackFrameFunctionName),
                Type = PathNodeModel.GeTypeName(sessionModel.CurrentStackFrameFunctionName),
                TypeFullPath = "TODO",
                Method = PathNodeModel.GetMethodName(sessionModel.CurrentStackFrameFunctionName),
                MethodKey = String.Empty,
                MethodSignature = sessionModel.CurrentStackFrameFunctionName,
                CharStart = sessionModel.CurrentDocument.StartLineText,
                CharEnd = sessionModel.CurrentDocument.EndLineText,
                LineNumber = sessionModel.CurrentDocument.CurrentLineNumber,
                LineOfCode = sessionModel.CurrentDocument.CurrentLine,
                Created = DateTime.Now
            };

            CurrentSession.Events.Add(eventData);
            Repository.Save(CurrentSession);
        }

        public static void RegisterStep(StepModel sessionModel)
        {
            EventData eventData = new EventData
            {
                EventKind = ((EventKind)sessionModel.CurrentCommandStep).ToString(),
                Detail = "TODO",
                Namespace = PathNodeModel.GeNamespaceName(sessionModel.CurrentStackFrameFunctionName),
                Type = PathNodeModel.GeTypeName(sessionModel.CurrentStackFrameFunctionName),
                TypeFullPath = "TODO",
                Method = PathNodeModel.GetMethodName(sessionModel.CurrentStackFrameFunctionName),
                MethodKey = String.Empty,
                MethodSignature = sessionModel.CurrentStackFrameFunctionName,
                CharStart = sessionModel.CurrentDocument.StartLineText,
                CharEnd = sessionModel.CurrentDocument.EndLineText,
                LineNumber = sessionModel.CurrentDocument.CurrentLineNumber,
                LineOfCode = sessionModel.CurrentDocument.CurrentLine,
                Created = DateTime.Now
            };

            CurrentSession.Events.Add(eventData);
            Repository.Save(CurrentSession);
        }

        public static void RegisterStartPathNode(PathNodeModel pathNodeModel)
        {
            if (addedPathNode)
                return;

            AddNodeAsOf(0, pathNodeModel.StackTraceItems, PathNodeOrigin.Breakpoint);

            addedPathNode = true;
        }

        public static void RegisterPathNode(PathNodeModel pathNodeModel)
        {
            if (pathNodeModel.CurrentCommandStep != CurrentCommandStep.StepInto)
                return;

            for (int i = 0; i < pathNodeModel.StackTraceItems.Count; i++)
            {
                if (i >= CurrentSession.PathNodes.Count)
                {
                    AddNodeAsOf(i, pathNodeModel.StackTraceItems, PathNodeOrigin.StepInto);
                    break;
                }
                else if (!pathNodeModel.StackTraceItems[i].Equals(CurrentSession.PathNodes[i].GetStackTrace()))
                {
                    AddNodeAsOf(i, pathNodeModel.StackTraceItems, PathNodeOrigin.StepInto);
                    break;
                }
                else if (i == pathNodeModel.StackTraceItems.Count - 1)
                {
                    AddNodeAsOf(i, pathNodeModel.StackTraceItems, PathNodeOrigin.StepInto);
                    break;
                }
            }
        }

        private static void AddNodeAsOf(int start, List<PathNodeItemModel> stackTrace, PathNodeOrigin pathNodeOrigin)
        {
            for (int i = start; i < stackTrace.Count; i++)
            {
                CurrentSession.PathNodes.Add(new PathNodeData
                {
                    Method = PathNodeModel.GetMethodName(stackTrace[i].StackName),
                    Created = DateTime.Now,
                    Namespace = PathNodeModel.GeNamespaceName(stackTrace[i].StackName),
                    Parent = i == 0 ? null : PathNodeModel.GetMethodName(stackTrace[i - 1].StackName),
                    Type = PathNodeModel.GeTypeName(stackTrace[i].StackName),
                    ReturnType = stackTrace[i].ReturnType,
                    Origin = i == stackTrace.Count - 1 ? pathNodeOrigin.ToString() : PathNodeOrigin.Trace.ToString()
                });

                Repository.Save(CurrentSession);
            }
        }

        public static void RegisterNewSession()
        {
            if (CurrentSession != null)
                return;

            CurrentSession = new SessionData
            {
                Description = "TODO",
                Label = "TODO",
                Purpose = "TODO",
                Started = DateTime.Now
            };

            Repository.GenerateIdentifier();
            Repository.Save(CurrentSession);

            SessionInputService sessionInputService = new SessionInputService(new RepositoryLog(), String.Empty);
            SessionInputData sessionInputData = sessionInputService.GetInputData();

            CurrentSession.Task = new TaskData
            {
                Name = (sessionInputData.Task.LastOrDefault() ?? new TaskData { Name = String.Empty }).Name,
                Description = (sessionInputData.Task.LastOrDefault() ?? new TaskData { Description = String.Empty }).Description,
                Action = (sessionInputData.Task.LastOrDefault() ?? new TaskData { Action = TaskAction.ResolvingBug.ToString() }).Action,
                Created = (sessionInputData.Task.LastOrDefault() ?? new TaskData { Created = DateTime.Now }).Created,
                Project = new ProjectData
                {
                    Name = sessionInputData.Project,
                    Description = String.Empty
                }
            };

            Repository.Save(CurrentSession);

            CurrentSession.Developer = new DeveloperData
            {
                Name = sessionInputData.Developer
            };

            Repository.Save(CurrentSession);

            addedBreakpoints = false;
            addedPathNode = false;
        }

        public static void EndCurrentSession()
        {
            CurrentSession.Finished = DateTime.Now;

            Repository.Save(CurrentSession);

            CurrentSession = null;
            addedBreakpoints = false;
            addedPathNode = false;
        }
    }
}