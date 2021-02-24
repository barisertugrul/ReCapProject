using System;

namespace ConsoleUI.Concrete
{
    public static class ConsoleTexts
    {
        private const int _consoleWidth = 110;
        private const string _leftText = "     *";
        private const string _rightText = "*";
        private const int _itemLeft = 5;
        private static string _emptyLine = _leftText + RepeatText(" ", _consoleWidth - 4) + _leftText;

        public static void WriteMenuConsoleTexts(string headerText, string[] menuItems)
        {
            HeaderFooterLine();
            Header(headerText);
            WriteMenuItems(menuItems);
            HeaderFooterLine();
        }

        public static void HeaderFooterLine()
        {
            string text = _leftText + RepeatText(" *", _consoleWidth / 2) + " " + _rightText;
            Console.WriteLine(text);
        }

        public static void Header(string header)
        {
            int headerLength = header.Length + _itemLeft * 2 + 2;
            int left = (_consoleWidth - headerLength) / 2;
            int right = _consoleWidth - (headerLength + left) + 1;
            string text = _leftText + RepeatText(" ", _itemLeft) + RepeatText("=", left) + " " + header + " " + RepeatText("=", right) + RepeatText(" ", _itemLeft) + _rightText;
            text += "\n" + _emptyLine;
            Console.WriteLine(text);
        }

        private static string RepeatText(string repeatText, int numberOfRepeat)
        {
            string text = "";
            for (int i = 0; i < numberOfRepeat; i++)
            {
                text += repeatText;
            }
            return text;
        }

        public static void WriteSubMenuItems(string subHeader, string[] menuItems)
        {
            Console.WriteLine(_leftText + RepeatText(" ", _itemLeft + 2) + subHeader + RepeatText(" ", _consoleWidth - (6 + subHeader.Length)) + _rightText);
            Console.WriteLine(_leftText + RepeatText(" ", _itemLeft + 2) + RepeatText(" ", subHeader.Length) + RepeatText(" ", _consoleWidth - (6 + subHeader.Length)) + _rightText);
            foreach (string item in menuItems)
            {
                string text = _leftText + RepeatText(" ", _itemLeft + 2) + item + RepeatText(" ", _consoleWidth - (6 + item.Length)) + _rightText;
                Console.WriteLine(text);
            }
            Console.WriteLine(_emptyLine);
        }

        public static void WriteMenuItems(string[] menuItems)
        {
            string text;
            if(menuItems != null && menuItems.Length > 0)
            {
                foreach (string item in menuItems)
                {
                    text = _leftText + RepeatText(" ", _itemLeft) + item + RepeatText(" ", _consoleWidth - (4 + item.Length)) + _rightText;
                    Console.WriteLine(text);
                }
            }
            else
            {
                text = _leftText + RepeatText(" ", _itemLeft) + "No data to show" + RepeatText(" ", _consoleWidth - 4) + _rightText;
                Console.WriteLine(text);
            }
            Console.WriteLine(_emptyLine);
        }
    }
}
