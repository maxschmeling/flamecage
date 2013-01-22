using System.Collections.ObjectModel;
using System.Windows.Input;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Messages;
using GrayHills.FlameCage.Client.Models;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class SiteListViewModel : ViewModelBase
  {
    private readonly IRepository repository;
    private readonly IEventAggregator eventAggregator;

    private SiteViewModel selectedSite;

    public SiteViewModel SelectedSite
    {
      get { return selectedSite; }
      set
      {
        if (selectedSite == value) return;

        selectedSite = value;
        this.NotifyPropertyChanged(vm => vm.SelectedSite);
      }
    }
    public ObservableCollection<SiteViewModel> Sites { get; private set; }

    public ICommand DeleteSiteCommand { get; private set; }

    public bool CanDeleteSelectedSite
    {
      get { return SelectedSite != null; }
    }

    public SiteListViewModel(IRepository repository, IEventAggregator eventAggregator)
    {
      this.repository = repository;
      this.eventAggregator = eventAggregator;

      this.Sites = new ObservableCollection<SiteViewModel>();

      foreach (var site in this.repository.All<Site>())
      {
        this.Sites.Add(new SiteViewModel(site));
      }

      this.DeleteSiteCommand = new RelayCommand(x => DeleteSelectedSite(), x => CanDeleteSelectedSite);
    }

    private void DeleteSelectedSite()
    {
      var siteToDelete = this.repository.SingleOrDefault<Site>(s => s.ID == SelectedSite.ID);

      this.eventAggregator.SendMessage(new SiteDeletedMessage(siteToDelete));

      this.Sites.Remove(SelectedSite);

      this.repository.Delete(siteToDelete);
      this.repository.Save();
    }
  }
}
