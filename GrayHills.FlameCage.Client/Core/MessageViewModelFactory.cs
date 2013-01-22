using System;
using System.Collections.Generic;
using GrayHills.FlameCage.Client.ViewModels;
using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.Core
{
  public class MessageViewModelFactory
  {
    private readonly Dictionary<MessageType, Func<Message, MessageViewModel>> builders;

    public MessageViewModelFactory()
    {
      builders = new Dictionary<MessageType, Func<Message, MessageViewModel>>
      {
        { MessageType.Advertisement, m => new AdvertisementMessageViewModel(m)},
        { MessageType.AllowGuests, m => new AllowGuestsMessageViewModel(m)},
        { MessageType.DisallowGuests, m => new DisallowGuestsMessageViewModel(m)},
        { MessageType.Enter, m => new EnterMessageViewModel(m)},
        { MessageType.Idle, m => new IdleMessageViewModel(m)},
        { MessageType.Kick, m => new KickMessageViewModel(m)},
        { MessageType.Leave, m => new LeaveMessageViewModel(m)},
        { MessageType.Lock, m => new LockMessageViewModel(m)},
        { MessageType.Paste, m => new PasteMessageViewModel(m)},
        { MessageType.Sound, m => new SoundMessageViewModel(m)},
        { MessageType.System, m => new SystemMessageViewModel(m)},
        { MessageType.Text, m => new TextMessageViewModel(m)},
        { MessageType.Timestamp, m => new TimestampMessageViewModel(m)},
        { MessageType.TopicChange, m => new TopicChangeMessageViewModel(m)},
        { MessageType.Unidle, m => new UnidleMessageViewModel(m)},
        { MessageType.Unlock, m => new UnlockMessageViewModel(m)},
        { MessageType.Upload, m => new UploadMessageViewModel(m)}
      };
    }

    public MessageViewModel BuildFor(Message message)
    {
      if (builders.ContainsKey(message.Type))
      {
        return builders[message.Type](message);
      }

      return new UnknownMessageViewModel(message);
    }
  }
}
