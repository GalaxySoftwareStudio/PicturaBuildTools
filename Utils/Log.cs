using System;

namespace PicturaBuildTools
{
    public class Log
    {
        enum LogLevel
        {
            Verbose = 0,
            Trace = 1,
            Warning = 2,
            Error = 2,
            Off = 3,
        }

        public class ProgressBar
        {
            private int CreationPosX = 0;
            private int CreationPosY = 0;
            private int LengthInPercent = 0;
            private int LengthInCharSize = 0;

            private int _progression;
            public int Progression
            {
                get { return _progression; }
                set { _progression = value; }
            }

            public ProgressBar(int lengthInPercent = 50)
            {
                CreationPosX = Console.CursorLeft;
                CreationPosY = Console.CursorTop;
                LengthInPercent = lengthInPercent;
                LengthInCharSize = (Console.WindowWidth * (LengthInPercent / 100));

                Console.WriteLine("Length : " + LengthInCharSize);
            }

            public void Show()
            {
                LengthInCharSize = (Console.WindowWidth * (LengthInPercent / 100));
                for (int i = 0; i < LengthInCharSize; i++)
                {

                }
            }
        }

        static LogLevel GlobalLogLevel = LogLevel.Trace;
        public static string LogTime
        {
            get { return DateTime.Now.ToLongTimeString(); }
        }

        static void Debug(string message)
        {
            if (GlobalLogLevel >= LogLevel.Verbose)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[DEBUG - " + LogTime + "] : " + message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void Trace(string message)
        {
            if (GlobalLogLevel >= LogLevel.Trace)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[LOG - " + LogTime + "] : " + message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[SUCCESS - " + LogTime + "] : " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARNING - " + LogTime + "] : " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR - " + LogTime + "] : " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}