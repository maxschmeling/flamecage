
namespace GrayHills.FlameCage.Client.Messages
{
  public class SiteDeletedMessage
  {
    public Models.Site Site { get; private set; }

    public SiteDeletedMessage(GrayHills.FlameCage.Client.Models.Site site)
    {
      this.Site = site;
    }
  }
}
