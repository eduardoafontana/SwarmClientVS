using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using EnvDTE;
using EnvDTE80;

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
        private BuildEvents buildEvents;
        private DebuggerEvents debugEvents;
        private CommandEvents commandEvents;
        private TextEditorEvents textEditorEvents;
        private DocumentEvents documentEvents;

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
            buildEvents = applicationObject.Events.BuildEvents;
            debugEvents = applicationObject.Events.DebuggerEvents;
            commandEvents = applicationObject.Events.CommandEvents;
            textEditorEvents = applicationObject.Events.TextEditorEvents;
            documentEvents = applicationObject.Events.DocumentEvents;

            //----------------------
            //DTE2 ao = (DTE2)GetService(typeof(DTE));

            //string bpHash = string.Empty;
            //foreach (Breakpoint bp in ao.Debugger.Breakpoints)
            //{
            //    bpHash += string.Format("{0}|{1}|{2}|{3}{4}", bp.FileLine, bp.File, bp.FunctionName, bp.Name, Environment.NewLine);
            //}
            //----------------------

            //applicationObject.Debugger.Breakpoints
            debugEvents.OnContextChanged += delegate (EnvDTE.Process newProc, EnvDTE.Program newProg, EnvDTE.Thread newThread, EnvDTE.StackFrame newStkFrame)
            {

            };

            debugEvents.OnEnterDesignMode += delegate (dbgEventReason reason)
            {

            };

            debugEvents.OnEnterBreakMode += delegate (dbgEventReason reason, ref dbgExecutionAction action)
            {
                //dbgEventReason.dbgEventReasonBreakpoint
                //https://msdn.microsoft.com/pt-br/subscriptions/envdte.dbgeventreason.aspx

                //if (reason != dbgEventReason.dbgEventReasonStep && Options.IsBeepOnBreakpointHit)
                //{
                //    HandleEventSafe(EventType.BreakpointHit, "Breakpoint was hit.");
                //}
            };

            commandEvents.AfterExecute += delegate (string Guid, int ID, object CustomIn, object CustomOut)
            {
                //CommandEventsAfterBeforeMonitoring("After", Guid, ID, CustomIn, CustomOut);

                DTE2 ao1 = (DTE2)GetService(typeof(DTE));

                if (ao1.Debugger.Breakpoints != null)//Se não há solution aberta, breakpoints é nulo
                {
                    foreach (Breakpoint bp in ao1.Debugger.Breakpoints)
                    {
                        Debug.WriteLine(String.Format("Afeter: {0}|{1}|{2}|{3}{4}", bp.FileLine, bp.File, bp.FunctionName, bp.Name, Environment.NewLine));
                    }
                }

                Debug.WriteLine("-----------");
            };

            commandEvents.BeforeExecute += delegate(string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault)
            {
                //CommandEventsAfterBeforeMonitoring("Before", Guid, ID, CustomIn, CustomOut);

                Debug.WriteLine("-----------");

                DTE2 ao1 = (DTE2)GetService(typeof(DTE));

                if (ao1.Debugger.Breakpoints != null)//Se não há solution aberta, breakpoints é nulo
                {
                    foreach (Breakpoint bp in ao1.Debugger.Breakpoints)
                    {
                        Debug.WriteLine(String.Format("Before: {0}|{1}|{2}|{3}{4}", bp.FileLine, bp.File, bp.FunctionName, bp.Name, Environment.NewLine));
                    }
                }
            };

            textEditorEvents.LineChanged += delegate (TextPoint StartPoint, TextPoint EndPoint, int Hint)
            {
                
            };
        }

        private void CommandEventsAfterBeforeMonitoring(string origin, string Guid, int ID, object CustomIn, object CustomOut)
        {
            DTE2 ao1 = (DTE2)GetService(typeof(DTE));

            if (ID == 769)//Parece que o 769 é disparado sempre e apenas quando um breakpoint é inserido.
            {
                EnvDTE.Command bpCommand = ao1.Commands.Item(Guid, ID);

                object bp = bpCommand.Bindings;

                Debug.WriteLine(String.Format("{0}|{1}|{2}|{3}|{4}|{5}{6}", origin, bpCommand.ID, CustomIn, CustomOut, bpCommand.Name, bpCommand.LocalizedName, ao1.CommandLineArguments));
            }

            EnvDTE.Command command = ao1.Commands.Item(Guid, ID);

            Debug.WriteLine(String.Format("{0}|{1}|{2}|{3}|{4}|{5}{6}", origin, command.ID, CustomIn, CustomOut, command.Name, command.LocalizedName, ao1.CommandLineArguments));
        }
        #endregion
    }
}
