using System;

namespace PicturaBuildTools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~ Pictura Build Tools (" + OperatingSystem.Name + ") ~");
            Console.WriteLine("");

            if (BuildTool.Run() == ExitCodes.NeedHelp)
            {
                Utils.PrintUsage();
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}