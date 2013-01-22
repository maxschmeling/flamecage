using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GrayHills.FlameCage.Client.ViewModels;

namespace GrayHills.FlameCage.Client.Core
{
  public class MessageViewModelTemplateSelector : DataTemplateSelector
  {
    public DataTemplate AdvertisementMessageDataTemplate { get; set; }
    public DataTemplate EnterMessageDataTemplate { get; set; }
    public DataTemplate KickMessageDataTemplate { get; set; }
    public DataTemplate PasteMessageDataTemplate { get; set; }
    public DataTemplate TextMessageDataTemplate { get; set; }
    public DataTemplate TimestampMessageDataTemplate { get; set; }
    public DataTemplate UnknownMessageDataTemplate { get; set; }
    public DataTemplate LeaveMessageDataTemplate { get; set; }
    public DataTemplate MyMessageGroupDataTemplate { get; set; }
    public DataTemplate OtherMessageGroupDataTemplate { get; set; }
    public DataTemplate ImageUploadDataTemplate { get; set; }
    public DataTemplate TopicChangedDataTemplate { get; set; }
    public DataTemplate LockedDataTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      if (item is AdvertisementMessageViewModel)
        return AdvertisementMessageDataTemplate;
      if (item is AllowGuestsMessageViewModel)
        return UnknownMessageDataTemplate;
      if (item is DisallowGuestsMessageViewModel)
        return UnknownMessageDataTemplate;
      if (item is EnterMessageViewModel)
        return EnterMessageDataTemplate;
      if (item is IdleMessageViewModel)
        return UnknownMessageDataTemplate;
      if (item is KickMessageViewModel)
        return KickMessageDataTemplate;
      if (item is LeaveMessageViewModel)
        return LeaveMessageDataTemplate;
      if (item is LockMessageViewModel)
        return LockedDataTemplate;
      if (item is PasteMessageViewModel)
        return PasteMessageDataTemplate;
      if (item is SoundMessageViewModel)
        return UnknownMessageDataTemplate;
      if (item is SystemMessageViewModel)
        return UnknownMessageDataTemplate;
      if (item is TextMessageViewModel)
        return TextMessageDataTemplate;
      if (item is TimestampMessageViewModel)
        return TimestampMessageDataTemplate;
      if (item is TopicChangeMessageViewModel)
        return TopicChangedDataTemplate;
      if (item is UnidleMessageViewModel)
        return UnknownMessageDataTemplate;
      if (item is UnlockMessageViewModel)
        return UnknownMessageDataTemplate;
      if (item is UploadMessageViewModel)
      {
        switch ((item as UploadMessageViewModel).ContentType)
        {
          case "image/jpg":
          case "image/jpeg":
          case "image/png":
          case "image/x-png":
          case "image/gif":
            return ImageUploadDataTemplate;
        }

        return UnknownMessageDataTemplate;
      }
      if (item is UnknownMessageViewModel)
        return UnknownMessageDataTemplate;
      if (item is MessageGroupViewModel)
      {
        var vm = item as MessageGroupViewModel;
        var firstMsg = ((MessageViewModel)vm.Messages.First()).Message;

        if (vm.User.Name == firstMsg.CampfireSite.Username)
          return MyMessageGroupDataTemplate;

        return OtherMessageGroupDataTemplate;
      }

      return base.SelectTemplate(item, container);
    }
  }
}