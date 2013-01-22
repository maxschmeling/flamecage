using System.Windows.Controls;
using GrayHills.FlameCage.Client.Views.Interfaces;

namespace GrayHills.FlameCage.Client.Views
{
  public partial class SignInView : UserControl, ISignInView
  {
    public SignInView()
    {
      InitializeComponent();

      //this.DataContext = new GrayHills.FlameCage.Client.ViewModels.SignInViewModel(siteID);
    }

    private void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      
    }

    public void Show()
    {
      this.Visibility = System.Windows.Visibility.Visible;
    }
  }
}
