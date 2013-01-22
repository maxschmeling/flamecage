using System.Windows.Input;
using System.Windows.Media;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Messages;
using GrayHills.FlameCage.Client.Properties;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class SettingsViewModel : ViewModelBase
  {
    private readonly IEventAggregator eventAggregator;

    public Color PrimaryColor { get; set; }
    public bool RunOnSystemStartup { get; set; }

    public ICommand SaveChangesCommand { get; private set; }
    public ICommand CancelCommand { get; private set; }

    public SettingsViewModel(IEventAggregator eventAggregator)
    {
      this.eventAggregator = eventAggregator;

      SaveChangesCommand = new RelayCommand(x => SaveChanges());
      CancelCommand = new RelayCommand(x => Cancel());
    }

    private void SaveChanges()
    {
      Settings.Default.Save();

      eventAggregator.SendMessage(new GoToStateMessage { State = "RoomView" });
    }

    private void Cancel()
    {
      Settings.Default.Reset();

      eventAggregator.SendMessage(new GoToStateMessage { State = "RoomView" });
    }
  }
}
