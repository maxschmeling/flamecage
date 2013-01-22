using System.Windows;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Views.Interfaces;

namespace GrayHills.FlameCage.Client.Views
{
  public partial class ActivationView : ShadowedWindow, IActivationView
  {
    public ActivationView()
    {
      InitializeComponent();
    }

    void IActivationView.Show()
    {
      this.Owner = Application.Current.MainWindow;

      this.ShowDialog();
    }

    private void AddSiteButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      this.DialogResult = true;
      this.Close();
    }
  }
}
