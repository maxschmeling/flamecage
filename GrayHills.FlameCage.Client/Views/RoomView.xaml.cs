using System.Windows;
using GrayHills.FlameCage.Client.ViewModels;
using GrayHills.FlameCage.Client.Views.Interfaces;

namespace GrayHills.FlameCage.Client.Views
{
  public partial class RoomView : IRoomView
  {
    public RoomView()
    {
      InitializeComponent();
    }

    public RoomView(ActiveRoomViewModel viewModel)
    {
      InitializeComponent();

      DataContext = viewModel;
    }

    private void Border_Drop(object sender, DragEventArgs e)
    {
      var data = e.Data as DataObject;

      if (data == null) return;

      foreach (var filePath in data.GetFileDropList())
      {
        var viewModel = DataContext as ActiveRoomViewModel;

        if (viewModel != null) 
          viewModel.UploadDroppedFileCommand.Execute(filePath);
      }
    }
  }
}