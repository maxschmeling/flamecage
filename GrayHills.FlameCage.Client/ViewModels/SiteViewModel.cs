namespace GrayHills.FlameCage.Client.ViewModels
{
  public class SiteViewModel
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }

    public SiteViewModel(Models.Site site)
    {
      ID = site.ID;
      Name = site.Name;
      Username = site.Username;
    }
  }
}
