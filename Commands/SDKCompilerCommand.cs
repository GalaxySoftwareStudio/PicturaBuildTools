using System;
using System.Collections.Generic;

namespace PicturaBuildTools
{
    public class SDKCompiler
    {
        public static ExitCodes Run(Dictionary<string, string> parameters)
        {
            Log.Trace($"Compiling SDK [{parameters["targetConfig"].ToLower()}] for {parameters["targetPlatform"].ToLower()}...");
            return ExitCodes.Success;
        }

        public static void Help()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Build SDK options (build-sdk) : ");
            Arguments.PrintOptionDescription("$Set where PicturaSDK root directory is", "-s=<path>", "--sdk=<path>");
            Arguments.PrintOptionDescription("$Switch the target platform [windows, linux, macos]", "-p=<name>", "--platform=<name>");
            Arguments.PrintOptionDescription("$Switch the target configuration [development, release]", "-c=<name>", "--configuration=<name>");
            Arguments.PrintOptionDescription("$Don't save PicturaSDK as an environment variable if the compilation succeed", "-np", "--no-env-var");
        }
    }
}