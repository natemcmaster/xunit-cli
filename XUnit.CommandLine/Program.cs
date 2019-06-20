using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XUnit.CommandLine
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length > 0 && IsDllFilePath(args[0]))
            {
                var dllPath = args[0];
                var passThruArgs = args.Skip(1).ToArray();
                return RunXunitConsole(dllPath, passThruArgs);
            }

            var runner = new NetCoreXunitRunner();
            runner.ShowHelp();
            Console.WriteLine();
            Console.WriteLine("Additional options:");
            Console.WriteLine("  -x86                   : Force tests to run in an x86 process (Windows, .NET Framework only)");
            return ExitCodes.HelpShown;
        }

        static int RunXunitConsole(string dllPath, string[] args)
        {
            XUnitRunner runner;
            IEnumerable<string> xunitArgs = args;
            if (IsDllNetCoreApp(dllPath))
            {
                runner = new NetCoreXunitRunner();
            }
            else
            {
                var forceX86 = args.Any(a => a == "-x86");
                xunitArgs = args.Where(a => a != "-x86");
                runner = new NetFrameworkXunitRunner(forceX86);
            }

            return runner.Run(dllPath, xunitArgs);
        }

        private static bool IsDllNetCoreApp(string dllPath)
        {
            // use the presence of a runtimeconfig.json file to determine that a .dll is .NET Core
            var runtimeConfigJson = Path.GetFileNameWithoutExtension(dllPath) + ".runtimeconfig.json";
            return File.Exists(Path.Combine(Path.GetDirectoryName(dllPath), runtimeConfigJson));
        }

        static bool IsDllFilePath(string arg)
        {
            if (string.IsNullOrEmpty(arg))
            {
                return false;
            }

            if (arg.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                return false;
            }

            return arg.EndsWith(".dll", StringComparison.OrdinalIgnoreCase);
        }
    }
}
