
namespace GrayHills.FlameCage.Client.Messages
{
  public class OverlayMessage
  {
    public bool IsVisible { get; private set; }

    public OverlayMessage(bool visible)
    {
      this.IsVisible = visible;
    }
  }
}
