
namespace GrayHills.FlameCage.Client.Core
{
  public interface IListener<T>
  {
    void Handle(T message);
  }
}
