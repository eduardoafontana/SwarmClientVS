using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;

namespace SwarmClientVS
{
    public class SCSession
    {
        private List<SCBreakpoint> currentBreakpointsList = new List<SCBreakpoint>();
        private string lastStackFrameFunctionName = String.Empty;

        internal void VerifyBreakpointAddedOne(Breakpoints breakpoints)
        {
            if (breakpoints == null)
                return;

            List<Breakpoint> newBreakpointsList = breakpoints.Cast<Breakpoint>().Where(n => !currentBreakpointsList.Any(o => o.Name == n.Name)).ToList();

            foreach (Breakpoint item in newBreakpointsList)
            {
                SCBreakpoint newBreakpoint = new SCBreakpoint(item);

                SCLog.WriteLog(String.Format("Added: {0}", newBreakpoint.ToString()));

                currentBreakpointsList.Add(newBreakpoint);
            }
        }

        internal void VerifyBreakpointRemovedOne(Breakpoints breakpoints)
        {
            if (breakpoints == null)
                return;

            List<SCBreakpoint> newBreakpointsList = currentBreakpointsList.Where(n => !breakpoints.Cast<Breakpoint>().Any(o => o.Name == n.Name)).ToList();

            foreach (SCBreakpoint item in newBreakpointsList)
            {
                SCLog.WriteLog(String.Format("Removed: {0}", item.ToString()));

                currentBreakpointsList.Remove(item);
            }
        }

        internal void RegisterHitted(StackFrame currentStackFrame, Breakpoint breakpointLastHit)
        {
            SCLog.WriteLog(String.Format("Hitted: {0}|{1}", breakpointLastHit.Name, currentStackFrame.FunctionName));
        }

        internal void RegisterStep(string stepName, StackFrame currentStackFrame)
        {
            //TODO: point to continuous logical evolution
            //if (!debugger.CurrentStackFrame.FunctionName.Equals(lastStackFrameFunctionName))
            //{ 
            SCLog.WriteLog(String.Format("{0}: {1} -> {2}", stepName, lastStackFrameFunctionName, currentStackFrame.FunctionName));

            lastStackFrameFunctionName = currentStackFrame.FunctionName;
            //}
        }
    }
}