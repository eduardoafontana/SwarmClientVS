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
        private string lastStackFrameFunctionName = String.Empty;

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
            lastStackFrameFunctionName = sessionModel.CurrentStackFrameFunctionName;

            //SCLog.WriteLog(String.Format("Hitted: {0}|{1} : {2}", sessionModel.BreakpointLastHitName, sessionModel.CurrentStackFrameFunctionName, sessionModel.CurrentDocumentLine));

            IEventData eventData = new EventData
            {
                LineOfCode = sessionModel.CurrentDocumentLine,
                Detail = sessionModel.CurrentStackFrameFunctionName,
                Method = sessionModel.BreakpointLastHitName
            };

            Repository.Save(eventData);
        }

        public void RegisterStep(SessionModel sessionModel)
        {
            //SCLog.WriteLog(String.Format("{0}: {1} -> {2} : {3}", sessionModel.StepName, lastStackFrameFunctionName, sessionModel.CurrentStackFrameFunctionName, sessionModel.CurrentDocumentLine));

            lastStackFrameFunctionName = sessionModel.CurrentStackFrameFunctionName;
        }

        public void RegisterNewSession()
        {           
            SessionData sessionData = new SessionData
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