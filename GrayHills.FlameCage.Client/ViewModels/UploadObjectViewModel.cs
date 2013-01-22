using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Threading;
using GrayHills.FlameCage.Client.Core;
using GrayHills.FlameCage.Client.Interop;
using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class UploadObjectViewModel : ViewModelBase
  {
    private readonly UploadObject uploadObject;
    private int percentDownloaded;
    private string filename, fullUrl;
    private bool isDownloading;
    private BackgroundWorker downloadWorker;

    public int Id { get; set; }

    public string Filename
    {
      get { return filename; }
      set
      {
        if (filename == value) return;

        filename = value;

        this.NotifyPropertyChanged(vm => vm.Filename);
      }
    }

    public string FullUrl
    {
      get { return fullUrl; }
      set
      {
        if (fullUrl == value) return;

        fullUrl = value;

        this.NotifyPropertyChanged(vm => vm.FullUrl);
      }
    }

    public int PercentDownloaded
    {
      get { return percentDownloaded; }
      set
      {
        if (percentDownloaded == value) return;

        percentDownloaded = value;

        this.NotifyPropertyChanged(vm => vm.PercentDownloaded);
      }
    }

    public bool IsDownloading
    {
      get { return isDownloading; }
      set
      {
        if (isDownloading == value) return;

        isDownloading = value;

        this.NotifyPropertyChanged(vm => vm.IsDownloading);
      }
    }

    public string ContentType { get; set; }

    public RelayCommand DownloadCommand { get; set; }

    public UploadObjectViewModel(UploadObject uploadObject)
    {
      this.uploadObject = uploadObject;

      Filename = uploadObject.Name;
      Id = uploadObject.ID;
      ContentType = uploadObject.ContentType;

      DownloadCommand = new RelayCommand(Download);
    }

    private void Download()
    {
      PercentDownloaded = 0;
      IsDownloading = true;

      downloadWorker = new BackgroundWorker();
      downloadWorker.DoWork += downloadWorker_OnDoWork;
      downloadWorker.RunWorkerAsync(Dispatcher.CurrentDispatcher);
    }

    private void downloadWorker_OnDoWork(object sender, DoWorkEventArgs e)
    {
      var dispatcher = e.Argument as Dispatcher;

      const int bufferSize = 512;

      IntPtr pPath;
      var path = string.Empty;
      if (Win32.SHGetKnownFolderPath(KnownFolders.Downloads, 0, IntPtr.Zero, out pPath) == 0)
      {
        path = System.Runtime.InteropServices.Marshal.PtrToStringUni(pPath) ?? string.Empty;
        System.Runtime.InteropServices.Marshal.FreeCoTaskMem(pPath);
        // s now contains the path for the all-users "Public Desktop" folder
      }


      var file = Path.Combine(path, filename);

      using (var stream = this.uploadObject.GetDownloadStream())
      using (Stream outputStream = File.Create(file))
      {
        var totalBytesRead = 0;

        while (true)
        {
          var readBytes = new byte[bufferSize];
          var bytesRead = stream.Read(readBytes, 0, bufferSize);

          totalBytesRead += bytesRead;

          var read = totalBytesRead;

          dispatcher.Invoke(() => PercentDownloaded = (read * 100) / uploadObject.ByteCount);

          if (bytesRead == 0)
            break;

          outputStream.Write(readBytes, 0, bytesRead);
        };
      }

      downloadWorker.DoWork -= downloadWorker_OnDoWork;
      IsDownloading = false;

      Process.Start(file);
    }
  }
}
