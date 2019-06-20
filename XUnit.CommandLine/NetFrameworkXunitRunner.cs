using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace XUnit.CommandLine
{
    internal class NetFrameworkXunitRunner : XUnitRunner
    {
        private readonly string xunitPath;
        private readonly bool forceX86;

        public NetFrameworkXunitRunner(bool forceX86)
        {
            this.forceX86 = forceX86;
            var xunitFileName = forceX86
                ? "xunit.console.x86.exe"
                : "xunit.console.exe";
            xunitPath = Path.Combine(AppContext.BaseDirectory, "tools", "net472", xunitFileName);
        }

        public override int Run(string dllPath, IEnumerable<string> args)
        {
            ProcessStartInfo psi;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                psi = new ProcessStartInfo
                {
                    FileName = xunitPath,
                };
            }
            else
            {
                if (forceX86)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("error: -x86 is only supported on Windows");
                    Console.ResetColor();
                    return ExitCodes.BadArgs;
                }

                psi = new ProcessStartInfo
                {
                    FileName = "mono",
                    ArgumentList =
                    {
                        xunitPath
                    }
                };
            }

            psi.ArgumentList.Add(dllPath);

            foreach (var arg in args)
            {
                psi.ArgumentList.Add(arg);
            }

            return StartAndWaitForProcess(psi);
        }
    }
}
