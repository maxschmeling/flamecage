using System.Diagnostics;
using System.Windows;
using GrayHills.FlameCage.Client.Core;

namespace GrayHills.FlameCage.Client.Views
{
  public partial class ExpiredView : ShadowedWindow
  {
    public ExpiredView()
    {
      InitializeComponent();
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Process.Start(@"http://www.grayhills.com/products-services/openflame/download/");
    }

    private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }
  }
}
