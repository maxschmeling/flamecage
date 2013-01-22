using System;
using System.Windows;
using System.Windows.Shell;
using GrayHills.FlameCage.Client.Views;
using StructureMap;

namespace GrayHills.FlameCage.Client
{
  public class Startup : Application
  {
    [STAThread]
    public static void Main()
    {
      var startup = new Startup();

      startup.AddMergedResourceDictionary("Resources/General.xaml");
      startup.AddMergedResourceDictionary("Resources/Dictionary1.xaml");
      startup.AddMergedResourceDictionary("Resources/Dictionary2.xaml");
      startup.AddMergedResourceDictionary("Resources/Geometry.xaml");
      startup.AddMergedResourceDictionary("Resources/Buttons.xaml");

      ObjectFactory.Initialize(x =>
      {
        x.UseDefaultStructureMapConfigFile = false;
        x.AddRegistry(new MainStructureMapRegistry());
      });

      Window startupWindow;

      if (DateTime.Now > new DateTime(2014, 1, 1))
        startupWindow = new ExpiredView();
      else
        startupWindow = ObjectFactory.GetInstance<MainWindow>();

      Current.MainWindow = startupWindow;

      startup.DispatcherUnhandledException += startup_OnDispatcherUnhandledException;

      startup.Run(startupWindow);
    }

    static void startup_OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      var mbox = new MetroMessageBox();
      mbox.ShowMessage("Unhandled Error", e.Exception.ToString());
    }

    public Startup()
    {
      var jumpList = new JumpList();
      JumpList.SetJumpList(Current, jumpList);
      
      // todo: configure jump list

      //JumpTask t;

      //jumpList.JumpItems.Add(new JumpTask()
      //  {
      //    Title = "netCDS Wing",
      //    CustomCategory = "Recent Rooms"
      //  });
      //jumpList.JumpItems.Add(new JumpTask()
      //{
      //  Title = "netCDS Wing",
      //  CustomCategory = "Recent Rooms"
      //});

      jumpList.Apply();
    }

    /// <summary>
    /// This method adds a merged resource dictionary to the application. 
    /// It is needed because we aren't using the standard App.xaml file.
    /// </summary>
    /// <param name="uri">A relative Uri to the resource dictionary file.</param>
    private void AddMergedResourceDictionary(string uri)
    {
      this.Resources.MergedDictionaries.Add(Application.LoadComponent(
        new Uri(uri, UriKind.Relative)) as ResourceDictionary);
    }
  }
}
