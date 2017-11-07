﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SwarmClientVS.Domain.IRepository;
using SwarmClientVS.Domain.DataModel;

namespace SwarmClientVS.Domain.Service
{
    public class SessionService
    {
        private IRepository<IData> Repository { get; set; }

        public SessionService(IRepository<IData> repository)
        {
            Repository = repository;
        }

        private List<BreakpointModel> currentBreakpointsList = new List<BreakpointModel>();

        public void RegisterAlreadyAddedBreakpoints(List<BreakpointModel> breakpoints)
        {
            foreach (BreakpointModel item in breakpoints)
            {
                BreakpointData breakpointData = new BreakpointData
                {
                    BreakpointKind = BreakpointKind.Line.ToString(),
                    Namespace = "TODO",
                    Type = "AlreadyAdded",
                    LineNumber = item.FileLine,
                    LineOfCode = item.FunctionName + "|" + item.Name,
                    Created = DateTime.Now
                };

                Repository.Save(breakpointData);

                currentBreakpointsList.Add(item);
            }
        }

        public void VerifyBreakpointAddedOne(List<BreakpointModel> breakpoints)
        {
            List<BreakpointModel> newBreakpointsList = breakpoints.Where(n => !currentBreakpointsList.Any(o => o.Name == n.Name)).ToList();

            foreach (BreakpointModel item in newBreakpointsList)
            {
                BreakpointData breakpointData = new BreakpointData
                {
                    BreakpointKind = BreakpointKind.Line.ToString(),
                    Namespace = "TODO",
                    Type = "Added",
                    LineNumber = item.FileLine,
                    LineOfCode = item.FunctionName + "|" + item.Name,
                    Created = DateTime.Now
                };

                Repository.Save(breakpointData);

                currentBreakpointsList.Add(item);
            }
        }

        public void VerifyBreakpointRemovedOne(List<BreakpointModel> breakpoints)
        {
            List<BreakpointModel> newBreakpointsList = currentBreakpointsList.Where(n => !breakpoints.Any(o => o.Name == n.Name)).ToList();

            foreach (BreakpointModel item in newBreakpointsList)
            {
                BreakpointData breakpointData = new BreakpointData
                {
                    BreakpointKind = BreakpointKind.Line.ToString(),
                    Namespace = "TODO",
                    Type = "Removed",
                    LineNumber = item.FileLine,
                    LineOfCode = item.FunctionName + "|" + item.Name,
                    Created = DateTime.Now
                };

                Repository.Save(breakpointData);

                currentBreakpointsList.Remove(item);
            }
        }

        public void RegisterHitted(StepModel sessionModel)
        {           
            IEventData eventData = new EventData
            {
                EventKind = EventKind.BreakpointHitted.ToString(),
                Detail = sessionModel.BreakpointLastHitName,
                Namespace = sessionModel.CurrentDocument.Namespace,
                Type = "TODO",
                TypeFullPath = "TODO",
                Method = sessionModel.CurrentStackFrameFunctionName,
                MethodKey = "TODO",
                MethodSignature = "TODO",
                CharStart = 0,
                CharEnd = 0,
                LineNumber = sessionModel.CurrentDocument.CurrentLineNumber,
                LineOfCode = sessionModel.CurrentDocument.CurrentLine,
                Created = DateTime.Now
            };

            Repository.Save(eventData);
        }

        public void RegisterStep(StepModel sessionModel)
        {
            IEventData eventData = new EventData
            {
                EventKind = ((EventKind)sessionModel.CurrentCommandStep).ToString(),
                Detail = "TODO",
                Namespace = sessionModel.CurrentDocument.Namespace,
                Type = "TODO",
                TypeFullPath = "TODO",
                Method = sessionModel.CurrentStackFrameFunctionName,
                MethodKey = "TODO",
                MethodSignature = "TODO",
                //CharStart = sessionModel.CurrentDocument,
                CharEnd = 0,
                LineNumber = sessionModel.CurrentDocument.CurrentLineNumber,
                LineOfCode = sessionModel.CurrentDocument.CurrentLine,
                Created = DateTime.Now
            };

            Repository.Save(eventData);
        }

        public void RegisterNewSession()
        {           
            ISessionData sessionData = new SessionData
            {
                Description = "Description...",
                Label = "Fixed session, not implemented yet.",
                Purpose = "Purpose",
                Started = DateTime.Now
            };

            Repository.Save(sessionData);
        }
    }
}