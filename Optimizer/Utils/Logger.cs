using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Utils
{
    public static class Logger
    {
        private static StringBuilder Logs = new StringBuilder();

        public static void Log(string message)
        {
            Logs.Append(message);
            Logs.AppendLine();
        }

        public static string DumpLog()
        {
            return Logs.ToString();
        }

        public static void Log(string text, params object[] args)
        {
            Log(string.Format(text, args));
        }
    }
}
