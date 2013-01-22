using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class AdvertisementMessageViewModel : MessageViewModel
  {
    public string Content
    {
      get { return base.Message.Body; }
    }

    public AdvertisementMessageViewModel(Message message)
      : base(message)
    {

    }
  }
}
