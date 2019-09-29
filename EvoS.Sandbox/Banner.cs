using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Sandbox
{
    public static class Banner
    {
        public static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(@"|    ___________            _________    |");
            Console.WriteLine(@"|    \_   _____/__  ______ /   _____/    |");
            Console.WriteLine(@"|     |    __)_\  \/ /  _ \\_____  \     |");
            Console.WriteLine(@"|     |        \\   (  <_> )        \    |");
            Console.WriteLine(@"|    /_______  / \_/ \____/_______  /    |");
            Console.WriteLine(@"|            \/                   \/     |");
            Console.WriteLine(@"|_____________    Sandbox    ____________|");

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
