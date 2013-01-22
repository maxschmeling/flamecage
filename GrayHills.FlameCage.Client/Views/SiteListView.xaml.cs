using System.Windows;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Messages;
using GrayHills.FlameCage.Client.ViewModels;
using GrayHills.FlameCage.Client.Views.Interfaces;

namespace GrayHills.FlameCage.Client.Views
{
  public partial class SiteListView : ShadowedWindow, ISiteListView
  {
    private IEventAggregator eventAggregator;

    public SiteListView(SiteListViewModel viewModel, IEventAggregator eventAggregator)
    {
      InitializeComponent();

      this.eventAggregator = eventAggregator;
      this.DataContext = viewModel;
    }

    private void DoneButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      this.eventAggregator.SendMessage(new OverlayMessage(false));
      this.Close();
    }

    void ISiteListView.Show()
    {
      this.Owner = Application.Current.MainWindow;

      this.ShowDialog();
    }
  }
}
