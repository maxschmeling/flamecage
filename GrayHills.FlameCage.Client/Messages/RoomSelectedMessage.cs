using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.Messages
{
  public class RoomSelectedMessage
  {
    public IRoom Room { get; set; }
    
    public RoomSelectedMessage(IRoom room)
    {
      Room = room;
    }
  }
}
