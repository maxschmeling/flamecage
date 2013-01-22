using System.Windows.Controls;
namespace GrayHills.FlameCage.Client.Controls
{
  public partial class MessageList : UserControl
  {
    public MessageList()
    {
      InitializeComponent();

      this.MessagesListView.ItemContainerGenerator.ItemsChanged += (s, ea) => ScrollToEnd();
    }

    private void ScrollToEnd()
    {
      if (this.MessagesListView.Items.Count == 0) return;

      try
      {
        this.MessagesScrollView.ScrollToBottom();
      }
      catch
      {
        // intentionally left blank -- random error in try block occasionally.
      }
    }
  }
}
