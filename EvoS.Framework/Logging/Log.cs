using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Logging
{
    public static class Log
    {
        public static bool Debug = true;
        private static object printLock = new object();

        public static readonly Dictionary<LogType, (ConsoleColor Color, string Name)> TypeColor = new Dictionary<LogType, (ConsoleColor Color, string Name)>()
        {
            { LogType.Debug,   (ConsoleColor.DarkMagenta,  "  Debug  ") },
            { LogType.Misc  ,  (ConsoleColor.DarkGreen,    "  Misc   ") },
            { LogType.Server,  (ConsoleColor.Green,        "  Server ") },
            { LogType.Lobby,   (ConsoleColor.Green,        "  Lobby  ") },
            { LogType.Game,    (ConsoleColor.Green,        "  Game   ") },
            { LogType.Error,   (ConsoleColor.Red,          "  Error  ") },
            { LogType.Packet,  (ConsoleColor.Cyan,         "  Packet ") },
            { LogType.Warning, (ConsoleColor.Yellow,       " Warning ") },
            { LogType.Network, (ConsoleColor.DarkCyan,     " Network ") }
        };

        public static void Print(LogType _type, object _obj, bool showTime = false, bool showLogLevel = false)
        {
            if (showTime)
                Console.Write($"{DateTime.Now:HH:mm:ss tt} |");

            if (showLogLevel)
            {
                if (_type == LogType.Debug)
                {
                    if (!Debug)
                        return;
                    else
                    {
                        Console.ForegroundColor = TypeColor[LogType.Debug].Color;
                        Console.Write(TypeColor[LogType.Debug].Name);
                    }
                }
                else
                {
                    Console.ForegroundColor = TypeColor[_type].Color;
                    Console.Write(TypeColor[_type].Name);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"| {_obj.ToString()}");
        }

        public static void Print(LogType _type, object _obj)
        {
            lock (printLock) // avoid multiple lines merging in one by different tasks calling this method
            {
                Console.Write($"{DateTime.Now:HH:mm:ss tt} |");

                Console.ForegroundColor = TypeColor[_type].Color;

                if (_type == LogType.Debug)
                {
                    if (!Debug)
                        return;
                    else
                    {
                        Console.ForegroundColor = TypeColor[LogType.Debug].Color;
                        Console.Write(TypeColor[LogType.Debug].Name);
                    }
                }
                else
                {
                    Console.ForegroundColor = TypeColor[_type].Color;
                    Console.Write(TypeColor[_type].Name);
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"| {_obj.ToString()}");
            }
        }
    }
}
