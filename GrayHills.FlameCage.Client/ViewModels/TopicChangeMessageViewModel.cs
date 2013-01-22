using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class TopicChangeMessageViewModel : MessageViewModel
  {
    public string NewTopic
    {
      get { return Message.Body; }
    }

    public TopicChangeMessageViewModel(Message message)
      : base(message)
    {
    }
  }
}
