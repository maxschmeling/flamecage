using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Messages;
using GrayHills.Matches;
using Microsoft.Win32;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class ActiveRoomViewModel : ViewModelBase,
                                     IListener<RoomSelectedMessage>,
                                     IListener<ApplicationClosingMessage>
  {
    #region Fields

    private readonly Dispatcher dispatcher;
    private readonly IEventAggregator eventAggregator;
    private readonly JoinedRoomHolder joinedRoomHolder;
    private readonly MessageViewModelFactory messageViewModelFactory;

    private bool isLocked;
    private string newMessageText;
    private IRoom room;
    private string roomName;
    private string searchTerm;

    #endregion

    #region Properties

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

    public string NewMessageText
    {
      get { return newMessageText; }
      set
      {
        if (newMessageText == value) return;

        newMessageText = value;
        this.NotifyPropertyChanged(vm => vm.NewMessageText);
      }
    }

    public string RoomName
    {
      get { return roomName; }
      set
      {
        if (roomName == value) return;

        roomName = value == null ? value : value.ToLower();
        this.NotifyPropertyChanged(vm => vm.RoomName);
      }
    }

    public bool IsLocked
    {
      get { return isLocked; }
      set
      {
        if (isLocked == value) return;

        isLocked = value;
        this.NotifyPropertyChanged(vm => vm.IsLocked);
      }
    }

    public ObservableCollection<ViewModelBase> Messages { get; private set; }
    public ObservableCollection<UploadObjectViewModel> Uploads { get; private set; }
    public ObservableCollection<UserViewModel> Users { get; private set; }

    #endregion

    #region Command Properties

    public ICommand SendMessageCommand { get; private set; }
    public ICommand UploadFileCommand { get; private set; }
    public ICommand LeaveCommand { get; private set; }
    public ICommand LockCommand { get; private set; }
    public ICommand SearchTranscriptCommand { get; private set; }
    public ICommand UploadDroppedFileCommand { get; private set; }

    #endregion

    #region Constructor(s)

    public ActiveRoomViewModel(JoinedRoomHolder joinedRoomHolder, Dispatcher dispatcher,
                               IEventAggregator eventAggregator, MessageViewModelFactory messageViewModelFactory)
    {
      this.eventAggregator = eventAggregator;
      this.joinedRoomHolder = joinedRoomHolder;
      this.messageViewModelFactory = messageViewModelFactory;
      this.dispatcher = dispatcher;

      Users = new ObservableCollection<UserViewModel>();
      Uploads = new ObservableCollection<UploadObjectViewModel>();
      Messages = new ObservableCollection<ViewModelBase>();

      SendMessageCommand = new RelayCommand(x => SendMessage(), x => room != null && !String.IsNullOrEmpty(NewMessageText));
      UploadFileCommand = new RelayCommand(x => UploadFile(), x => room != null);
      LeaveCommand = new RelayCommand(x => Leave(), x => room != null);
      LockCommand = new RelayCommand(x => Lock(), x => room != null);
      SearchTranscriptCommand = new RelayCommand(x => SearchTranscript(), x => room != null);
      UploadDroppedFileCommand = new RelayCommand<string>(UploadDroppedFile);
    }

    #endregion

    #region Event Handler Methods

    private void activeRoom_OnMessageReceived(object sender, MessageEventArgs e)
    {
      AddMessageViewModel(e.Message);
    }

    private void activeRoom_OnUserJoined(object sender, UserEventArgs e)
    {
      var userViewModel = new UserViewModel(e.User);

      dispatcher.Invoke(() => Users.Add(userViewModel));
    }

    private void activeRoom_OnUserLeft(object sender, UserEventArgs e)
    {
      var userViewModel = Users.Single(vm => vm.ID == e.User.ID);

      dispatcher.Invoke(() => Users.Remove(userViewModel));
    }

    private void activeRoom_OnUserKicked(object sender, UserEventArgs e)
    {
      var userViewModel = Users.Single(vm => vm.ID == e.User.ID);

      dispatcher.Invoke(() => Users.Remove(userViewModel));
    }

    private void activeRoom_OnFileUploaded(object sender, MessageEventArgs e)
    {
      var uploadObject = e.Message.Room.GetUploadObject(e.Message.ID);

      dispatcher.Invoke(() => Uploads.Add(new UploadObjectViewModel(uploadObject)));
    }

    #endregion

    #region Methods

    private void SearchTranscript()
    {
      var currentRoom = joinedRoomHolder.CurrentRoom.Room;

      eventAggregator.SendMessage(new ViewTranscriptMessage(currentRoom.Site, currentRoom, SearchTerm));

      SearchTerm = string.Empty;
    }

    private void UploadDroppedFile(string fileName)
    {
      room.UploadFile(File.Open(fileName, FileMode.Open), Path.GetFileName(fileName));
    }

    private void HookupActiveRoomEvents()
    {
      joinedRoomHolder.CurrentRoom.MessageReceived += activeRoom_OnMessageReceived;
      joinedRoomHolder.CurrentRoom.UserJoined += activeRoom_OnUserJoined;
      joinedRoomHolder.CurrentRoom.UserKicked += activeRoom_OnUserKicked;
      joinedRoomHolder.CurrentRoom.UserLeft += activeRoom_OnUserLeft;
      joinedRoomHolder.CurrentRoom.FileUploaded += activeRoom_OnFileUploaded;
    }

    private void ClearActiveRoomEvents()
    {
      if (joinedRoomHolder.CurrentRoom == null) return;

      joinedRoomHolder.CurrentRoom.MessageReceived -= activeRoom_OnMessageReceived;
      joinedRoomHolder.CurrentRoom.UserJoined -= activeRoom_OnUserJoined;
      joinedRoomHolder.CurrentRoom.UserKicked -= activeRoom_OnUserKicked;
      joinedRoomHolder.CurrentRoom.UserLeft -= activeRoom_OnUserLeft;
      joinedRoomHolder.CurrentRoom.FileUploaded -= activeRoom_OnFileUploaded;
    }

    public void LoadMessages()
    {
      foreach (var msg in joinedRoomHolder.CurrentRoom.Messages)
      {
        AddMessageViewModel(msg);
      }
    }

    public void LoadUploads()
    {
      foreach (var upload in joinedRoomHolder.CurrentRoom.Room.RecentUploads)
      {
        Uploads.Add(new UploadObjectViewModel(upload));
      }
    }

    private MessageViewModel AddMessageViewModel(Message msg)
    {
      MessageViewModel mvm = null;

      dispatcher.Invoke(() =>
                          {
                            var messages =
                              Messages.Where(m => m is MessageGroupViewModel).Cast<MessageGroupViewModel>().SelectMany(
                                mg => mg.Messages);

                            if (messages.Any(m => m.ID == msg.ID)) return;

                            lock (Messages)
                            {
                              if (Messages.Count() > 100)
                                Messages.Remove(Messages.First());

                              mvm = messageViewModelFactory.BuildFor(msg);

                              if (mvm is LockMessageViewModel)
                              {
                                IsLocked = true;
                              }
                              else if (mvm is UnlockMessageViewModel)
                              {
                                IsLocked = false;
                              }

                              if (mvm is TextMessageViewModel || mvm is PasteMessageViewModel || mvm is UploadMessageViewModel)
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

    private void SendMessage()
    {
      var messageText = NewMessageText;

      var vm = AddMessageViewModel(new Message(room.Site)
                                                  {
                                                    Body = messageText,
                                                    User = room.Site.GetMe()
                                                  });
      NewMessageText = string.Empty;

      new Action(() => { vm.ID = room.Say(messageText).ID; }).BeginInvoke(null, null);
    }

    private void UploadFile()
    {
      // Configure open file dialog box
      var dlg = new OpenFileDialog { Filter = "All Files|*.*" };

      // Show open file dialog box
      var result = dlg.ShowDialog();

      // Process open file dialog box results
      if (result == true)
      {
        // Open document
        room.UploadFile(File.OpenRead(dlg.FileName), Path.GetFileName(dlg.FileName));
      }
    }

    private void Leave()
    {
      joinedRoomHolder.RemoveRoom(room.ID);

      room.Leave();

      eventAggregator.SendMessage(new RoomLeftMessage(room));

      IRoom roomToSelect = null;

      if (joinedRoomHolder.Rooms.Any())
      {
        roomToSelect = joinedRoomHolder.Rooms.First().Room;
      }

      eventAggregator.SendMessage(new RoomSelectedMessage(roomToSelect));
    }

    private void Lock()
    {
      if (IsLocked) // this is reversed logic because of the order things get set
      {
        room.Lock();
        IsLocked = true;
      }
      else
      {
        room.Unlock();
        IsLocked = false;
      }
    }

    #endregion

    #region Message Listener Methods

    void IListener<RoomSelectedMessage>.Handle(RoomSelectedMessage message)
    {
      ClearActiveRoomEvents();

      dispatcher.Invoke(() =>
                          {
                            Users.Clear();
                            Uploads.Clear();
                            Messages.Clear();
                          });

      room = message.Room;

      if (room != null)
      {
        IActiveRoom newActiveRoom;

        if (!joinedRoomHolder.Rooms.Any(jr => jr.Room.ID == room.ID))
        {
          room.Join();

          newActiveRoom = joinedRoomHolder.AddRoom(room);
        }
        else
        {
          newActiveRoom = joinedRoomHolder.Rooms.Single(r => r.Room.ID == room.ID);
        }

        dispatcher.Invoke(() => room.Users.ForEach(u => Users.Add(new UserViewModel(u))));

        joinedRoomHolder.CurrentRoom = newActiveRoom;

        dispatcher.Invoke(() => RoomName = newActiveRoom.Room.Name);

        HookupActiveRoomEvents();

        dispatcher.Invoke(LoadMessages);
        dispatcher.Invoke(LoadUploads);
      }
    }

    void IListener<ApplicationClosingMessage>.Handle(ApplicationClosingMessage message)
    {
      joinedRoomHolder.Rooms.ToList().ForEach(r => r.Room.Leave());
    }

    #endregion
  }
}
