using System.Collections.ObjectModel;
using System.Linq;
using GrayHills.FlameCage.Client.Core;
using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class RoomListSiteViewModel
  {
    public ObservableCollection<RoomViewModel> Rooms { get; private set; }

    public RoomListSiteViewModel(Models.Site modelSite, ISite campfireSite, IEventAggregator eventAggregator)
    {
      Rooms = new ObservableCollection<RoomViewModel>(
        campfireSite.GetRooms().Select(r => new RoomViewModel(campfireSite, modelSite, r, eventAggregator)));
    }
  }
}
