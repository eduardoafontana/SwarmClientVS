using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using EnvDTE;
using EnvDTE80;
using System.IO;
using System.Linq;
using SwarmClientVS.Domain.Service;
using SwarmClientVS.DataLog.FileLog;

namespace SwarmClientVS
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(CommandPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    public sealed class CommandPackage : Package
    {
        /// <summary>
        /// CommandPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "7f3585ff-b094-482c-b5dd-38a76345d91f";

        private DTE2 applicationObject;
        private DebuggerEvents debugEvents;
        private CommandEvents commandEvents;
        private SolutionEvents solutionEvents;
        private CurrentCommandStep currentCommandStep;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public CommandPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Command.Initialize(this);
            base.Initialize();

            applicationObject = (DTE2)GetService(typeof(DTE));
            debugEvents = applicationObject.Events.DebuggerEvents;
            commandEvents = applicationObject.Events.CommandEvents;
            solutionEvents = applicationObject.Events.SolutionEvents;
            currentCommandStep = CurrentCommandStep.StepInto;

            solutionEvents.Opened += SolutionEvents_Opened;
            debugEvents.OnEnterBreakMode += DebugEvents_OnEnterBreakMode;
            debugEvents.OnEnterDesignMode += DebugEvents_OnEnterDesignMode;
            debugEvents.OnEnterRunMode += DebugEvents_OnEnterRunMode;
            commandEvents.AfterExecute += CommandEvents_AfterExecute;
        }

        private void CommandEvents_AfterExecute(string Guid, int ID, object CustomIn, object CustomOut)
        {
            DTE2 dte = (DTE2)GetService(typeof(DTE));

            VerifyBreakpointAddedRemoved(ID, dte);

            VerifyCommandStep(Guid, ID, dte);

            //API explorer code.
            //CommandEventsAfterBeforeMonitoring("After", Guid, ID, CustomIn, CustomOut);
        }

        private void DebugEvents_OnEnterRunMode(dbgEventReason Reason)
        {
            SessionService.RegisterNewSession();

            VerifyBreakpointAlreadyAdded(applicationObject);
        }

        private void DebugEvents_OnEnterDesignMode(dbgEventReason Reason)
        {
            SessionService.EndCurrentSession();
        }

        private void DebugEvents_OnEnterBreakMode(dbgEventReason reason, ref dbgExecutionAction action)
        {
            DTE2 dte = (DTE2)GetService(typeof(DTE));

            if (reason == dbgEventReason.dbgEventReasonBreakpoint)//Breakpoint is hitted
            {
                SessionService.RegisterHitted(
                    new StepModel
                    {
                        CurrentStackFrameFunctionName = dte.Debugger.CurrentStackFrame.FunctionName,
                        BreakpointLastHitName = dte.Debugger.BreakpointLastHit.Name,
                        CurrentDocument = DocumentModelBuilder.Build(dte.ActiveDocument)
                    }
                );

                SessionService.ResetPathNode();
            }

            if (reason == dbgEventReason.dbgEventReasonStep)//Any debug step (into, over, out)
            {
                //String stackTrace = String.Empty;

                //foreach (EnvDTE.StackFrame frame in dte.Debugger.CurrentThread.StackFrames)
                //{
                //    stackTrace += String.Format("{0} | ", frame.FunctionName);
                //}

                StepModel stepModel = new StepModel
                {
                    CurrentCommandStep = currentCommandStep,
                    CurrentStackFrameFunctionName = dte.Debugger.CurrentStackFrame.FunctionName,
                    CurrentDocument = DocumentModelBuilder.Build(dte.ActiveDocument)
                };

                SessionService.RegisterStep(stepModel);
                SessionService.RegisterPathNode(stepModel);
            }
        }

        private void SolutionEvents_Opened()
        {
            SessionInputForm window = new SessionInputForm(GetSolutionName(applicationObject));
            window.ShowDialog();
        }

        private string GetSolutionName(DTE2 dte)
        {
            if (dte.Solution == null)
                return "Fail to get solution name. Solution null.";

            if (String.IsNullOrEmpty(dte.Solution.FileName))
                return "Fail to get solution name. FileName empty.";

            return Path.GetFileName(dte.Solution.FileName);
        }

        private void VerifyBreakpointAlreadyAdded(DTE2 dte)
        {
            if (dte.Debugger == null)
                return;

            if (dte.Debugger.Breakpoints == null)
                return;

            SessionService.RegisterAlreadyAddedBreakpoints(dte.Debugger.Breakpoints.Cast<Breakpoint>().Select(x =>
                new BreakpointModel
                {
                    Name = x.Name,
                    FunctionName = x.FunctionName,
                    FileLine = x.FileLine,
                    StartLineText = x.FileColumn,
                    DocumentModel = DocumentModelBuilder.Build(x.File, x.FileLine, x.FileColumn)
                }
            ).ToList());
        }

        private void VerifyCommandStep(string Guid, int ID, DTE2 dte)
        {
            if (dte.Commands == null)
                return;

            EnvDTE.Command command = dte.Commands.Item(Guid, ID);

            if (command == null)
                return;

            switch (command.Name)
            {
                case "Debug.StepInto": currentCommandStep = CurrentCommandStep.StepInto; break;
                case "Debug.StepOver": currentCommandStep = CurrentCommandStep.StepOver; break;
                case "Debug.StepOut": currentCommandStep = CurrentCommandStep.StepOut; break;
            }
        }

        private void VerifyBreakpointAddedRemoved(int ID, DTE2 dte)
        {
            if (dte.Debugger == null)
                return;

            if (dte.Debugger.Breakpoints == null)
                return;

            if (ID == 769)//The event code for breakpoint add
                SessionService.VerifyBreakpointAddedOne(dte.Debugger.Breakpoints.Cast<Breakpoint>().Select(x =>
                    new BreakpointModel
                    {
                        Name = x.Name,
                        FunctionName = x.FunctionName,
                        FileLine = x.FileLine,
                        StartLineText = x.FileColumn,
                        DocumentModel = DocumentModelBuilder.Build(x.File, x.FileLine, x.FileColumn)
                    }
                ).ToList());
            else//The other event code that can represent a removed breakpoint. There is no especific event code for breakpoint remotion.
                SessionService.VerifyBreakpointRemovedOne(dte.Debugger.Breakpoints.Cast<Breakpoint>().Select(x =>
                    new BreakpointModel
                    {
                        Name = x.Name,
                        FunctionName = x.FunctionName,
                        FileLine = x.FileLine,
                        StartLineText = x.FileColumn,
                        DocumentModel = DocumentModelBuilder.Build(x.File, x.FileLine, x.FileColumn)
                    }
                ).ToList());
        }

        //API explorer code.
        private void CommandEventsAfterBeforeMonitoring(string origin, string Guid, int ID, object CustomIn, object CustomOut)
        {
            DTE2 dte = (DTE2)GetService(typeof(DTE));

            EnvDTE.Command command = dte.Commands.Item(Guid, ID);

            Debug.WriteLine(String.Format("{0}|{1}|{2}|{3}|{4}|{5}{6}", origin, command.ID, CustomIn, CustomOut, command.Name, command.LocalizedName, dte.CommandLineArguments));
        }
        #endregion
    }
}
