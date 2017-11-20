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
        private static ISessionData CurrentSession { get; set; }

        private static List<BreakpointModel> currentBreakpointsList = new List<BreakpointModel>();
        private static List<BreakpointModel> dataBreakpointsList = new List<BreakpointModel>();

        public static void RegisterSessionInformation(SessionModel sessionModel)
        {
            CurrentSession.Task = new TaskData
            {
                Name = sessionModel.Task,
                Description = sessionModel.TaskDescription,
                Project = new ProjectData
                {
                    Name = sessionModel.Project,
                    Description = sessionModel.ProjectDescription
                }
            };

            CurrentSession.Developer = new DeveloperData
            {
                Name = sessionModel.Developer
            };

            Repository.Save(CurrentSession);
        }

        public static void RegisterAlreadyAddedBreakpoints(List<BreakpointModel> breakpoints)
        {
            foreach (BreakpointModel item in breakpoints)
            {
                currentBreakpointsList.Add(item);
                dataBreakpointsList.Add(item);

                IEventData eventData = new EventData
                {
                    EventKind = EventKind.BreakpointAdd.ToString(),
                    Detail = item.Name,
                    Namespace = item.DocumentModel.Namespace,
                    Type = "TODO",
                    TypeFullPath = "TODO",
                    Method = item.FunctionName,
                    MethodKey = String.Empty,
                    MethodSignature = "TODO",
                    CharStart = item.StartLineText,
                    CharEnd = item.DocumentModel.EndLineText,
                    LineNumber = item.DocumentModel.CurrentLineNumber,
                    LineOfCode = item.DocumentModel.CurrentLine,
                    Created = DateTime.Now
                };

                BreakpointData breakpointData = new BreakpointData
                {
                    BreakpointKind = BreakpointKind.Line.ToString(),
                    Namespace = item.DocumentModel.Namespace,
                    Type = "AlreadyAdded",
                    LineNumber = item.FileLine,
                    LineOfCode = item.DocumentModel.CurrentLine,
                    Created = DateTime.Now
                };

                CurrentSession.Events.Add(eventData);
                CurrentSession.Breakpoints.Add(breakpointData);
                Repository.Save(CurrentSession);
            }
        }

        public static void VerifyBreakpointAddedOne(List<BreakpointModel> breakpoints)
        {
            List<BreakpointModel> newBreakpointsList = breakpoints.Where(n => !currentBreakpointsList.Any(o => o.Name == n.Name)).ToList();

            foreach (BreakpointModel item in newBreakpointsList)
            {
                currentBreakpointsList.Add(item);

                IEventData eventData = new EventData
                {
                    EventKind = EventKind.BreakpointAdd.ToString(),
                    Detail = item.Name,
                    Namespace = item.DocumentModel.Namespace,
                    Type = "TODO",
                    TypeFullPath = "TODO",
                    Method = item.FunctionName,
                    MethodKey = String.Empty,
                    MethodSignature = "TODO",
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
                    Namespace = item.DocumentModel.Namespace,
                    Type = "Added",
                    LineNumber = item.FileLine,
                    LineOfCode = item.DocumentModel.CurrentLine,
                    Created = DateTime.Now
                };

                CurrentSession.Breakpoints.Add(breakpointData);
                Repository.Save(CurrentSession);
            }
        }

        public static void VerifyBreakpointRemovedOne(List<BreakpointModel> breakpoints)
        {
            List<BreakpointModel> newBreakpointsList = currentBreakpointsList.Where(n => !breakpoints.Any(o => o.Name == n.Name)).ToList();

            foreach (BreakpointModel item in newBreakpointsList)
            {
                currentBreakpointsList.Remove(item);

                IEventData eventData = new EventData
                {
                    EventKind = EventKind.BreakpointRemove.ToString(),
                    Detail = item.Name,
                    Namespace = item.DocumentModel.Namespace,
                    Type = "TODO",
                    TypeFullPath = "TODO",
                    Method = item.FunctionName,
                    MethodKey = String.Empty,
                    MethodSignature = "TODO",
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
            IEventData eventData = new EventData
            {
                EventKind = EventKind.BreakpointHitted.ToString(),
                Detail = sessionModel.BreakpointLastHitName,
                Namespace = sessionModel.CurrentDocument.Namespace,
                Type = "TODO",
                TypeFullPath = "TODO",
                Method = sessionModel.CurrentStackFrameFunctionName,
                MethodKey = String.Empty,
                MethodSignature = "TODO",
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
            IEventData eventData = new EventData
            {
                EventKind = ((EventKind)sessionModel.CurrentCommandStep).ToString(),
                Detail = "TODO",
                Namespace = sessionModel.CurrentDocument.Namespace,
                Type = "TODO",
                TypeFullPath = "TODO",
                Method = sessionModel.CurrentStackFrameFunctionName,
                MethodKey = String.Empty,
                MethodSignature = "TODO",
                CharStart = sessionModel.CurrentDocument.StartLineText,
                CharEnd = sessionModel.CurrentDocument.EndLineText,
                LineNumber = sessionModel.CurrentDocument.CurrentLineNumber,
                LineOfCode = sessionModel.CurrentDocument.CurrentLine,
                Created = DateTime.Now
            };

            CurrentSession.Events.Add(eventData);
            Repository.Save(CurrentSession);

            if (!(CurrentSession.PathNodes.LastOrDefault() ?? new PathNodeData { Method = String.Empty }).Method.Equals(sessionModel.CurrentStackFrameFunctionName))
            {
                CurrentSession.PathNodes.Add(new PathNodeData
                {
                    Method = sessionModel.CurrentStackFrameFunctionName,
                    Created = DateTime.Now,
                    Namespace = sessionModel.CurrentDocument.Namespace,
                    Type = "TODO"
                });

                Repository.Save(CurrentSession);
            }
        }

        public static void RegisterNewSession()
        {
            CurrentSession = new SessionData
            {
                Description = "Description...",
                Label = "Fixed session, not implemented yet.",
                Purpose = "Purpose",
                Started = DateTime.Now
            };

            Repository.GenerateIdentifier();
            Repository.Save(CurrentSession);
        }
    }
}