using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using GrayHills.FlameCage.Client.Core;
using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class TranscriptViewModel : ViewModelBase
  {
    private readonly DateTime startDate = new DateTime(2000, 1, 1);
    private readonly DateTime endDate = DateTime.Today;
    private readonly ISite site;
    private readonly IRoom room;
    private readonly Dispatcher dispatcher;
    private readonly MessageViewModelFactory messageViewModelFactory;
    private string searchTerm;
    private DateTime searchDate;
    private RoomViewModel selectedRoom;

    public DateTime StartDate { get { return startDate; } }
    public DateTime EndDate { get { return endDate; } }

    public string SearchTerm
    {
      get { return searchTerm; }
      set
      {
        if (searchTerm == value) return;

        searchTerm = value;
        this.NotifyPropertyChanged(vm => vm.SearchTerm);
      }
    }

    public DateTime SearchDate
    {
      get { return searchDate; }
      set
      {
        if (searchDate == value) return;

        searchDate = value;
        this.NotifyPropertyChanged(vm => vm.SearchDate);
      }
    }

    public ObservableCollection<ViewModelBase> Messages { get; private set; }
    public ObservableCollection<RoomViewModel> Rooms { get; private set; }

    public RoomViewModel SelectedRoom
    {
      get { return selectedRoom; }
      set
      {
        if (selectedRoom == value) return;

        selectedRoom = value;
        this.NotifyPropertyChanged(vm => vm.SelectedRoom);
      }
    }

    public ICommand SearchByStringCommand { get; private set; }
    public ICommand SearchByDateCommand { get; private set; }

    public TranscriptViewModel(IRoom room, ISite site, string searchTerm, 
      DateTime searchDate, MessageViewModelFactory messageViewModelFactory, IEventAggregator eventAggregator)
    {
      this.room = room;
      this.site = site;

      this.SearchTerm = searchTerm;
      this.SearchDate = searchDate;

      this.dispatcher = Dispatcher.CurrentDispatcher;

      this.messageViewModelFactory = messageViewModelFactory;
      this.Messages = new ObservableCollection<ViewModelBase>();
      this.Rooms = new ObservableCollection<RoomViewModel>();

      if (!string.IsNullOrWhiteSpace(this.SearchTerm))
        SearchByString();
      else if (this.SearchDate != DateTime.MinValue)
        SearchByDate();

      if (SearchDate == DateTime.MinValue)
        SearchDate = DateTime.Today;

      this.SearchByStringCommand = new RelayCommand(SearchByString);
      this.SearchByDateCommand = new RelayCommand(SearchByDate);

      foreach (var r in this.site.Rooms)
        this.Rooms.Add(new RoomViewModel(site, null, r, eventAggregator));
    }

    private void SearchByString()
    {
      this.Messages.Clear();

      foreach (var m in this.site.Search(SearchTerm))
        AddMessageViewModel(m);
    }

    private void SearchByDate()
    {
      this.Messages.Clear();

      foreach (var m in this.room.GetTranscript(SearchDate))
        AddMessageViewModel(m);
    }

    private MessageViewModel AddMessageViewModel(Message msg)
    {
      MessageViewModel mvm = null;

      this.dispatcher.Invoke(() =>
      {
        var messages = this.Messages.Where(m => m is MessageGroupViewModel).Cast<MessageGroupViewModel>().SelectMany(mg => mg.Messages);

        if (messages.Any(m => m.ID == msg.ID)) return;

        lock (Messages)
        {
          if (Messages.Count() > 100) // todo: make this configurable
            Messages.Remove(Messages.First());

          mvm = this.messageViewModelFactory.BuildFor(msg);

          if (mvm is TextMessageViewModel || mvm is PasteMessageViewModel)
          {
            if (Messages.Any() && Messages.Last() is MessageGroupViewModel &&
              ((MessageGroupViewModel)Messages.Last()).Messages.First().User.ID == mvm.User.ID)
            {
              ((MessageGroupViewModel)Messages.Last()).Messages.Add(mvm);
            }
            else
            {
              var mgvm = new MessageGroupViewModel(mvm.User);
              mgvm.Messages.Add(mvm);
              Messages.Add(mgvm);
            }
          }
          else
          {
            Messages.Add(mvm);
          }
        }
      });

      return mvm;
    }
  }
}
