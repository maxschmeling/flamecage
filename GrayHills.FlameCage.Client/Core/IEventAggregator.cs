
namespace GrayHills.FlameCage.Client.Core
{
  public interface IEventAggregator
  {
    void SendMessage<T>(T message);
    void AddListener(object listener);
    void RemoveListener(object listener);
  }
}
