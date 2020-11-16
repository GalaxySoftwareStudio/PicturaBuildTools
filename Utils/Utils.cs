using System;
using System.Runtime.InteropServices;

namespace PicturaBuildTools
{
    public enum ExitCodes
    {
        UnknownState,
        Canceled,
        Error,
        Success,
        NeedHelp
    }

    public static class Utils
    {

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

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Commands : ");
            Console.ForegroundColor = ConsoleColor.White;
            Arguments.PrintOptionDescription("$Prepare the build tools to compile the SDK", "build-sdk");
            Arguments.PrintOptionDescription("$Prepare the build tools to compile a Pictura project", "build");
            Arguments.PrintOptionDescription("$Remove all project binaries and temporary files", "clean");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("General options : ");
            Console.ForegroundColor = ConsoleColor.White;
            Arguments.PrintOptionDescription("Print this help message", "-h", "--help");
            Console.CursorLeft = 44;
            Console.WriteLine("(Can be passed to any command) [e.g PicturaBuildTools build-sdk --help].\n");

            Arguments.PrintOptionDescription("Print PicturaBuildTools and host version informations", "-ver", "--version");
            Arguments.PrintOptionDescription("Set the verbosity level of the build tools [off, error, warning, trace, all]", "-ll=<level>", "--log-level=<level>");
            Arguments.PrintOptionDescription("Verbose output while building (same as --log-level=all)", "-v", "--verbose");
            Console.WriteLine("");
        }
    }
}