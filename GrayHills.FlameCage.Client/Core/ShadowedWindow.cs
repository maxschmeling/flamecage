using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using GrayHills.FlameCage.Client.Interop;
using Color = System.Drawing.Color;

namespace GrayHills.FlameCage.Client.Core
{
  public class ShadowedWindow : Window
  {
    private HwndSource hwndSource;

    public ShadowedWindow()
    {
      WindowStyle = WindowStyle.None;

      Loaded += ShadowedWindow_OnLoaded;
    }

    public bool ShadowApplicationAttempted { get; private set; }

    private void ShadowedWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      hwndSource = PresentationSource.FromVisual((Visual) sender) as HwndSource;

      if (hwndSource != null)
        hwndSource.AddHook(WndProc);
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
      switch (msg)
      {
        case WindowsMessages.DwmCompositionChanged:
          TryApplyShadow();
          handled = true;
          break;
        case WindowsMessages.DwmColorizationColorChanged:
          handled = true;
          break;
        case WindowsMessages.ActivateNonClientArea:
          {
            var ptr = Win32.DefWindowProc(hwnd, msg, wParam, new IntPtr(-1));
            handled = true;
            return ptr;
          }
        case WindowsMessages.CalculateNonClientArea:
          if (wParam == new IntPtr(1))
          {
            handled = true;
          }
          break;

        case WindowsMessages.EraseBackground:
          Graphics.FromHdc(wParam).Clear(Color.White);
          handled = true;
          break;

        case WindowsMessages.GetMinMaxInfo:
          WmGetMinMaxInfo(hwnd, lParam);
          handled = true;
          break;

        case WindowsMessages.WindowPositionChanging:
          if (!ShadowApplicationAttempted)
          {
            TryApplyShadow();
          }
          break;
      }
      return IntPtr.Zero;
    }

    protected void ResizeWindow(ResizeDirection direction)
    {
      Win32.SendMessage(hwndSource.Handle, 0x112, (IntPtr) (0xf000 + direction), IntPtr.Zero);
    }

    public void TryApplyShadow()
    {
      if (Win32.IsDwmAvailable())
      {
        var margins = new Win32.MARGINS
                        {
                          bottomHeight = 1,
                          leftWidth = 0,
                          rightWidth = 0,
                          topHeight = 0
                        };
        var helper = new WindowInteropHelper(this);
        Win32.DwmExtendFrameIntoClientArea(helper.Handle, ref margins);
      }
      ShadowApplicationAttempted = true;
    }

    public void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
    {
      var structure = (MINMAXINFO) Marshal.PtrToStructure(lParam, typeof (MINMAXINFO));
      var hMonitor = Win32.MonitorFromWindow(hwnd, 2);
      if (hMonitor != IntPtr.Zero)
      {
        var monitorinfo = new MONITORINFO();
        Win32.GetMonitorInfo(hMonitor, monitorinfo);

        var rcWork = monitorinfo.rcWork;
        var rcMonitor = monitorinfo.rcMonitor;

        structure.ptMaxPosition.x = Math.Abs(rcWork.left - rcMonitor.left);
        structure.ptMaxPosition.y = Math.Abs(rcWork.top - rcMonitor.top);
        structure.ptMaxSize.x = Math.Abs(rcWork.right - rcWork.left);
        structure.ptMaxSize.y = Math.Abs(rcWork.bottom - rcWork.top);
        structure.ptMinTrackSize.x = (int) MinWidth;
        structure.ptMinTrackSize.y = (int) MinHeight;
      }
      Marshal.StructureToPtr(structure, lParam, true);
    }
  }
}