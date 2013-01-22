using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.Messages
{
  public class MessageReceivedMessage
  {
    public Message Message { get; private set; }

    public MessageReceivedMessage(Message message)
    {
      this.Message = message;
    }
  }
}
