using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Messages;
using GrayHills.FlameCage.Client.Models;
using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class RoomListViewModel : ViewModelBase,
    IListener<RoomSelectedMessage>,
    IListener<MessageReceivedMessage>,
    IListener<SiteAddedMessage>,
    IListener<RoomLeftMessage>,
    IListener<SiteDeletedMessage>
  {
    private readonly JoinedRoomHolder joinedRoomHolder;
    private readonly Dispatcher dispatcher;
    private readonly IRepository repository;

    private IEventAggregator eventAggregator;
    private RoomViewModel selectedRoom;
    private RoomViewModel previouslySelectedRoom;
    private List<ISite> sites;

    public ObservableCollection<RoomViewModel> Rooms { get; private set; }

    public RoomViewModel SelectedRoom
    {
      get { return selectedRoom; }
      set
      {
        if (selectedRoom == value) return;

        previouslySelectedRoom = selectedRoom;
        selectedRoom = value;
        this.NotifyPropertyChanged(vm => vm.SelectedRoom);
      }
    }

    public ICommand LoadRoomCommand { get; private set; }
    public ICommand LoadDataCommand { get; private set; }
    public ICommand AddSiteCommand { get; private set; }
    public ICommand ViewSitesCommand { get; private set; }

    public RoomListViewModel(IEventAggregator eventAggregator,
      IRepository repository, JoinedRoomHolder joinedRoomHolder)
    {
      this.eventAggregator = eventAggregator;
      this.dispatcher = Dispatcher.CurrentDispatcher;
      this.sites = new List<ISite>();
      this.joinedRoomHolder = joinedRoomHolder;
      this.repository = repository;

      this.Rooms = new ObservableCollection<RoomViewModel>();

      this.LoadRoomCommand = new RelayCommand(x => LoadRoom((RoomViewModel)x));
      this.LoadDataCommand = new AsyncRelayCommand(LoadData);
      this.AddSiteCommand = new RelayCommand(x => this.eventAggregator.SendMessage(new PromptForNewSiteMessage()));
      this.ViewSitesCommand = new RelayCommand(x => this.eventAggregator.SendMessage(new DisplaySiteListMessage()));
    }

    private void LoadRoom(RoomViewModel roomViewModel)
    {
      if (roomViewModel != previouslySelectedRoom)
      {
        var selectRoom = new Task(() => eventAggregator.SendMessage(
                                        new RoomSelectedMessage(roomViewModel == null
                                    ? (IRoom) null
                                    : roomViewModel.Room)));
        selectRoom.Start();
      }
    }

    public void Handle(RoomSelectedMessage message)
    {
      if (message.Room == null)
        SelectedRoom = null;
      else
      {
        SelectedRoom = Rooms.SingleOrDefault(r => r.Room.ID == message.Room.ID);

        if (SelectedRoom != null)
        {
          SelectedRoom.IsActive = true;
          SelectedRoom.UnreadMessageCount = 0;
        }
      }
    }

    public void Handle(RoomLeftMessage message)
    {
      var room = this.Rooms.SingleOrDefault(r => r.Room.ID == message.Room.ID);

      if (room != null)
        room.IsActive = false;
    }

    public void Handle(MessageReceivedMessage message)
    {
      var roomViewModel = this.Rooms.Single(r => r.Room.ID == message.Message.Room.ID);

      if ((message.Message.Type == MessageType.Text ||
           message.Message.Type == MessageType.Paste) &&
          (joinedRoomHolder.CurrentRoom != null &&
           roomViewModel.Room.ID != joinedRoomHolder.CurrentRoom.Room.ID))
      {
        roomViewModel.UnreadMessageCount++;
      }
    }

    public void Handle(SiteAddedMessage message)
    {
      foreach (var room in message.SiteApi.GetRooms()
        .Select(r => new RoomViewModel(message.SiteApi, message.SiteModel, r, eventAggregator)))
      {
        dispatcher.Invoke(() => this.Rooms.Add(room));
      }
    }

    private void LoadData()
    {
      // hack: should this use automapper?
      foreach (var site in repository.All<Models.Site>())
      {
        this.sites.Add(new GrayHills.Matches.Site()
                         {
                           Name = site.Name,
                           ApiToken = site.ApiAuthToken,
                           Username = site.Username,
                           Credentials = new NetworkCredential(site.ApiAuthToken, "X")
                         });
      }

      foreach (var room in sites.SelectMany(s => s.GetRooms()
                                                   .Select(
                                                     r =>
                                                     new RoomViewModel(s,
                                                                       this.repository.
                                                                         SingleOrDefault
                                                                         <Models.Site>(
                                                                           sm =>
                                                                           sm.Name ==
                                                                           s.Name), r,
                                                                       eventAggregator)))
        )
      {
        this.dispatcher.Invoke(() => this.Rooms.Add(room));
      }
    }

    void IListener<SiteDeletedMessage>.Handle(SiteDeletedMessage message)
    {
      for (var i = this.Rooms.Count() - 1; i >= 0; i--)
      {
        var room = this.Rooms.ElementAt(i);

        if (room.SiteName == message.Site.Name)
          this.dispatcher.Invoke(() => this.Rooms.Remove(room));
      }
    }
  }
}
