using System.Windows;

namespace GrayHills.FlameCage.Client
{
  public partial class MetroMessageBox
  {
    public MetroMessageBox()
    {
      InitializeComponent();
    }

    private void OkButton_OnClick(object sender, System.Windows.RoutedEventArgs e)
    {
      DialogResult = true;

      Close();
    }

    public void ShowMessage(string title, string message)
    {
      Owner = Application.Current.MainWindow;

      Title = title;
      MessageTextBlock.Text = message;

      ShowDialog();
    }
  }
}
