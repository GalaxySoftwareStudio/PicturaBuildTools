using System;
using System.Collections.Generic;

namespace PicturaBuildTools
{
    public class ProjectCompiler
    {
        public static ExitCodes Run(Dictionary<string, string> parameters)
        {
            Log.Trace($"Compiling pictura project [{parameters["targetConfig"].ToLower()}] for {parameters["targetPlatform"].ToLower()}...");
            return ExitCodes.Success;
        }

        public static void Help()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Build project options : ");
            Arguments.PrintOptionDescription("$Indicate which SDK to use (Default path is PICTURA_SDK environment path)", "-s=<path>", "--sdk=<path>");
            Arguments.PrintOptionDescription("$Switch the target platform [windows, linux, macos]", "-p=<name>", "--platform=<name>");
            Arguments.PrintOptionDescription("$Switch the target configuration [development, release]", "-c=<name>", "--configuration=<name>");
        }
    }
}