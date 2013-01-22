using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class UploadMessageViewModel : MessageViewModel
  {
    private readonly UploadObject uploadObject;
    private ImageSource content;

    public string ContentType
    {
      get { return uploadObject.ContentType; }
    }

    public ImageSource Content
    {
      get
      {
        var bi = new BitmapImage();
        bi.BeginInit();
        bi.StreamSource = uploadObject.GetDownloadStream();
        bi.EndInit();

        return bi;

        return new BitmapImage(new Uri(uploadObject.FullUrl));
        return new BitmapImage(new Uri("http://www.google.com/images/logos/ps_logo2.png"));

        if (content == null)
        {
          var decoder = new JpegBitmapDecoder(uploadObject.GetDownloadStream(),
                                              BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);

          content = decoder.Preview;

          var task = new Task(() =>
                                {
                                  Thread.Sleep(10000);
                                  OnPropertyChanged(new PropertyChangedEventArgs("Content"));;
                                });
          task.Start();
        }

        return content;
      }
    }

    public UploadMessageViewModel(Message message)
      : base(message)
    {
      uploadObject = message.Room.GetUploadObject(message.ID);
    }
  }
}
