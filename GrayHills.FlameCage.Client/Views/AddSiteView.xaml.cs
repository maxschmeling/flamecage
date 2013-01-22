using System;
using System.Windows;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Messages;
using GrayHills.FlameCage.Client.ViewModels;
using GrayHills.FlameCage.Client.Views.Interfaces;

namespace GrayHills.FlameCage.Client.Views
{
  public partial class AddSiteView : IAddSiteView
  {
    private readonly IEventAggregator eventAggregator;

    public AddSiteView(AddSiteViewModel viewModel, IEventAggregator eventAggregator)
    {
      InitializeComponent();

      this.eventAggregator = eventAggregator;
      this.DataContext = viewModel;
      viewModel.PropertyChanged += viewModel_OnPropertyChanged;
    }

    private void viewModel_OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "DialogVisibility")
      {
        var vm = sender as AddSiteViewModel;

        if (vm != null)
        {
          switch (vm.DialogVisibility)
          {
            case Visibility.Visible:
              break;
            case Visibility.Hidden:
            case Visibility.Collapsed:
              CloseWindow();
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
        }
      }
    }

    private void CloseWindow()
    {
      eventAggregator.SendMessage(new OverlayMessage(false));
      Close();
    }

    private void ShadowedWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(SiteNameTextBox.Text))
      {
        SiteNameTextBox.Focus();
      }
      else
      {
        SiteNameTextBox.Focus();
        SiteNameTextBox.SelectAll();
      }
    }

    void IAddSiteView.Show()
    {
      Owner = Application.Current.MainWindow;

      ShowDialog();
    }
  }
}
