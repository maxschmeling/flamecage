using System.Collections.ObjectModel;
using GrayHills.FlameCage.Client.Core;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class MessageGroupViewModel : ViewModelBase
  {
    public UserViewModel User { get; set; }
    public ObservableCollection<MessageViewModel> Messages { get; set; }

    public MessageGroupViewModel(UserViewModel userViewModel)
    {
      User = userViewModel;
      this.Messages = new ObservableCollection<MessageViewModel>();
    }
  }
}
