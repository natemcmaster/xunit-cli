using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace XUnit.CommandLine
{
    internal class NetCoreXunitRunner : XUnitRunner
    {
        private readonly string xunitPath;

        public NetCoreXunitRunner()
        {
            xunitPath = Path.Combine(AppContext.BaseDirectory, "tools", "netcoreapp2.0", "xunit.console.dll");
        }

        public int ShowHelp()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                ArgumentList =
                {
                    xunitPath,
                }
            };

            return StartAndWaitForProcess(psi);
        }

        public override int Run(string dllPath, IEnumerable<string> args)
        {
            var dllDir = Path.GetDirectoryName(dllPath);
            var depsPath = Path.Combine(dllDir, Path.GetFileNameWithoutExtension(dllPath) + ".deps.json");
            var runtimeConfigPath = Path.Combine(dllDir, Path.GetFileNameWithoutExtension(dllPath) + ".runtimeconfig.json");

            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
            };

            psi.ArgumentList.Add("exec");
            psi.ArgumentList.Add("--depsfile");
            psi.ArgumentList.Add(depsPath);
            psi.ArgumentList.Add("--runtimeconfig");
            psi.ArgumentList.Add(runtimeConfigPath);

            psi.ArgumentList.Add(xunitPath);
            psi.ArgumentList.Add(dllPath);
            foreach (var arg in args)
            {
                psi.ArgumentList.Add(arg);
            }

            return StartAndWaitForProcess(psi);
        }
    }
}
