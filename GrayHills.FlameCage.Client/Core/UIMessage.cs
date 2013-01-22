
namespace GrayHills.FlameCage.Client.Core
{
  public class UIMessage : IMessage
  {
    public void Show(string title, string message)
    {
      var metroMessageBox = new MetroMessageBox();
      metroMessageBox.ShowMessage(title, message);
    }
  }
}
