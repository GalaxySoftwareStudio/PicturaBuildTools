using System;
using System.Threading;

namespace PicturaBuildTools
{
    public class Log
    {
        public enum LogLevel
        {
            Off = -1,
            Error = 0,
            Warning = 1,
            Trace = 2,
            Verbose = 3
        }

        public class ProgressBar
        {
            private int CreationPosX = 0;
            private int CreationPosY = 0;
            private int LengthInPercent = 0;
            private int LengthInCharSize = 0;
            private int DelayOnError = 0;
            private bool onScreen = false;
            private bool completed = false;

            private bool _errorState;
            public bool ErrorState
            {
                get { return _errorState; }
                set
                {
                    _errorState = value;
                    Progression = Progression;
                    Thread.Sleep(DelayOnError);

                    if (CleanOnCompleted) { Clean(); }
                    if (value == true) { Error?.Invoke(this, new EventArgs()); }
                }
            }

            private int _progression;
            public int Progression
            {
                get
                {
                    return _progression;
                }
                set
                {
                    _progression = ErrorState ? _progression : value;
                    if (onScreen)
                    {
                        int prevCurX = Console.CursorLeft;
                        int prevCurY = Console.CursorTop;
                        Console.CursorLeft = CreationPosX;
                        Console.CursorTop = CreationPosY;

                        int progressionFill = (int)((LengthInCharSize) * (Progression / 100.0));
                        Console.ForegroundColor = ErrorState ? ConsoleColor.Red : ConsoleColor.Blue;
                        for (int i = 0; i < LengthInCharSize; i++)
                        {
                            Console.Write(i == 0 ? '[' : (i != LengthInCharSize - 1 ? (i < progressionFill ? '=' : '-') : ']'));
                        }
                        Console.Write(" [" + Progression.ToString() + " %]");

                        Console.ForegroundColor = ConsoleColor.White;

                        Console.CursorLeft = prevCurX;
                        Console.CursorTop = prevCurY;

                        if (_progression >= 100 && !completed)
                        {
                            if (CleanOnCompleted) { Clean(); }
                            Completed?.Invoke(this, new EventArgs());
                        }
                    }
                }
            }

            public event EventHandler Completed;
            public event EventHandler Error;

            public bool CleanOnCompleted = true;

            public ProgressBar(int lengthInPercent = 50, bool cleanOnCompleted = true, int delayOnError = 1500)
            {
                LengthInPercent = lengthInPercent;
                CleanOnCompleted = cleanOnCompleted;
                DelayOnError = delayOnError;
                LengthInCharSize = (int)(Console.WindowWidth * (LengthInPercent / 100.0));
                Show();
            }

            private void Show()
            {
                if (!onScreen)
                {
                    CreationPosX = Console.CursorLeft;
                    CreationPosY = Console.CursorTop;
                    LengthInCharSize = (int)(Console.WindowWidth * (LengthInPercent / 100.0));
                    onScreen = true;
                    Progression = Progression; //Property setter update the bar progression on the terminal
                }
            }

            private void Clean()
            {
                int prevCurX = Console.CursorLeft;
                int prevCurY = Console.CursorTop;

                Console.CursorLeft = CreationPosX;
                Console.CursorTop = CreationPosY;
                Console.Write(new string(' ', LengthInCharSize + 24));

                Console.CursorLeft = prevCurX;
                Console.CursorTop = prevCurY;
            }
        }

        public static LogLevel GlobalLogLevel = LogLevel.Trace;
        public static string LogTime
        {
            get { return DateTime.Now.ToLongTimeString(); }
        }

        public static void Debug(string message)
        {
            if (GlobalLogLevel >= LogLevel.Verbose)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[DEBUG - " + LogTime + "] : " + message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void Trace(string message)
        {
            if (GlobalLogLevel >= LogLevel.Trace)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[LOG - " + LogTime + "] : " + message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[SUCCESS - " + LogTime + "] : " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARNING - " + LogTime + "] : " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR - " + LogTime + "] : " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}