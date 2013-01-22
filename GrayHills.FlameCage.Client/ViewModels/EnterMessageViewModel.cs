using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
    public class EnterMessageViewModel : MessageViewModel
    {
        public EnterMessageViewModel(Message message)
            : base(message)
        {
            User = new UserViewModel(message.User);
        }
    }
}
