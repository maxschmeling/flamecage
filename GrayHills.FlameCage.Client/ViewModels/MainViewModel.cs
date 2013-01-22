using System.Windows;
using System.Windows.Input;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Messages;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class MainViewModel : ViewModelBase
  {
    private readonly IEventAggregator eventAggregator;
    private WindowState currentWindowState;

    public WindowState CurrentWindowState
    {
      get { return currentWindowState; }
      set
      {
        if (currentWindowState == value) return;

        currentWindowState = value;
        this.NotifyPropertyChanged(vm => vm.CurrentWindowState);
      }
    }

    public ICommand MaximizeCommand { get; private set; }
    public ICommand MinimizeCommand { get; private set; }
    public ICommand RestoreCommand { get; private set; }
    public ICommand CloseCommand { get; private set; }

    public MainViewModel(IEventAggregator eventAggregator)
    {
      this.eventAggregator = eventAggregator;

      this.MaximizeCommand = new RelayCommand(Maximize);
      this.MinimizeCommand = new RelayCommand(Minimize);
      this.RestoreCommand = new RelayCommand(Restore);
      this.CloseCommand = new RelayCommand(Close);
    }

    private void Maximize()
    {
      this.CurrentWindowState = WindowState.Maximized;
    }

    private void Minimize()
    {
      this.CurrentWindowState = WindowState.Minimized;
    }

    private void Restore()
    {
      this.CurrentWindowState = WindowState.Normal;
    }

    private void Close()
    {
      this.eventAggregator.SendMessage(new ApplicationClosingMessage());
      Application.Current.Shutdown();
    }
  }
}
