using System;
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

        public void VerifyBreakpointAddedOne(List<BreakpointModel> breakpoints)
        {
            List<BreakpointModel> newBreakpointsList = breakpoints.Where(n => !currentBreakpointsList.Any(o => o.Name == n.Name)).ToList();

            foreach (BreakpointModel item in newBreakpointsList)
            {
                //Repository.SaveLog(String.Format("Added: {0}", item.ToString()));
                //SCLog.WriteLog(String.Format("Added: {0}", item.ToString()));

                currentBreakpointsList.Add(item);
            }
        }

        public void VerifyBreakpointRemovedOne(List<BreakpointModel> breakpoints)
        {
            List<BreakpointModel> newBreakpointsList = currentBreakpointsList.Where(n => !breakpoints.Any(o => o.Name == n.Name)).ToList();

            foreach (BreakpointModel item in newBreakpointsList)
            {
                //SCLog.WriteLog(String.Format("Removed: {0}", item.ToString()));

                currentBreakpointsList.Remove(item);
            }
        }

        public void RegisterHitted(SessionModel sessionModel)
        {           
            IEventData eventData = new EventData
            {
                EventKind = EventKind.BreakpointHitted.ToString(),
                Detail = sessionModel.BreakpointLastHitName,
                Namespace = "TODO",
                Type = "TODO",
                TypeFullPath = "TODO",
                Method = sessionModel.CurrentStackFrameFunctionName,
                MethodKey = "TODO",
                MethodSignature = "TODO",
                CharStart = 0,
                CharEnd = 0,
                LineNumber = 0,
                LineOfCode = sessionModel.CurrentDocumentLine,
                Created = DateTime.Now
            };

            Repository.Save(eventData);
        }

        public void RegisterStep(SessionModel sessionModel)
        {           
            IEventData eventData = new EventData
            {
                EventKind = ((EventKind)sessionModel.CurrentCommandStep).ToString(),
                Detail = "TODO",
                Namespace = "TODO",
                Type = "TODO",
                TypeFullPath = "TODO",
                Method = sessionModel.CurrentStackFrameFunctionName,
                MethodKey = "TODO",
                MethodSignature = "TODO",
                CharStart = 0,
                CharEnd = 0,
                LineNumber = 0,
                LineOfCode = sessionModel.CurrentDocumentLine,
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