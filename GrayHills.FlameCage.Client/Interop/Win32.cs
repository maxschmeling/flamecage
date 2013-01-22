using System;
using System.Runtime.InteropServices;

namespace GrayHills.FlameCage.Client.Interop
{
  public static class Win32
  {
    [DllImport(Assemblies.User32, CharSet = CharSet.Auto)]
    internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport(Assemblies.User32)]
    internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

    [DllImport(Assemblies.User32)]
    internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

    [DllImport(Assemblies.Shell32)]
    public static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] 
      Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);

    [DllImport("user32.dll")]
    public static extern IntPtr DefWindowProc(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);
    public static int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins)
    {
      var hModule = LoadLibrary("dwmapi");
      if (hModule == IntPtr.Zero)
      {
        return 0;
      }
      var procAddress = GetProcAddress(hModule, "DwmExtendFrameIntoClientArea");
      if (procAddress == IntPtr.Zero)
      {
        return 0;
      }
      var delegateForFunctionPointer = (DwmExtendFrameIntoClientAreaDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DwmExtendFrameIntoClientAreaDelegate));
      return delegateForFunctionPointer(hwnd, ref margins);
    }

    public static DWMCOLORIZATIONPARAMS DwmGetColorizationParameters()
    {
      var delegateForFunctionPointer = (DwmGetColorizationParametersDelegate)Marshal.GetDelegateForFunctionPointer(GetProcAddressByOrdinal(LoadLibrary("dwmapi"), 0x7f), typeof(DwmGetColorizationParametersDelegate));
      var dwmColorParams = new DWMCOLORIZATIONPARAMS();
      delegateForFunctionPointer(ref dwmColorParams);
      return dwmColorParams;
    }

    public static int DwmSetWindowAttribute(IntPtr hwnd, uint dwAttribute, ref IntPtr pvAttribute, uint cbAttribute)
    {
      var hModule = LoadLibrary("dwmapi");
      if (hModule == IntPtr.Zero)
      {
        return 0;
      }
      var procAddress = GetProcAddress(hModule, "DwmSetWindowAttribute");
      if (procAddress == IntPtr.Zero)
      {
        return 0;
      }
      var delegateForFunctionPointer = (DwmSetWindowAttributeDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DwmSetWindowAttributeDelegate));
      return delegateForFunctionPointer(hwnd, dwAttribute, ref pvAttribute, cbAttribute);
    }

    [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
    [DllImport("kernel32", EntryPoint = "GetProcAddress", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern IntPtr GetProcAddressByOrdinal(IntPtr hModule, ushort ordinal);
    public static bool IsDwmAvailable()
    {
      if (LoadLibrary("dwmapi") == IntPtr.Zero)
      {
        return false;
      }
      return true;
    }

    [DllImport("kernel32", SetLastError = true)]
    private static extern IntPtr LoadLibrary(string lpFileName);

    [StructLayout(LayoutKind.Sequential)]
    public struct DWMCOLORIZATIONPARAMS
    {
      public uint ColorizationColor;
      public uint ColorizationAfterglow;
      public uint ColorizationColorBalance;
      public uint ColorizationAfterglowBalance;
      public uint ColorizationBlurBalance;
      public uint ColorizationGlassReflectionIntensity;
      public uint ColorizationOpaqueBlend;
    }

    private delegate int DwmExtendFrameIntoClientAreaDelegate(IntPtr hwnd, ref Win32.MARGINS margins);

    private delegate int DwmGetColorizationParametersDelegate(ref Win32.DWMCOLORIZATIONPARAMS dwmColorParams);

    private delegate int DwmSetWindowAttributeDelegate(IntPtr hwnd, uint dwAttribute, ref IntPtr pvAttribute, uint cbAttribute);

    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
      public int leftWidth;
      public int rightWidth;
      public int topHeight;
      public int bottomHeight;
    }

    public enum WindowMessages
    {
      SC_MINIMIZE = 0xf020,
      SC_RESTORE = 0xf120,
      WM_DWMCOLORIZATIONCOLORCHANGED = 800,
      WM_DWMCOMPOSITIONCHANGED = 0x31e,
      WM_ERASEBKGND = 20,
      WM_GETMINMAXINFO = 0x24,
      WM_NCACTIVATE = 0x86,
      WM_NCCALCSIZE = 0x83,
      WM_SYSCOMMAND = 0x112,
      WM_WINDOWPOSCHANGING = 70
    }

    public enum wParams
    {
      SC_SIZE = 0xf000
    }
  }
}
