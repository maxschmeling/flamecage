using System.Windows;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Messages;
using GrayHills.FlameCage.Client.ViewModels;
using GrayHills.FlameCage.Client.Views.Interfaces;

namespace GrayHills.FlameCage.Client.Views
{
  public partial class TranscriptView : ShadowedWindow, ITranscriptView
  {
    private readonly IEventAggregator eventAggregator;

    public TranscriptView(TranscriptViewModel viewModel, IEventAggregator eventAggregator)
    {
      InitializeComponent();

      this.DataContext = viewModel;

      this.eventAggregator = eventAggregator;
    }

    void ITranscriptView.Show()
    {
      this.Owner = Application.Current.MainWindow;

      this.ShowDialog();
    }

    private void DoneButton_Click(object sender, RoutedEventArgs e)
    {
      this.eventAggregator.SendMessage(new OverlayMessage(false));
      this.Close();
    }
  }
}
