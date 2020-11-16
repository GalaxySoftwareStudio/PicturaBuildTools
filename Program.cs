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
                Utils.PrintUsage();
                return;
            }
            else
            {
                string[] targetPlatform = Arguments.GetSwitchValue(new[] { OperatingSystem.Name }, "p", "platform");
                string[] targetConfig = Arguments.GetSwitchValue(new[] { "Development" }, "c", "configuration");

                if (Arguments.HasSwitch("version") > 0)
                {
                    Utils.PrintVersion();
                    return;
                }

                if (Arguments.HasSwitch("buildsdk") > 0)
                {
                    if (Arguments.HasSwitch("h", "help") > 0)
                    {
                        Utils.PrintUsage();
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
                        Utils.PrintUsage();
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
    }
}