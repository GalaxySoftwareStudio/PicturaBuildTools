using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace PicturaBuildTools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~ Pictura Build Tools (" + OperatingSystem.Name + ") ~");
            Console.WriteLine("");

            Run();

            Console.WriteLine("");
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }

        static void Run()
        {
            if (Arguments.HasSwitch("v", "verbose") > 0 && Arguments.Count() > 1)
            {
                Log.GlobalLogLevel = Log.LogLevel.Trace;
            }

            if (Arguments.HasSwitch("h", "help") > 0 && Arguments.Count() < 2)
            {
                PrintUsage();
                return;
            }
            else
            {
                string[] targetPlatform = Arguments.GetSwitchValue(new[] { OperatingSystem.Name }, "p", "platform");
                string[] targetConfig = Arguments.GetSwitchValue(new[] { "Development" }, "c", "configuration");

                if (Arguments.HasSwitch("version") > 0)
                {
                    PrintVersion();
                    return;
                }

                if (Arguments.HasSwitch("buildsdk") > 0)
                {
                    if (Arguments.HasSwitch("h", "help") > 0)
                    {
                        PrintUsage();
                        SDKCompiler.Help();
                        return;
                    }

                    foreach (string config in targetConfig)
                    {
                        foreach (string platform in targetPlatform)
                        {
                            SDKCompiler.Run(new Dictionary<string, string> { { "targetConfig", config }, { "targetPlatform", platform } });
                        }
                    }

                    return;
                }

                if (Arguments.HasSwitch("build") > 0)
                {
                    if (Arguments.HasSwitch("h", "help") > 0)
                    {
                        PrintUsage();
                        ProjectCompiler.Help();
                        return;
                    }

                    foreach (string config in targetConfig)
                    {
                        foreach (string platform in targetPlatform)
                        {
                            ProjectCompiler.Run(new Dictionary<string, string> { { "targetConfig", config }, { "targetPlatform", platform } });
                        }
                    }

                    return;
                }
            }
        }

        public static void PrintVersion()
        {
            Console.WriteLine("Version : 0.1");
            Console.WriteLine($"Host platform : {OperatingSystem.Name}");
            Console.WriteLine($"Host platform version : {Environment.OSVersion.Version}");
            Console.WriteLine($"Host architecture : {RuntimeInformation.OSArchitecture.ToString()}");
            Console.WriteLine($"Host description : {RuntimeInformation.OSDescription}");
        }

        public static void PrintUsage()
        {
            Console.WriteLine("Usage : PicturaBuildTools [command] [options] [sdkDirectory || projectName.pictura]");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Commands : ");
            Console.ForegroundColor = ConsoleColor.White;
            Arguments.PrintOptionDescription("Prepare the build tools to compile the SDK", "build-sdk");
            Arguments.PrintOptionDescription("Prepare the build tools to compile a Pictura project", "build");
            Arguments.PrintOptionDescription("Remove all project binaries and temporary files", "clean");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("General options : ");
            Console.ForegroundColor = ConsoleColor.White;
            Arguments.PrintOptionDescription("Print this help message", "-h", "--help");
            Console.CursorLeft = 42;
            Console.WriteLine("(Can be passed to any command) [e.g PicturaBuildTools build-sdk --help].\n");

            Arguments.PrintOptionDescription("Print PicturaBuildTools and host version informations", "-ver", "--version");
            Arguments.PrintOptionDescription("Set the verbosity level of the build tools [off, error, warning, trace, all]", "-ll=<level>", "--log-level=<level>");
            Arguments.PrintOptionDescription("Verbose output while building (same as --log-level=all)", "-v", "--verbose");
            Console.WriteLine("");
        }
    }
}