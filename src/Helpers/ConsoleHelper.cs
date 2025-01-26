using System.Diagnostics;

namespace Plugin.Toolkit.Security.Helpers
{
    internal class ConsoleHelper
    {
        public static void WriteLine(object value)
        {
#if DEBUG
            Print(value);
#endif
        }

        public static void WriteLine(Exception value)
        {
#if DEBUG
            Print(value.ToString());
#endif
        }

        private static void Print(object value)
        {
#if DEBUG
            string Platform = Environment.OSVersion.Platform.ToString().ToLower();
            if (Platform.Contains("win"))
            {
                Debug.WriteLine(value.ToString());
            }
            else
            {
                System.Console.WriteLine(value.ToString());
            }
#endif
        }
    }
}
