using System;
using System.Linq;
#if DEBUG
using System.Diagnostics;
#endif

namespace systime
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                Helpers.GetSystemTimes(out var idleTime, out var kernelTime, out var userTime);
                var ticks = (long) NativeMethods.GetTickCount64();
                var uptime = TimeSpan.FromMilliseconds(ticks);
                var systemTime = DateTime.Now;
                var systemTimeUtc = systemTime.ToUniversalTime();
                var timeZone = TimeZoneInfo.Local;
                var bootTime = systemTime.Subtract(uptime);

                Print();
                Print("System Time (UTC)", GetPrintableDateTime(systemTimeUtc));
                Print("System Time", GetPrintableDateTime(systemTime));
                Print("Time Zone", timeZone.DisplayName);
                Print("UTC Offset", timeZone.BaseUtcOffset);
                Print("Daylight Savings", timeZone.IsDaylightSavingTime(systemTime));
                Print("Tick Count", ticks);
                Print("Last Boot Time", GetPrintableDateTime(bootTime));
                Print("System Up Time", GetPrintableTimeSpan(uptime));
                Print("CPU Idle Time", GetPrintableTimeSpan(idleTime));
                Print("Kernel Mode", GetPrintableTimeSpan(kernelTime));
                Print("User Mode", GetPrintableTimeSpan(userTime));
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
            }

#if DEBUG
            PauseConsole();

            void PauseConsole()
            {
                if (!Debugger.IsAttached)
                {
                    return;
                }
                Console.Write("\r\nPress any key to exit...");
                Console.ReadKey();
            }
#endif
        }


        private static void Print(params object[] input)
        {
            const string format = " {0,17} : {1}";
            if (input == null || !input.Any())
            {
                Console.WriteLine();
                return;
            }
            Console.WriteLine(format, input);
        }

        private static string GetPrintableDateTime(DateTime dt)
        {
            const string dateFormat = "MM/dd/yyyy hh:mm:ss tt";
            return dt.ToString(dateFormat);
        }

        private static string GetPrintableTimeSpan(TimeSpan t)
        {
            return t.ToString().PadLeft(20, '0');
        }
    }
}
