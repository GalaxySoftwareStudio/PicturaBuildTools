using System;
using System.Collections.Generic;

namespace PicturaBuildTools
{
    public static class BuildTool
    {
        public static ExitCodes Run()
        {
            if (Arguments.HasSwitch("v", "verbose") > 0 && Arguments.Count() > 1)
            {
                Log.GlobalLogLevel = Log.LogLevel.Trace;
            }

            if (Arguments.HasSwitch("h", "help") > 0 && Arguments.Count() < 2)
            {
                Utils.PrintUsage();
                return ExitCodes.Success;
            }
            else
            {
                string[] targetPlatform = Arguments.GetSwitchValue(new[] { OperatingSystem.Name }, "p", "platform");
                string[] targetConfig = Arguments.GetSwitchValue(new[] { "Development" }, "c", "configuration");

                if (Arguments.HasSwitch("version") > 0)
                {
                    Utils.PrintVersion();
                    return ExitCodes.Success;
                }

                if (Arguments.HasSwitch("buildsdk") > 0)
                {
                    if (Arguments.HasSwitch("h", "help") > 0)
                    {
                        Utils.PrintUsage();
                        SDKCompiler.Help();
                        return ExitCodes.Success;
                    }

                    foreach (string config in targetConfig)
                    {
                        foreach (string platform in targetPlatform)
                        {
                            ExitCodes exitCode = SDKCompiler.Run(new Dictionary<string, string> { { "targetConfig", config }, { "targetPlatform", platform } });
                            if (exitCode != ExitCodes.Success)
                            {
                                return exitCode;
                            }
                        }
                    }

                    return ExitCodes.Success;
                }

                if (Arguments.HasSwitch("build") > 0)
                {
                    if (Arguments.HasSwitch("h", "help") > 0)
                    {
                        Utils.PrintUsage();
                        ProjectCompiler.Help();
                        return ExitCodes.Success;
                    }

                    foreach (string config in targetConfig)
                    {
                        foreach (string platform in targetPlatform)
                        {
                            ExitCodes exitCode = ProjectCompiler.Run(new Dictionary<string, string> { { "targetConfig", config }, { "targetPlatform", platform } });
                            if (exitCode != ExitCodes.Success)
                            {
                                return exitCode;
                            }
                        }
                    }

                    return ExitCodes.Success;
                }

                return ExitCodes.NeedHelp;
            }
        }
    }
}