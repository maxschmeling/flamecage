using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
    public class TextMessageViewModel : MessageViewModel
    {
        public string Text { get; set; }

        public TextMessageViewModel(Message message)
            : base(message)
        {
            this.Text = message.Body;
        }
    }
}
