using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPExplorer
{
    public static class WebHandler
    {
        private static HtmlWeb web = new HtmlWeb();
        private static HtmlNode FetchPageContent(string url)
        {
            try
            {
                return web.Load(url).GetElementbyId("page-content");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void DisplayPage(string url)
        {
            HtmlNode node = FetchPageContent(url);
            PrintNodesRecursively(node);
        }

        private static bool bq = false;
        public static void PrintNodesRecursively(HtmlNode node)
        {
            string text = HtmlEntity.DeEntitize(node.InnerText);
            if (node.GetClasses().Contains("scp-image-block")) return;
            else if (node.GetClasses().Contains("info-container")) return;
            else if (node.GetClasses().Contains("page-rate-widget-box")) return;
            else if (node.GetClasses().Contains("footer-wikiwalk-nav")) return;
            else if (node.Name == "li")
            {
                Console.WriteLine();
                Output.PrintString(new FormattedString($" - {text}"), 1, Console.CursorTop, bq ? Console.BufferWidth - 8 : Console.BufferWidth - 2, bq ? 4 : 1);
            }
            else if (node.Name == "blockquote")
            {
                Output.PrintString(new FormattedString("┌" + new string('─', Console.BufferWidth - 4) + "┐"), 1, Console.CursorTop + 1);
                bq = true;
            }
            else if (node.Name == "hr")
            {
                Output.PrintString(new FormattedString(new string('─', bq ? Console.BufferWidth - 10 : Console.BufferWidth - 2)), 1, Console.CursorTop + 1, bq ? Console.BufferWidth - 8 : Console.BufferWidth - 2, bq ? 4 : 1);
            }
            else if (node.Name == "br")
            {
                Console.WriteLine();
                Console.SetCursorPosition(1, Console.CursorTop - 1);
            }
            else if (node.Name == "em")
            {
                Output.PrintString(new FormattedString(text, StringFormat.FAINT, StringColor.BGREEN, StringColor.BLACK), Console.CursorLeft, Console.CursorTop, bq ? Console.BufferWidth - 8 : Console.BufferWidth - 2, bq ? 4 : 1);
                return;
            }
            else if (node.Name == "p")
            {
                Console.WriteLine();
                Console.SetCursorPosition(bq ? 4 : 1, Console.CursorTop);
            }
            else if (node.Name == "strong" || node.Name == "b")
            {
                Output.PrintString(new FormattedString(text, StringFormat.UNDERLINE, StringColor.BGREEN, StringColor.BLACK), Console.CursorLeft, Console.CursorTop, bq ? Console.BufferWidth - 4 : Console.BufferWidth - 2, bq ? 2 : 1);
                return;
            }
            else if (node.Name == "#text")
            {
                Output.PrintString(new FormattedString(text), Console.CursorLeft, Console.CursorTop, bq ? Console.BufferWidth - 8 : Console.BufferWidth - 2, bq ? 4 : 1);
            }
            foreach (HtmlNode subnode in node.ChildNodes)
            {
                PrintNodesRecursively(subnode);
            }
            if (node.Name == "blockquote")
            {
                bq = false;
                Output.PrintString(new FormattedString("└" + new string('─', Console.BufferWidth - 4) + "┘"), 1, Console.CursorTop + 1);
            }
        }
    }
}
