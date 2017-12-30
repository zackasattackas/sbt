using System.Runtime.InteropServices;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace systime
{
    internal class NativeMethods
    {
        [DllImport("kernel32.dll", EntryPoint = "GetTickCount64", SetLastError = true)]
        public static extern ulong GetTickCount64();

        [DllImport("kernel32.dll", EntryPoint = "GetSystemTimes", SetLastError = true)]
        public static extern bool GetSystemTimes([Out] out FILETIME lpIdleTime, [Out] out FILETIME lpKernelTime, [Out] out FILETIME lpUserTime);

        [DllImport("kernel32.dll", EntryPoint = "FileTimeToSystemTime", CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        public static extern bool FileTimeToSystemTime([In] ref FILETIME lpFileTime, [Out] out SYSTEMTIME lpSystemTime);
    }
}
