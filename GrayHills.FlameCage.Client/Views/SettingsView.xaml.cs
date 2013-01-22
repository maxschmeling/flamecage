using System.Windows.Controls;
using GrayHills.FlameCage.Client.ViewModels;
using GrayHills.FlameCage.Client.Views.Interfaces;

namespace GrayHills.FlameCage.Client.Views
{
  /// <summary>
  /// Interaction logic for SettingsView.xaml
  /// </summary>
  public partial class SettingsView : UserControl, ISettingsView
  {
    public SettingsView()
    {
      InitializeComponent();
    }

    public SettingsView(SettingsViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;
    }
  }
}
