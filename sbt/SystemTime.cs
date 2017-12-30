using System;
using System.Runtime.InteropServices;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

// ReSharper disable InconsistentNaming

namespace systime
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SYSTEMTIME
    {
        [MarshalAs(UnmanagedType.U2)] public short Year;
        [MarshalAs(UnmanagedType.U2)] public short Month;
        [MarshalAs(UnmanagedType.U2)] public short DayOfWeek;
        [MarshalAs(UnmanagedType.U2)] public short Day;
        [MarshalAs(UnmanagedType.U2)] public short Hour;
        [MarshalAs(UnmanagedType.U2)] public short Minute;
        [MarshalAs(UnmanagedType.U2)] public short Second;
        [MarshalAs(UnmanagedType.U2)] public short Milliseconds;

        public SYSTEMTIME(FILETIME fileTIme)
        {
            if (!NativeMethods.FileTimeToSystemTime(ref fileTIme, out this))
            {
                Helpers.ThrowIfLastWin32ErrorIsNotZero();
            }
        }
    }
}
