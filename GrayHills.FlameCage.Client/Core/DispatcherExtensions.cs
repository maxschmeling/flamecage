using System;
using System.Windows.Threading;

namespace GrayHills.FlameCage.Client.Core
{
  public static class DispatcherExtensions
  {
    public static void Invoke(this Dispatcher dispatcher, Action action)
    {
      dispatcher.Invoke((Delegate)action, null);
    }
  }
}
