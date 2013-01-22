using System.Windows.Threading;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Models;
using GrayHills.FlameCage.Client.Views;
using GrayHills.FlameCage.Client.Views.Interfaces;
using StructureMap.Configuration.DSL;

namespace GrayHills.FlameCage.Client
{
  public class MainStructureMapRegistry : Registry
  {
    public MainStructureMapRegistry()
    {
      For<IEventAggregator>().Singleton().Use<EventAggregator>();
      
      For<MessageViewModelFactory>().Singleton();
      For<JoinedRoomHolder>().Singleton();
      For<IRepository>().Use<EfCfRepository>();
      For<Dispatcher>().Use(() => Dispatcher.CurrentDispatcher);
      For<IMessage>().Use<UIMessage>();

      // views
      For<IActivationView>().Use<ActivationView>();
      For<ISignInView>().Use<SignInView>();
      For<IRoomView>().Use<RoomView>();
      For<IRoomListView>().Use<RoomListView>();
      For<IAddSiteView>().Use<AddSiteView>();
      For<ISiteListView>().Use<SiteListView>();
      For<ITranscriptView>().Use<TranscriptView>();
      For<ISettingsView>().Use<SettingsView>();
      For<IMainView>().Use<MainWindow>();
      
      RegisterInterceptor(new EventAggregatorTypeInterceptor());
    }
  }
}
