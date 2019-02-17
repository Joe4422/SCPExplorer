using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPExplorer
{
    public static class TerminalHandler
    {
        private static string path = "/database/scp/";
        private static string baseUrl = "http://www.scp-wiki.net/";
        public static void DoTerminalLoop()
        {
            FormattedString temp = new FormattedString(" ");
            while (true)
            {
                Output.PrintString(new FormattedString($"{LogonManager.Username}:{path} > "), 1, Console.CursorTop + 1);
                Console.Write(temp.AnsiStart);
                string input = Console.ReadLine();
                Console.Write(temp.AnsiEnd);
                if (input.StartsWith("access "))
                {
                    string article = input.Substring(7);
                    WebHandler.DisplayPage(baseUrl + article);
                }
            }
        }
    }
}
