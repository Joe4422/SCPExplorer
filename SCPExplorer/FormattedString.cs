using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPExplorer
{
    public enum StringFormat
    {
        NONE,
        BOLD,
        UNDERLINE,
        STRIKETHROUGH,
        FAINT
    }

    public enum StringColor
    {
        DEFAULT,
        BLACK,
        RED,
        GREEN,
        YELLOW,
        BLUE,
        MAGENTA,
        CYAN,
        WHITE,
        BBLACK,
        BRED,
        BGREEN,
        BYELLOW,
        BBLUE,
        BMAGENTA,
        BCYAN,
        BWHITE,
        RANDOM
    }

    public class FormattedString
    {
        private int GetEscValFormat()
        {
            return new int[] { 108, 1, 4, 9, 2, 108 }[(int)Format];
        }

        private int GetEscValColor(bool background)
        {
            if (background)
                if (BackgroundColor == StringColor.RANDOM)
                    return new int[] { 49, 40, 41, 42, 43, 44, 45, 46, 47, 100, 101, 102, 103, 104, 105, 106, 107 }[new Random().Next(0, 17)];
                else
                    return new int[] { 49, 40, 41, 42, 43, 44, 45, 46, 47, 100, 101, 102, 103, 104, 105, 106, 107 }[(int)BackgroundColor];
            else
                if (Format == StringFormat.FAINT) 
                    return new int[] { 39, 39, 39, 39, 39, 39, 39, 39, 39, 30, 31, 32, 33, 34, 35, 36, 37 }[(int)ForegroundColor];
                else if (ForegroundColor == StringColor.RANDOM)
                    return new int[] { 39, 30, 31, 32, 33, 34, 35, 36, 37, 90, 91, 92, 93, 94, 95, 96, 97 }[new Random().Next(0, 17)];
                else    
                    return new int[] { 39, 30, 31, 32, 33, 34, 35, 36, 37, 90, 91, 92, 93, 94, 95, 96, 97 }[(int)ForegroundColor];
        }

        public StringFormat Format { get; set; }

        public StringColor ForegroundColor { get; set; }

        public StringColor BackgroundColor { get; set; }
        
        public string RawString { get; set; }

        public string AnsiString => $"\u001b[{GetEscValColor(false)}m\u001b[{GetEscValColor(true)}m\u001b[{GetEscValFormat()}m{RawString}\u001b[0m";

        public string AnsiStart => $"\u001b[{GetEscValColor(false)}m\u001b[{GetEscValColor(true)}m\u001b[{GetEscValFormat()}m";

        public string AnsiEnd => "\u001b[0m";


        public FormattedString(string s, StringFormat format, StringColor foregroundColor, StringColor backgroundColor)
        {
            RawString = s;
            Format = format;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public FormattedString(string s) : this(s, StringFormat.NONE, StringColor.BGREEN, StringColor.BLACK) { }

        public List<FormattedString> Split(char split)
        {
            List<FormattedString> s = new List<FormattedString>();
            RawString.Split(split).ToList().ForEach(x => s.Add(new FormattedString(x, Format, ForegroundColor, BackgroundColor)));
            return s;
        }

        public string GetNextWord(int index)
        {
            int i = index;
            string buffer = "";
            try
            {
                while (char.IsWhiteSpace(RawString[i++]));
                while (!char.IsWhiteSpace(RawString[i++])) buffer += RawString[i];
            } catch (Exception) { }
            return buffer;

        }
    }
}
