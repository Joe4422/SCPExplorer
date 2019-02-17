using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SCPExplorer
{
    class Program
    {
        static int linesPrinted = 0;
        static Dictionary<string, string> namesList = new Dictionary<string, string>()
        {
            { "jtalloran1126", "Researcher J. Talloran" },
            { "jbright5555", "Dr. J. Bright" },
            { "rscranton4421", "Dr. R. Scranton" },
            { "mwheeler5348", "Dr. M. Wheeler. There is no Antimemetics Division" }
        };
        static KeyValuePair<string, string> login;

        static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.BufferHeight = Console.WindowHeight;

            //Startup();

            //var baseUrl = "http://www.scp-wiki.net/";
            //var web = new HtmlWeb();
            //while (true)
            //{
            //    SlowWrite($"${login.Key}:/database/> ", 1);
            //    var input = Console.ReadLine();
            //    var doc = web.Load(baseUrl + input.Replace(' ', '-'));
            //    var content = doc.GetElementbyId("page-content");
            //    SlowWriteLine($"{doc.GetElementbyId("page-title").InnerText.Trim()}\n", 1);
            //    foreach (var c in content.ChildNodes)
            //    {
            //        if (c.Name == "blockquote")
            //        {
            //            Console.WriteLine();
            //            SlowWriteLine(new string('─', (int)(Console.BufferWidth * 0.75)), (int)(Console.BufferWidth * 0.125));
            //            Console.WriteLine();
            //            SlowWrite(SplitString(System.Net.WebUtility.HtmlDecode(c.InnerText), (int) (Console.BufferWidth * 0.75)), (int) (Console.BufferWidth * 0.125));
            //            Console.WriteLine();
            //            SlowWriteLine(new string('─', (int)(Console.BufferWidth * 0.75)), (int)(Console.BufferWidth * 0.125));
            //            Console.WriteLine();
            //        }
            //        else if (c.Name == "p")
            //        {
            //            SlowWrite(SplitString(System.Net.WebUtility.HtmlDecode(c.InnerText), Console.BufferWidth - 3));
            //        }
            //        else if (c.Name.StartsWith("h"))
            //        {
            //            SlowWrite(SplitString(System.Net.WebUtility.HtmlDecode(c.InnerText).ToUpper(), Console.BufferWidth - 3));
            //        }
            //        else if (c.Name == "ul")
            //        {
            //            foreach (var li in c.ChildNodes.Where(x => x.Name == "li"))
            //            {
            //                SlowWrite(new List<string>() { "\t-\t" + System.Net.WebUtility.HtmlDecode(li.InnerText), " "});
            //            }
            //        }

            //    }
            //    linesPrinted = 0;
            //}
            Output.InitConsole();

            LogonManager.DoLogonSequence();

            TerminalHandler.DoTerminalLoop();

            Console.ReadLine();
        }



        static void Startup()
        {
            SlowWrite(new List<string>()
                {
                    " ",
                    " ",
                    " ",
                    "             ###########",
                    "            ##         ##",
                    "        m###*     m     *###m",
                    "      ##*     ..#####,,     *##",
                    "    m##    .#############,    ##m",
                    "   ##    .###     #     ###,    ##",
                    "  m#    ###     #####     ###    #m",
                    "  ##   .##       '#'      ###,   ##",
                    "  ##   ###   wwwww wwwww   ###   ##",
                    "  ##   '##  w####   ####w  ##*   ##",
                    ",#*     *###*' *     * '*####     '#,",
                    " ##,   *'*###,         ,###'`'   ,##",
                    "  *##       '*#########*'       ##'",
                    "    ##.,#m                 m#,.##",
                    "     *'  *#####m     m#####*  '*",
                    "             '*#######*'",
                    " ",
                    " ",
                    "                S C P",
                    "         F O U N D A T I O N",
                    " ",
                    "     \"Secure. Contain. Protect.\"",
                    " ",
                    " ",
                    " "
                },
                Console.BufferWidth / 2 - 18
            );
            linesPrinted = 0;
            SlowWriteLine("NOTICE: You are entering a secure database.", 1);
            SlowWriteLine("Unauthorized access will be met with excessive force.", 1);
            linesPrinted = 0;
            SlowWrite("Login: ", 1);
            linesPrinted = 0;
            login = namesList.ElementAt(new Random().Next(namesList.Count));
            int loginIndex = 0;
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (loginIndex != login.Key.Length || key.Key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(true);
                if (loginIndex != login.Key.Length && char.IsLetterOrDigit(key.KeyChar))
                {
                    Console.Write(login.Key[loginIndex++]);
                }
            }
            Console.WriteLine();
            SlowWrite("Password: ", 1);
            loginIndex = 0;
            int desiredLength = new Random().Next(5, 20);
            while (loginIndex != desiredLength || key.Key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(true);
                if (loginIndex != desiredLength && char.IsLetterOrDigit(key.KeyChar))
                {
                    Console.Write("*");
                    loginIndex++;
                }
            }
            Console.Clear();
            Console.WriteLine();
            SlowWriteLine($"Welcome, {login.Value}. How can I help you today?", 1);

        }

        static void SlowWrite(string text, int x = -1, int y = -1)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x == -1 ? Console.CursorLeft : x, y == -1 ? Console.CursorTop : y);
            foreach (char c in text)
            {
                if (char.IsWhiteSpace(c))
                    Console.Write(c);
                else
                    Console.Write(c + "█\b");
                Thread.Sleep(2);
                Console.Write(" \b");
            }
            Console.CursorVisible = true;
        }

        static void SlowWriteLine(string text, int x = -1, int y = -1)
        {
            SlowWrite(text + Environment.NewLine, x, y);
        }

        static void SlowWrite(List<string> text, int offset = 1, bool newline = true)
        {
            Console.CursorVisible = false;
            foreach (string s in text)
            {
                if (linesPrinted == Console.BufferHeight - 2)
                {
                    Console.Write("\r ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(" ENTER \r");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.Black;
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
                    Console.Write(new string(' ', 8) + "\r");
                    linesPrinted = 0;
                }
                Console.Write(new string(' ', offset));
                for (int i = 0; i < s.Length - 1; i++)
                {
                    if (char.IsWhiteSpace(s[i]))
                        Console.Write(s[i]);
                    else
                        Console.Write(s[i] + "█\b");
                    Thread.Sleep(2);
                    Console.Write(" \b");
                }
                if (newline)
                    Console.WriteLine(s.Last());
                else
                    Console.Write(s.Last());
                linesPrinted++;
            }
            Console.CursorVisible = true;
        }

        static List<string> SplitString(string text, int lineLength)
        {
            var q = text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var outp = new List<string>();
            foreach (string s in q)
            {
                var lines = Regex.Matches(s, @"(.{1," + lineLength + @"})(?:\s|$)").Cast<Match>().Select(m => m.Value).ToList();
                lines.Add("");
                outp.AddRange(lines);
            }
            for (int i = 0; i < outp.Count; i++)
            {
                while (outp[i].Length <= lineLength) outp[i] += " ";
            }
            return outp;
        }
    }
}
