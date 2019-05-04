using System;
using System.Drawing;
using static Colorful.Console;

namespace Extensions
{
    public static class Print
    {
        public static void Yellow(object data)
        {
            WithColor(data, Color.GreenYellow);
        }

        public static void Green(object data)
        {
            WithColor(data, Color.Green);
        }
        public static void Done(object data)
        {
            Green(data);
            AudioManager.Play(AudioManager.Sent);
        }

        public static void White(object data)
        {
            WithColor(data, Color.White);
        }

        public static void Red(object data)
        {
            WithColor(data, Color.Red);
        }

        public static void Exception(Exception exception)
        {
            Red($"{DateTime.Now} :: Exception: {exception.Message}");
            //Red($"{DateTime.Now} :: Exception: {exception.ToString()}");

        }

        public static void WithColor(object data, Color color)
        {
            WriteLine(data, color);
            WriteLine();
        }
    }
}
