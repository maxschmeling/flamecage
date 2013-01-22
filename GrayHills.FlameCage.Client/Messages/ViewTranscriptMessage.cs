using GrayHills.Matches;
namespace GrayHills.FlameCage.Client.Messages
{
  public class ViewTranscriptMessage
  {
    public ISite Site { get; private set; }
    public IRoom Room { get; private set; }
    public string Search { get; private set; }

    public ViewTranscriptMessage()
    {
      this.Search = string.Empty;
    }

    public ViewTranscriptMessage(ISite site)
    {
      this.Site = site;
      this.Search = string.Empty;
    }

    public ViewTranscriptMessage(ISite site, string search)
    {
      this.Site = site;
      this.Search = search;
    }

    public ViewTranscriptMessage(ISite site, IRoom room, string search)
    {
      this.Site = site;
      this.Room = room;
      this.Search = search;
    }
  }
}
