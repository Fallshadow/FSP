using System;
using fsp.utility;
using UnityEngine;

namespace fsp.debug
{
    public static class PrintSystem
    {
        [Flags]
        public enum PrintBy : int
        {
            SunShuChao = 1 << 1,
            unknown = 1 << 31,
        }


        [Flags]
        public enum SystemPrintBy : int
        {
            Camera = 1 << 1,
            unknown = 1 << 31,
        }

        private static PrintBy outPutLogger = 0;

        // 外部设置logger
        public static void SetOutPutLogger(PrintBy logger)
        {
            outPutLogger = logger;
        }

        [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]
        public static void Log(string str, PrintBy pb = PrintBy.unknown)
        {
            if (!isSelectedPrinter(pb)) return;

            string title = printerTitle(pb);
            // Debuger.Log($"{title} {str}");
            Debug.Log($"{title} {str}");
        }

        [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]
        public static void Log(string str, Color color, PrintBy pb = PrintBy.unknown)
        {
            if (!isSelectedPrinter(pb)) return;

            string title = printerTitle(pb);
            Debuger.Log(Utility.GetColoredString(title, str, color));
        }

        [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]
        public static void LogWarning(string str, PrintBy pb = PrintBy.unknown)
        {
            if (!isSelectedPrinter(pb)) return;

            string title = printerTitle(pb);
            Debuger.LogWarning($"{title} {str}");
            Debuger.LogWarning($"{title} {str}");
        }

        [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]
        public static void LogWarning(string str, Color color, PrintBy pb = PrintBy.unknown)
        {
            if (isSelectedPrinter(pb))
            {
                string title = printerTitle(pb);
                Debuger.LogWarning(Utility.GetColoredString(title, str, color));
            }
        }

        [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]
        public static void LogError(string str, PrintBy pb = PrintBy.unknown)
        {
            if (CheckPrinter(pb)) return;

            string title = printerTitle(pb);
            Debuger.LogError($"{title} {str}");
        }


        private static bool CheckPrinter(PrintBy pb)
        {
            return pb != PrintBy.unknown && !isSelectedPrinter(pb);
        }

        private static bool isSelectedPrinter(PrintBy pb)
        {
            return (outPutLogger & pb) == pb;
        }

        private static string printerTitle(PrintBy pb)
        {
            return pb == PrintBy.unknown ? "" : $"[{pb}]";
        }
    }
}