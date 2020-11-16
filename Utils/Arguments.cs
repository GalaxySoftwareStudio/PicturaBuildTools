using System;

namespace PicturaBuildTools
{
    public class Arguments
    {
        public static int HasSwitch(params string[] switchName)
        {
            int result = 0;
            foreach (var sw in switchName)
            {
                string[] filteredArgs = Environment.GetCommandLineArgs();
                for (int i = 0; i < filteredArgs.Length; i++)
                {
                    filteredArgs[i] = filteredArgs[i].Replace("-", string.Empty).Split("=")[0];
                    if (filteredArgs[i] == sw)
                    {
                        result = i;
                        break;
                    }
                }
            }

            return result;
        }

        public static string[] GetSwitchValue(string[] defaultValue, params string[] switchName)
        {
            int switchIndex = HasSwitch(switchName);
            if (switchIndex > 0)
            {
                string[] switchValue = Environment.GetCommandLineArgs()[switchIndex].Split('=')[1].Split(',');
                return switchValue;
            }
            else
            {
                if (Log.GlobalLogLevel == Log.LogLevel.Verbose) { Log.Warning($"Value for switch [{string.Join(',', switchName)}] was not found ! Using default value."); }
                return defaultValue;
            }
        }

        public static int Count()
        {
            return Environment.GetCommandLineArgs().Length - 1;
        }

        public static void PrintArgs()
        {
            Console.WriteLine("Arguments : " + string.Join(", ", Environment.GetCommandLineArgs()));
        }

        public static void PrintOptionDescription(string description, params string[] possibleSwitches)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorLeft = 4;
            Console.ForegroundColor = description[0] == '$' ? ConsoleColor.Blue : ConsoleColor.Magenta;
            description = description.Replace("$", string.Empty);
            Console.Write(string.Join(" | ", possibleSwitches));

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.CursorLeft = 42;
            Console.WriteLine(description);

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}