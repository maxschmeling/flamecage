using System.Windows.Controls;
using GrayHills.FlameCage.Client.ViewModels;
using GrayHills.FlameCage.Client.Views.Interfaces;

namespace GrayHills.FlameCage.Client.Views
{
  public partial class RoomListView : UserControl, IRoomListView
  {
    public RoomListView()
    {
      InitializeComponent();
    }

    public RoomListView(RoomListViewModel vm)
    {
      InitializeComponent();
      this.DataContext = vm;
    }
  }
}
