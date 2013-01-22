using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class TimestampMessageViewModel : MessageViewModel
  {
    public string Value
    {
      get
      {
        return base.Message.CreatedAt.ToShortTimeString();
      }
    }

    public TimestampMessageViewModel(Message message)
      : base(message)
    {

    }
  }
}
