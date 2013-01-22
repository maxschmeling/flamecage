using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
    public class UnknownMessageViewModel : MessageViewModel
    {
        new public string Message { get; set; }

        public UnknownMessageViewModel(Message message)
            : base(message)
        {
            //this.User = new UserViewModel(message.User);
          this.Message = string.Format("Unkown Message Type: {0} Contents: {1}", message.Type, message.Body);
        }
    }
}
