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
        //private List<SCStackFrameFunction> stackFrameFunctionList = new List<SCStackFrameFunction>();
        private List<SCBreakpoint> currentBreakpointsList = new List<SCBreakpoint>();
        private string lastStackFrameFunctionName = String.Empty;

        internal void VerifyBreakpointAddedOne(Breakpoints breakpoints)
        {
            if (breakpoints == null)
                return;

            //var newBreakpointsList = (from item in breakpoints.Cast<Breakpoint>()
            //                          where !CurrentBreakpointsList.Contains(item)
            //                          select item).ToList();

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
            //if (!debugger.CurrentStackFrame.FunctionName.Equals(lastStackFrameFunctionName))
            //{ 
            SCLog.WriteLog(String.Format("{0}: {1} -> {2}", stepName, lastStackFrameFunctionName, currentStackFrame.FunctionName));

            lastStackFrameFunctionName = currentStackFrame.FunctionName;
            //}
        }

        //internal void RegisterBreakModeSteps(dbgEventReason reason, dbgExecutionAction action, Debugger debugger, ref string currentCommandStep)
        //{
        //    if (debugger == null)
        //        return;

        //    if (reason != dbgEventReason.dbgEventReasonBreakpoint && reason != dbgEventReason.dbgEventReasonStep)
        //        return;

        //    if(reason == dbgEventReason.dbgEventReasonBreakpoint)
        //    {
        //        Breakpoint breakpoint = debugger.BreakpointLastHit;

        //        SCLog.WriteLog(String.Format("Hitted: {0}|{1}", breakpoint.Name, debugger.CurrentStackFrame.FunctionName));

        //        lastStackFrameFunctionName = debugger.CurrentStackFrame.FunctionName;

        //        //stackFrameFunctionList = new List<SCStackFrameFunction>();
        //        //stackFrameFunctionList.Add(new SCStackFrameFunction(debugger.CurrentStackFrame.FunctionName));
        //    }

        //    if (reason == dbgEventReason.dbgEventReasonStep)
        //    {
        //        if (!debugger.CurrentStackFrame.FunctionName.Equals(lastStackFrameFunctionName))
        //        {
        //            if(currentCommandStep.Equals("Debug.StepInto"))
        //            {
        //                SCLog.WriteLog(String.Format("Step into: {0} -> {1}", lastStackFrameFunctionName, debugger.CurrentStackFrame.FunctionName));

        //                currentCommandStep = String.Empty;
        //            }

        //            //SCStackFrameFunction stackFrameFunction = stackFrameFunctionList.FirstOrDefault(x => x.Name == debugger.CurrentStackFrame.FunctionName);

        //            //if (stackFrameFunction == null)//novo método NÃO está na pilha de debug
        //            //{
        //            //    SCLog.WriteLog(String.Format("Step into: {0} -> {1}", lastStackFrameFunctionName, debugger.CurrentStackFrame.FunctionName));

        //            //    stackFrameFunctionList.Add(new SCStackFrameFunction(debugger.CurrentStackFrame.FunctionName));
        //            //}
        //            //else //novo método está na pilha de debug
        //            //{
        //            //    SCLog.WriteLog(String.Format("Out method: {0} -> {1}", lastStackFrameFunctionName, debugger.CurrentStackFrame.FunctionName));

        //            //    stackFrameFunctionList.Remove(stackFrameFunction);
        //            //}

        //            lastStackFrameFunctionName = debugger.CurrentStackFrame.FunctionName;
        //        }


        //        //    //-----------------

        //        //    //List<Expressions> exp = debugger.CurrentStackFrame.Arguments.Cast<Expressions>().ToList();
        //        //    //string fn = debugger.CurrentStackFrame.FunctionName;
        //        //    //string fnParent = debugger.CurrentStackFrame.Parent.Name;
        //        //    //string module = debugger.CurrentStackFrame.Module;
        //        //    //string returnType = debugger.CurrentStackFrame.ReturnType;

        //        //    //string th = debugger.CurrentThread.Name;
        //        //    //List<Expressions> locals = debugger.CurrentStackFrame.Locals.Cast<Expressions>().ToList();

        //        //    //List<StackFrames> stFrames = debugger.CurrentThread.StackFrames.Cast<StackFrames>().ToList();


        //        //    //SCLog.WriteLog(String.Format("Step: {0}\r\n{1}\r\n{2}\r\n{3}", action, fn, fnParent, th));
        //    }
        //}
    }
}