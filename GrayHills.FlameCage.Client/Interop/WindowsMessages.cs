namespace GrayHills.FlameCage.Client.Interop
{
  public static class WindowsMessages
  {
    /// <summary>
    /// Sent to all top-level windows when Desktop Window 
    /// Manager (DWM) composition has been enabled or disabled.
    /// </summary>
    public const int DwmCompositionChanged = 0x31e;

    /// <summary>
    /// Sent to a window when its nonclient area needs to 
    /// be changed to indicate an active or inactive state.
    /// </summary>
    public const int ActivateNonClientArea = 0x86;

    /// <summary>
    /// Sent when the size and position of a window's client area 
    /// must be calculated. By processing this message, an application 
    /// can control the content of the window's client area when the 
    /// size or position of the window changes.
    /// </summary>
    public const int CalculateNonClientArea = 0x83;

    /// <summary>
    /// Sent to a window whose size, position, or place in 
    /// the Z order is about to change as a result of a call 
    /// to the SetWindowPos function or another window-management function.
    /// </summary>
    public const int WindowPositionChanging = 0x46;

    /// <summary>
    /// Sent to a window when the size or position of the window is about to 
    /// change. An application can use this message to override the window's 
    /// default maximized size and position, or its default minimum or maximum tracking size.
    /// </summary>
    public const int GetMinMaxInfo = 0x24;

    /// <summary>
    /// Sent when the window background must be erased (for 
    /// example, when a window is resized). The message is sent to 
    /// prepare an invalidated portion of a window for painting.
    /// </summary>
    public const int EraseBackground = 0x14;

    /// <summary>
    /// Sent to all top-level windows when the colorization color has changed.
    /// </summary>
    public const int DwmColorizationColorChanged = 0x320;
  }
}
