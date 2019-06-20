using System.Collections.Generic;
using System.Diagnostics;

namespace XUnit.CommandLine
{
    internal abstract class XUnitRunner
    {
        public abstract int Run(string dllPath, IEnumerable<string> args);

        protected int StartAndWaitForProcess(ProcessStartInfo processStartInfo)
        {
            var process = Process.Start(processStartInfo);

            process.WaitForExit();

            return process.ExitCode;
        }
    }
}
