using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Logging
{
    public static class Log
    {
#if DEBUG
        public static bool Debug = true;
#else
        public static bool Debug = false;
#endif

        public static readonly Dictionary<LogType, (ConsoleColor Color, string Name)> TypeColor = new Dictionary<LogType, (ConsoleColor Color, string Name)>()
        {
            { LogType.Debug,    (ConsoleColor.DarkMagenta,  " Debug   ") },
            { LogType.Server,   (ConsoleColor.Green,        " Server  ") },
            { LogType.Error,    (ConsoleColor.Red,          " Error   ") },
            { LogType.Packet,   (ConsoleColor.Cyan,         " Packet  ") },
            { LogType.Warning,  (ConsoleColor.Yellow,       " Warning ") },
            { LogType.Network,  (ConsoleColor.DarkCyan,     " Network ") }
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
                        throw new Exception("Not in debug build..");
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
            Console.Write($"{DateTime.Now:HH:mm:ss tt} |");

            Console.ForegroundColor = TypeColor[_type].Color;

            if (_type == LogType.Debug)
            {
                if (!Debug)
                    throw new Exception("Not in debug build..");
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
