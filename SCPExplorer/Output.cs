using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCPExplorer
{
    public class Output
    {
        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        public static void InitConsole()
        {
            var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            if (!GetConsoleMode(iStdOut, out uint outConsoleMode))
            {
                Console.WriteLine("failed to get output console mode");
                Console.ReadKey();
                return;
            }

            outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
            if (!SetConsoleMode(iStdOut, outConsoleMode))
            {
                Console.WriteLine($"failed to set output console mode, error code: {GetLastError()}");
                Console.ReadKey();
                return;
            }
            Console.BufferHeight = Console.WindowHeight;
        }

        private static void WriteSlow(FormattedString str, int delay = 2, int width = -1, int blockOffset = 1)
        {
            Console.Write(str.AnsiStart);
            for (int i = 0; i < str.RawString.Length; i++)
            {
                if (Console.CursorLeft + str.GetNextWord(i).Length > width && width != -1)
                {
                    Console.Write(str.AnsiEnd);
                    if (Console.CursorTop + 1 >= Console.BufferHeight)
                    {
                        ScrollConsoleDown();
                        Console.SetCursorPosition(blockOffset, Console.CursorTop);
                    }
                    else Console.SetCursorPosition(blockOffset, Console.CursorTop + 1);
                    if (str.RawString[i] == ' ') i++;
                    Console.Write(str.AnsiStart);
                }
                Console.Write(str.RawString[i]);
                Thread.Sleep(delay);
            }
            Console.Write(str.AnsiEnd);
        }

        public static void PrintString(List<FormattedString> strs, int left, int top, int width = -1, int blockOffset = 1)
        {
            int tempTop = top;
            foreach (FormattedString s in strs)
            {
                while (tempTop >= Console.BufferHeight)
                {
                    ScrollConsoleDown();
                    tempTop--;
                }
                Console.SetCursorPosition(left, tempTop++);
                WriteSlow(s, 2, width, blockOffset);
            }
        }

        public static void PrintString(FormattedString str, int left, int top, int width = -1, int blockOffset = 1)
        {
            PrintString(str.Split('\n'), left, top, width, blockOffset);
        }

        public static void PrintStringCentered(List<FormattedString> strs, int top)
        {
            int longest = strs.Max(x => x.RawString.Length);
            int left = (int) (Console.BufferWidth / 2.0 - longest / 2.0);
            PrintString(strs, left, top);
        }

        public static void PrintStringCentered(FormattedString str, int top)
        {
            PrintStringCentered(str.Split('\n'), top);
        }

        public static void DrawBox(int top, int left, int bottom, int right)
        {
            Console.SetCursorPosition(left, top);
            WriteSlow(new FormattedString("┌" + new string('─', right - left - 1) + "┐"));
            for (int y = top + 1; y <= top + (bottom - top); y++)
            {
                Console.SetCursorPosition(left, y);
                WriteSlow(new FormattedString("│"));
                Console.SetCursorPosition(right, y);
                WriteSlow(new FormattedString("│"));
            }
            Console.SetCursorPosition(left, bottom);
            WriteSlow(new FormattedString("└" + new string('─', right - left - 1) + "┘"));
        }

        public static void DrawBoxCentered(int top, int width, int height)
        {
            int left = (int)(Console.BufferWidth / 2.0 - width / 2.0);
            int right = (int)(Console.BufferWidth / 2.0 + width / 2.0) - 1;
            int bottom = top + height - 1;
            DrawBox(top, left, bottom, right);
        }

        public static void ScrollConsoleDown()
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.SetCursorPosition(0, Console.BufferHeight - 1);
            Console.WriteLine();
            Console.SetCursorPosition(left, top);
        }
    }
}
