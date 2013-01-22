using GrayHills.FlameCage.Client.Core;
using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public abstract class MessageViewModel : ViewModelBase
  {
    public Message Message { get; private set; }

    public UserViewModel User { get; set; }
    public int ID { get; set; }
    public MessageType Type { get { return Message.Type; } }

    protected MessageViewModel(Message message)
    {
      this.Message = message;

      if (message.User != null)
        this.User = new UserViewModel(message.User);

      this.ID = message.ID;
    }
  }
}
