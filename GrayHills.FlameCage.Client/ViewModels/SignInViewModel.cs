using System.Net;
using System.Windows.Input;
using GrayHills.FlameCage.Client.Core;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class SignInViewModel : ViewModelBase
  {
    private string siteName, username, password;

    public string SiteName
    {
      get { return siteName; }
      set
      {
        if (siteName != value)
        {
          siteName = value;

          this.NotifyPropertyChanged(vm => vm.SiteName);
        }
      }
    }

    public string Username
    {
      get { return username; }
      set
      {
        if (username != value)
        {
          username = value;

          this.NotifyPropertyChanged(vm => vm.Username);
        }
      }
    }

    public string Password
    {
      get { return password; }
      set
      {
        if (password != value)
        {
          password = value;

          this.NotifyPropertyChanged(vm => vm.Password);
        }
      }
    }

    public ICommand SignInCommand { get; private set; }

    public SignInViewModel() : this(0) { }
    public SignInViewModel(int siteID)
    {
      if (siteID != 0)
      {
        //var db = new OpenFlameDbContext();
        GrayHills.FlameCage.Client.Models.Site site = null;// db.Sites.Single(s => s.ID == siteID);

        this.SiteName = site.Name;
        this.Username = site.Username;
      }

      this.SignInCommand = new RelayCommand(x => SignIn());
    }

    private void SignIn()
    {
      var site = new GrayHills.Matches.Site
      {
        Name = SiteName
      };

      var token = site.GetToken(new NetworkCredential(Username, Password));

      //var db = new OpenFlameDbContext();

      GrayHills.FlameCage.Client.Models.Site siteModel = null; //= db.Sites.SingleOrDefault(s => s.Name == SiteName);

      if (siteModel == null)
      {
        siteModel = new Models.Site { Name = SiteName, ApiAuthToken = token };
        //db.Sites.Add(siteModel);
      }
      else
      {
        siteModel.ApiAuthToken = token;
      }

      //db.SaveChanges();
    }
  }
}
