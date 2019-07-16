using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Logging
{
    public class Log : ILog
    {
        public static readonly Dictionary<LogType, (ConsoleColor Color, string Name)> TypeColor = new Dictionary<LogType, (ConsoleColor Color, string Name)>()
        {
            { LogType.Debug,    (ConsoleColor.DarkMagenta,  " Debug   ") },
            { LogType.Server,   (ConsoleColor.Green,        " Server  ") },
            { LogType.Error,    (ConsoleColor.Red,          " Error   ") },
            { LogType.Packet,   (ConsoleColor.Cyan,         " Packet  ") },
            { LogType.Warning,  (ConsoleColor.Yellow,       " Warning ") },
            { LogType.Network,  (ConsoleColor.DarkCyan,     " Network ") }
        };

        public void Print(LogType _type, object _obj, bool showTime = false, bool showLogLevel = false)
        {
            if (showTime)
                Console.Write($"{DateTime.Now:HH:mm:ss tt} |");

            if (showLogLevel)
            {
                Console.ForegroundColor = TypeColor[_type].Color;
                Console.Write(TypeColor[_type].Name);
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"| {_obj.ToString()}");
        }

        public void Print(LogType _type, object _obj)
        {
            Console.Write($"{DateTime.Now:HH:mm:ss tt} |");

            Console.ForegroundColor = TypeColor[_type].Color;
            Console.Write(TypeColor[_type].Name);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"| {_obj.ToString()}");
        }
    }
}
