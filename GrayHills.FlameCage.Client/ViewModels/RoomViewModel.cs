using System.Windows.Threading;
using GrayHills.FlameCage.Client.Core;
using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class RoomViewModel : ViewModelBase
  {
    private readonly Dispatcher dispatcher;
    private readonly IRoom room;
    private readonly ISite site;
    private readonly Models.Site siteModel;

    private string name, topic;
    private bool isActive;
    private int unreadMessageCount;
    private IEventAggregator eventAggregator;

    public IRoom Room { get { return room; } }

    public string Name
    {
      get { return name; }
      set
      {
        if (name != value)
        {
          name = value;

          this.NotifyPropertyChanged(vm => vm.Name);
        }
      }
    }
    public string Topic
    {
      get { return topic; }
      set
      {
        if (topic != value)
        {
          topic = value;

          this.NotifyPropertyChanged(vm => vm.Topic);
        }
      }
    }
    public int UnreadMessageCount
    {
      get { return unreadMessageCount; }
      set
      {
        if (unreadMessageCount == value) return;

        unreadMessageCount = value;
        this.NotifyPropertyChanged(vm => vm.UnreadMessageCount);
      }
    }
    public bool IsActive
    {
      get { return isActive; }
      set
      {
        if (isActive == value) return;

        isActive = value;
        this.NotifyPropertyChanged(vm => vm.IsActive);
      }
    }

    public string SiteName { get { return site.Name; } }
    public System.Windows.Media.SolidColorBrush SiteBackgroundBrush
    {
      get
      {
        return new System.Windows.Media.SolidColorBrush(siteModel.Color);
      }
    }
    
    public RoomViewModel(ISite site, Models.Site siteModel, IRoom room, IEventAggregator eventAggregator)
    {
      this.eventAggregator = eventAggregator;
      this.site = site;
      this.room = room;
      this.siteModel = siteModel;

      Name = room.Name;
      Topic = room.Topic;

      dispatcher = Dispatcher.CurrentDispatcher;
    }
  }
}
