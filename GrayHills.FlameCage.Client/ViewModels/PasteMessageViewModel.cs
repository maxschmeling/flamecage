using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
    public class PasteMessageViewModel : MessageViewModel
    {
        public string Text { get { return base.Message.Body; } }

        public PasteMessageViewModel(Message message)
            : base(message)
        {

        }
    }
}
