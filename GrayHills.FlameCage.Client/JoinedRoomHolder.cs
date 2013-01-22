using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Messages;
using GrayHills.Matches;

namespace GrayHills.FlameCage.Client
{
  public class JoinedRoomHolder
  {
    private readonly IEventAggregator eventAggregator;
    private readonly List<IActiveRoom> rooms;

    public ReadOnlyCollection<IActiveRoom> Rooms { get; private set; }
    public IActiveRoom CurrentRoom { get; set; }

    public JoinedRoomHolder(IEventAggregator eventAggregator)
    {
      this.eventAggregator = eventAggregator;
      this.rooms = new List<IActiveRoom>();

      this.Rooms = new ReadOnlyCollection<IActiveRoom>(this.rooms);
    }

    public IActiveRoom AddRoom(IRoom room)
    {
      lock (this)
      {
        if (!this.rooms.Any(r => r.Room.ID == room.ID))
        {
          var newActiveRoom = new ActiveRoom(room);
          newActiveRoom.MessageReceived += (sender, e) => 
            this.eventAggregator.SendMessage(new MessageReceivedMessage(e.Message));

          this.rooms.Add(newActiveRoom);
        }
      }

      return this.rooms.Single(r => r.Room.ID == room.ID);
    }

    public void RemoveRoom(int roomID)
    {
      this.rooms.Remove(this.rooms.Single(r => r.Room.ID == roomID));
    }
  }
}
