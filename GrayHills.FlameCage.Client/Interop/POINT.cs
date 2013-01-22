using System.Runtime.InteropServices;

namespace GrayHills.FlameCage.Client.Interop
{
  [StructLayout(LayoutKind.Sequential)]
  public struct POINT
  {
    public int x;
    public int y;

    public POINT(int x, int y)
    {
      this.x = x;
      this.y = y;
    }
  }
}
