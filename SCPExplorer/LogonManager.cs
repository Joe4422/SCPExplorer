using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPExplorer
{
    public static class LogonManager
    {
        private static Dictionary<string, string> usernames = new Dictionary<string, string>()
        {
            { "jtalloran1126", "Researcher J. Talloran" },
            { "jbright5555", "Dr. J. Bright" },
            { "rscranton4421", "Dr. R. Scranton" },
            { "mwheeler5348", "Dr. M. Wheeler" },
            { "kpcrow8888", "Professor K. P. Crow" }
        };
        
        public static string Username { get; private set; }

        public static string RealName { get; private set; }

        static LogonManager()
        {
            
        }

        public static void DoLogonSequence()
        {
            Username = usernames.ElementAt(new Random().Next(usernames.Count)).Key;
            RealName = usernames[Username];

            Output.PrintStringCentered(new FormattedString(Graphics.SCPLogo), 2);

            Output.PrintStringCentered(new FormattedString(" ENTER ", StringFormat.NONE, StringColor.BLACK, StringColor.BGREEN), Console.CursorTop + 3);
            Console.CursorVisible = false;
            if (Console.ReadKey(true).Key == ConsoleKey.Escape) return;
            Console.CursorVisible = true;
            Console.Clear();
            Output.PrintStringCentered(new FormattedString("WARNING: THE FOUNDATION DATABASE IS"), 1);
            Output.PrintStringCentered(new FormattedString(Graphics.ClassifiedBig), 2);
            Output.PrintStringCentered(new FormattedString("ACCESS BY UNAUTHORIZED PERSONNEL IS STRICTLY PROHIBITED"), Console.CursorTop + 1);
            Output.PrintStringCentered(new FormattedString("PERPETRATORS WILL BE TRACKED, LOCATED, AND DETAINED"), Console.CursorTop + 1);
            int boxPos = Console.CursorTop + 2;
            Output.DrawBoxCentered(boxPos, 40, 3);
            Output.PrintStringCentered(new FormattedString("Username"), boxPos);
            Output.PrintStringCentered(new FormattedString(new string(' ', 36), StringFormat.NONE, StringColor.BLACK, StringColor.BGREEN), boxPos + 1);
            Console.SetCursorPosition(Console.CursorLeft - 36, Console.CursorTop);
            FormattedString temp = new FormattedString(" ", StringFormat.NONE, StringColor.BLACK, StringColor.BGREEN);
            Console.Write(temp.AnsiStart);
            DoFakeInput(Username);
            Console.Write(temp.AnsiEnd);
            Output.PrintStringCentered(new FormattedString("Password"), boxPos);
            Output.PrintStringCentered(new FormattedString(new string(' ', 36), StringFormat.NONE, StringColor.BLACK, StringColor.BGREEN), boxPos + 1);
            Console.SetCursorPosition(Console.CursorLeft - 36, Console.CursorTop);
            Console.Write(temp.AnsiStart);
            DoFakeInput(new string('*', new Random().Next(10, 15)));
            Console.Write(temp.AnsiEnd);
            Console.Clear();
            Output.PrintString(new FormattedString($"Welcome, {RealName}."), 1, 1);
            Output.PrintString(new FormattedString("What can I do for you today?"), 1, 2);
        }

        private static void DoFakeInput(string desired)
        {
            int index = 0;
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (index != desired.Length || key.Key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(true);
                if (index != desired.Length && char.IsLetterOrDigit(key.KeyChar))
                {
                    Console.Write(desired[index++]);
                }
            }
        }


    }
}
