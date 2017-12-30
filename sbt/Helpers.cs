using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace systime
{
    internal class Helpers
    {
        public static void ThrowIfLastWin32ErrorIsNotZero()
        {
            var lastError = Marshal.GetLastWin32Error();
            if (lastError == 0)
            {
                return;
            }
            throw new Win32Exception(lastError);
        }

        public static TimeSpan GetTimeSpanFromFileTime(FILETIME time)
        {
            return TimeSpan.FromMilliseconds((((ulong)time.dwHighDateTime << 32) + (uint)time.dwLowDateTime) * 0.000001);
        }

        public static void GetSystemTimes(out TimeSpan idleTime, out TimeSpan kernelTime, out TimeSpan userTime)
        {
            var success = NativeMethods.GetSystemTimes(out var sIdleTime, out var sKernelTime, out var sUserTime);
            if (!success)
            {
                ThrowIfLastWin32ErrorIsNotZero();
            }

            idleTime = GetTimeSpanFromFileTime(sIdleTime);
            kernelTime = GetTimeSpanFromFileTime(sKernelTime);
            userTime = GetTimeSpanFromFileTime(sUserTime);
        }
    }
}
