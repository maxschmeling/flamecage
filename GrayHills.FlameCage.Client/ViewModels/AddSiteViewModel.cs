using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Messages;
using GrayHills.FlameCage.Client.Models;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class AddSiteViewModel : ViewModelBase, IListener<PromptForNewSiteMessage>
  {
    private readonly IRepository repository;
    private readonly IEventAggregator eventAggregator;
    private readonly IMessage messager;

    private string siteName, username, password;
    private Visibility dialogVisibility = Visibility.Hidden;

    public string SiteName
    {
      get { return siteName; }
      set
      {
        if (siteName == value) return;

        siteName = value;
        this.NotifyPropertyChanged(vm => vm.SiteName);
      }
    }

    public string Username
    {
      get { return username; }
      set
      {
        if (username == value) return;

        username = value;
        this.NotifyPropertyChanged(vm => vm.Username);
      }
    }

    public string Password
    {
      get { return password; }
      set
      {
        if (password == value) return;

        password = value;
        this.NotifyPropertyChanged(vm => vm.Password);
      }
    }

    public Visibility DialogVisibility
    {
      get { return dialogVisibility; }
      set
      {
        if (dialogVisibility == value) return;

        dialogVisibility = value;
        this.NotifyPropertyChanged(vm => vm.DialogVisibility);
      }
    }

    public ICommand AddSiteCommand { get; private set; }
    public ICommand CancelCommand { get; private set; }

    public AddSiteViewModel(IRepository repository, IEventAggregator eventAggregator, IMessage messager)
    {
      this.repository = repository;
      this.eventAggregator = eventAggregator;
      this.messager = messager;

      DialogVisibility = Visibility.Visible;

      AddSiteCommand = new RelayCommand(x => AddSite(), y => !string.IsNullOrEmpty(this.SiteName) &&
                                                             !string.IsNullOrEmpty(this.Username) &&
                                                             !string.IsNullOrEmpty(this.Password));
      CancelCommand = new RelayCommand(Cancel);

      var wrapper = new UrlHistoryWrapperClass();
      var enumerator = wrapper.GetEnumerator();

      var sites = repository.All<Site>().ToList();

      while (enumerator.MoveNext())
      {
        if (enumerator.Current.URL.Contains("campfirenow.com"))
        {
          if (!sites.Any(s => enumerator.Current.URL.Contains(s.Name)))
          {
            SiteName = enumerator.Current.URL.Substring(8, enumerator.Current.URL.IndexOf('.') - 8);

            if (sites.Any() && (sites.GroupBy(x => x.Username).Count() == 1))
            {
              Username = sites.First().Username;
            }

            break;
          }
        }
      }
    }

    private void AddSite()
    {
      var apiToken = string.Empty;

      var s = new GrayHills.Matches.Site { Name = SiteName, Credentials = new NetworkCredential(Username, Password) };

      try
      {
        apiToken = s.GetMe().ApiAuthToken;
      }
      catch (WebException ex)
      {
        if (ex.Status == WebExceptionStatus.ProtocolError)
        {
          messager.Show("Error", string.Format("Error logging into Campfire: {0}", ((HttpWebResponse)ex.Response).StatusDescription));
        }
      }

      if (!string.IsNullOrEmpty(apiToken))
      {
        var rand = new Random(DateTime.Now.Second);
        DialogVisibility = Visibility.Hidden;
        var siteModel = new Site
        {
          Name = this.SiteName,
          Username = this.Username,
          ApiAuthToken = apiToken,
          Color = Color.FromRgb((byte)rand.Next(25, 225), (byte)rand.Next(25, 225), (byte)rand.Next(25, 225))
        };
        repository.Add(siteModel);
        repository.Save();

        s.ApiToken = siteModel.ApiAuthToken;
        s.Credentials = new NetworkCredential(s.ApiToken, "X");

        ClearFields();

        eventAggregator.SendMessage(new SiteAddedMessage { SiteApi = s, SiteModel = siteModel });
      }
    }

    private void Cancel()
    {
      DialogVisibility = Visibility.Hidden;
      ClearFields();
    }

    private void ClearFields()
    {
      siteName = string.Empty;
      Username = string.Empty;
      Password = string.Empty;
    }

    public void Handle(PromptForNewSiteMessage message)
    {
      DialogVisibility = Visibility.Visible;
    }
  }
}
