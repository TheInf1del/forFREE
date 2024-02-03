using AntiDebugging;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace PES_CPK_Solution
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static Mutex mutex = new Mutex(true, "{16D75E79-4B6E-4AFD-A297-90F56E7355CA}");
        private static List<string> BadProcessNameList = new List<string>()
            {
                "procmon64",
                "codecracker",
                "ida",
                "idag",
                "idaw",
                "idaq",
                "idau",
                "scylla",
                "de4dot",
                "de4dotmodded",
                "protection_id",
                "ollydbg",
                "x64dbg",
                "x32dbg",
                "x96dbg",
                "x64netdumper",
                "petools",
                "dnspy",
                "windbg",
                "reshacker",
                "simpleassembly",
                "process hacker",
                "process monitor",
                "qt5core",
                "importREC",
                "immunitydebugger",
                "megadumper",
                "cheatengine-x86_64",
                "dump",
                "dbgclr",
                "wireshark",
                "hxd",
                "http",
            };
        [STAThread]
        static void Main()
        {
            if (PerformChecks())
            {
                Environment.FailFast("Debugger Detected");
            }
            else
            {
                //AntiDump.ProtectDump();
                //
                // Console.WriteLine("Test: Attaching to process in runtime...");
                //var ppid = 0;
                // if (args != null && args.Length > 0)
                // {
                //     int.TryParse(args[0], out ppid);
                // }
                //
                //SelfDebugger.DebugSelf(ppid);
                //
                // PerformChecks();

                Console.WriteLine();
                WriteColoredResult("Any debugger not found. Application run in a safe environment.", ConsoleColor.DarkGreen);
            }

            //Console.ReadLine();
            foreach (Process OneProcess in Process.GetProcesses())
            {
                foreach (string processbad in BadProcessNameList)
                {
                    if (OneProcess.ProcessName.ToString().ToLower().Contains(processbad) || OneProcess.MainWindowTitle.ToString().ToLower().Contains(processbad))
                    {
                        try { OneProcess.Kill(); } catch { }
                    }
                }
            }
            if (Environment.Is64BitOperatingSystem == false)
            {
                XtraMessageBox.Show("Sorry, This software isn't support 32-bit operating system.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                Application.Exit();
                return;
            }
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                if (My.Computer.Network.IsAvailable)
                {
                    Encryptions.t = Encryptions.Decrypt(Encryptions.t);
                    Encryptions.p = StringCipher.Decrypt(Encryptions.p, Encryptions.t);
                    Encryptions.gistUser = Encryptions.Decrypt(Encryptions.gistUser);
                    Encryptions.gitHash = Encryptions.Decrypt(Encryptions.gitHash);
                    Encryptions.ms = Encryptions.Decrypt(Encryptions.ms);
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                mutex.ReleaseMutex();
            }
            else
            {
                XtraMessageBox.Show("The application already running.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static bool PerformChecks()
        {
            var isProcessRemote = AntiDebug.CheckRemoteDebugger();
            var isManagedCodesAttached = AntiDebug.CheckDebuggerManagedPresent();
            var isUnManagedCodesAttached = AntiDebug.CheckDebuggerUnmanagedPresent();
            var checkDebugPort = AntiDebug.CheckDebugPort();
            var checkKernelDebugInformation = AntiDebug.CheckKernelDebugInformation();
            var detectEmulation = ProtectionHelper.DetectEmulation();
            var detectSandbox = ProtectionHelper.DetectSandbox();
            var detectVirtualMachine = ProtectionHelper.DetectVirtualMachine();
            var detachFromDebuggerProcess = AntiDebug.DetachFromDebuggerProcess();
            WriteBooleanResult($"{nameof(AntiDebug)}.{nameof(AntiDebug.CheckRemoteDebugger)}", isProcessRemote);
            WriteBooleanResult($"{nameof(AntiDebug)}.{nameof(AntiDebug.CheckDebuggerManagedPresent)}", isManagedCodesAttached);
            WriteBooleanResult($"{nameof(AntiDebug)}.{nameof(AntiDebug.CheckDebuggerUnmanagedPresent)}", isUnManagedCodesAttached);
            WriteBooleanResult($"{nameof(AntiDebug)}.{nameof(AntiDebug.CheckDebugPort)}", checkDebugPort);
            WriteBooleanResult($"{nameof(AntiDebug)}.{nameof(AntiDebug.CheckKernelDebugInformation)}", checkKernelDebugInformation);
            WriteBooleanResult($"{nameof(ProtectionHelper)}.{nameof(ProtectionHelper.DetectEmulation)}", detectEmulation);
            WriteBooleanResult($"{nameof(ProtectionHelper)}.{nameof(ProtectionHelper.DetectSandbox)}", detectSandbox);
            WriteBooleanResult($"{nameof(ProtectionHelper)}.{nameof(ProtectionHelper.DetectVirtualMachine)}", detectVirtualMachine);
            WriteBooleanResult($"{nameof(AntiDebug)}.{nameof(AntiDebug.DetachFromDebuggerProcess)}", detachFromDebuggerProcess);
            AntiDebug.HideOsThreads();
            Scanner.ScanAndKill();
            
            if (isProcessRemote || isManagedCodesAttached ||
                isUnManagedCodesAttached || checkDebugPort ||
                checkKernelDebugInformation || detectEmulation ||
                detectSandbox || detectVirtualMachine)
            {
                ProtectionHelper.FreezeMouse();
                //ProtectionHelper.ShowCmd("Protector", "Active debugger found! ", "C");
                foreach (Process OneProcess in Process.GetProcesses())
                {
                    foreach (string processbad in BadProcessNameList)
                    {
                        if (OneProcess.ProcessName.ToString().ToLower().Contains(processbad) || OneProcess.MainWindowTitle.ToString().ToLower().Contains(processbad))
                        {
                            try { OneProcess.Kill(); } catch { }
                        }
                    }
                }
                return true;
            }

            return false;
        }

        protected static void WriteBooleanResult(string prop, bool value)
        {
            Console.Write($"{prop}: ");
            WriteColoredResult(value.ToString(), value ? ConsoleColor.Red : ConsoleColor.Green);
        }
        protected static void WriteColoredResult(string text, ConsoleColor color)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }
    }
}
