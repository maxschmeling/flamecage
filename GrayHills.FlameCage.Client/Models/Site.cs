namespace GrayHills.FlameCage.Client.Models
{
  public class Site
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string ApiAuthToken { get; set; }
    public System.Windows.Media.Color Color { get; set; }
    public string ColorString
    {
      get { return Color.ToString(); }
      set { Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(value); }
    }
  }
}
