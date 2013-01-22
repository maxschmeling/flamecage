using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Messages;
using GrayHills.FlameCage.Client.ViewModels;
using GrayHills.FlameCage.Client.Views.Interfaces;
using StructureMap;
using SWS = System.Windows.Shapes;

namespace GrayHills.FlameCage.Client
{
  public partial class MainWindow : IMainView,
    IListener<PromptForNewSiteMessage>,
    IListener<OverlayMessage>,
    IListener<DisplaySiteListMessage>,
    IListener<ViewTranscriptMessage>,
    IListener<GoToStateMessage>
  {
    private readonly IEventAggregator eventAggregator;
    private readonly Dispatcher dispatcher;
    private readonly IRoomListView roomListView;
    private readonly IRoomView roomView;
    private readonly ISettingsView settingsView;

    public MainWindow(MainViewModel viewModel, IRoomListView roomListView, IRoomView roomView,
      IEventAggregator eventAggregator, ISettingsView settingsView)
    {
      InitializeComponent();

      DataContext = viewModel;

      this.eventAggregator = eventAggregator;
      this.roomListView = roomListView;
      this.roomView = roomView;
      this.settingsView = settingsView;

      dispatcher = Dispatcher.CurrentDispatcher;

      NotifyIcon.TrayMouseDoubleClick += NotifyIcon_OnTrayMouseDoubleClick;
    }

    private void NotifyIcon_OnTrayMouseDoubleClick(object sender, RoutedEventArgs e)
    {
      ((MainViewModel) DataContext).CurrentWindowState = WindowState.Normal;
      Activate();

      IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;

      FlashWindow.Flash(hwnd, 2);
    }

    private void LoadNormalView()
    {
      LeftDockPanel.Children.Add((UIElement)roomListView);
      DockPanel.SetDock((UIElement)roomListView, Dock.Top);
      RightDockPanel.Children.Add((UIElement)roomView);
      DockPanel.SetDock((UIElement)roomView, Dock.Top);
      SettingsPanel.Children.Add((UIElement) settingsView);
    }

    protected override void OnInitialized(EventArgs e)
    {
      base.OnInitialized(e);

      SetWindow(this.WindowState);
    }

    protected override void OnContentRendered(EventArgs e)
    {
      base.OnContentRendered(e);

      LoadNormalView();
    }

    private void HeaderArea_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      DragMove();

      if (e.ClickCount >= 2)
      {
        switch (WindowState)
        {
          case WindowState.Maximized:
            WindowState = WindowState.Normal;
            break;
          case WindowState.Normal:
            WindowState = WindowState.Maximized;
            break;
        }
      }
    }

    protected void SizingRect_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (base.WindowState != WindowState.Maximized)
      {
        if ((e.ClickCount >= 2) && (((sender as SWS.Rectangle) == this.TopResizeRectangle) || ((sender as SWS.Rectangle) == this.BottomResizeRectangle)))
        {
          //this.previousHeight = base.Height;
          //base.Top = SettingsData.Instance.CurrentScreen().WorkingArea.Top;
          //base.Height = SettingsData.Instance.CurrentScreen().WorkingArea.Height;
        }
        else
        {
          //SettingsData.Instance.WindowResizing = true;
          if ((sender as SWS.Rectangle) == this.LeftResizeRectangle)
          {
            this.ResizeWindow(ResizeDirection.Left);
          }
          else if ((sender as SWS.Rectangle) == this.TopLeftResizeRectangle)
          {
            this.ResizeWindow(ResizeDirection.TopLeft);
          }
          else if ((sender as SWS.Rectangle) == this.TopResizeRectangle)
          {
            this.ResizeWindow(ResizeDirection.Top);
          }
          else if ((sender as SWS.Rectangle) == this.TopRightResizeRectangle)
          {
            this.ResizeWindow(ResizeDirection.TopRight);
          }
          else if ((sender as SWS.Rectangle) == this.RightResizeRectangle)
          {
            this.ResizeWindow(ResizeDirection.Right);
          }
          else if ((sender as SWS.Rectangle) == this.BottomRightResizeRectangle)
          {
            this.ResizeWindow(ResizeDirection.BottomRight);
          }
          else if ((sender as SWS.Rectangle) == this.BottomResizeRectangle)
          {
            this.ResizeWindow(ResizeDirection.Bottom);
          }
          else if ((sender as SWS.Rectangle) == this.BottomLeftResizeRectangle)
          {
            this.ResizeWindow(ResizeDirection.BottomLeft);
          }
          //SettingsData.Instance.WindowResizing = false;
        }
      }
    }

    private void SetWindow(WindowState state)
    {
      switch (state)
      {
        case WindowState.Normal:
          MaximizeButton.Visibility = Visibility.Visible;
          RestoreButton.Visibility = Visibility.Collapsed;
          SetResizeRectangleVisibility(Visibility.Visible);
          break;
        case WindowState.Maximized:
          MaximizeButton.Visibility = Visibility.Collapsed;
          RestoreButton.Visibility = Visibility.Visible;
          SetResizeRectangleVisibility(Visibility.Hidden);
          break;
      }
    }

    private void SetResizeRectangleVisibility(Visibility visibility)
    {
      FormResizeGrip.Visibility = visibility;
      TopResizeRectangle.Visibility = visibility;
      LeftResizeRectangle.Visibility = visibility;
      BottomResizeRectangle.Visibility = visibility;
      RightResizeRectangle.Visibility = visibility;
      TopLeftResizeRectangle.Visibility = visibility;
      TopRightResizeRectangle.Visibility = visibility;
      BottomLeftResizeRectangle.Visibility = visibility;
      BottomRightResizeRectangle.Visibility = visibility;
    }

    private void ShadowedWindow_OnStateChanged(object sender, EventArgs e)
    {
      SetWindow(WindowState);
    }

    #region Message Handler Methods

    void IListener<PromptForNewSiteMessage>.Handle(PromptForNewSiteMessage message)
    {
      this.eventAggregator.SendMessage(new OverlayMessage(true));

      this.dispatcher.Invoke(() => ObjectFactory.GetInstance<IAddSiteView>().Show());
    }

    void IListener<OverlayMessage>.Handle(OverlayMessage message)
    {
      this.dispatcher.Invoke(() => this.OverlayCanvas.Visibility =
        (message.IsVisible ? Visibility.Visible : Visibility.Collapsed));
    }

    void IListener<DisplaySiteListMessage>.Handle(DisplaySiteListMessage message)
    {
      this.eventAggregator.SendMessage(new OverlayMessage(true));

      this.dispatcher.Invoke(() => ObjectFactory.GetInstance<ISiteListView>().Show());
    }

    void IListener<ViewTranscriptMessage>.Handle(ViewTranscriptMessage message)
    {
      eventAggregator.SendMessage(new OverlayMessage(true));

      dispatcher.Invoke(() =>
      {
        var viewModel = new TranscriptViewModel(message.Room, message.Site,
          message.Search, DateTime.MinValue, new MessageViewModelFactory(), eventAggregator) 
            { SearchTerm = message.Search };

        ObjectFactory.With(viewModel).GetInstance<ITranscriptView>().Show();
      });
    }

    #endregion

    public void Handle(GoToStateMessage message)
    {
      dispatcher.Invoke(() => VisualStateManager.GoToElementState(LayoutRoot, message.State, true));
    }
  }
}
