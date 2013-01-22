using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.Core
{
  public class RoomLeftMessage
  {
    public IRoom Room { get; private set; }

    public RoomLeftMessage(IRoom room)
    {
      this.Room = room;
    }
  }
}
