using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace GrayHills.FlameCage.Client.Core
{
  /// <summary>
  /// Some Win32Api Pinvoke.
  /// </summary>
  public class Win32api
  {
    /// <summary>
    /// Used by CannonializeURL method.
    /// </summary>
    [Flags]
    public enum shlwapi_URL : uint
    {
      /// <summary>
      /// Treat "/./" and "/../" in a URL string as literal characters, not as shorthand for navigation. 
      /// </summary>
      URL_DONT_SIMPLIFY = 0x08000000,
      /// <summary>
      /// Convert any occurrence of "%" to its escape sequence.
      /// </summary>
      URL_ESCAPE_PERCENT = 0x00001000,
      /// <summary>
      /// Replace only spaces with escape sequences. This flag takes precedence over URL_ESCAPE_UNSAFE, but does not apply to opaque URLs.
      /// </summary>
      URL_ESCAPE_SPACES_ONLY = 0x04000000,
      /// <summary>
      /// Replace unsafe characters with their escape sequences. Unsafe characters are those characters that may be altered during transport across the Internet, and include the (<, >, ", #, {, }, |, \, ^, ~, [, ], and ') characters. This flag applies to all URLs, including opaque URLs.
      /// </summary>
      URL_ESCAPE_UNSAFE = 0x20000000,
      /// <summary>
      /// Combine URLs with client-defined pluggable protocols, according to the World Wide Web Consortium (W3C) specification. This flag does not apply to standard protocols such as ftp, http, gopher, and so on. If this flag is set, UrlCombine does not simplify URLs, so there is no need to also set URL_DONT_SIMPLIFY.
      /// </summary>
      URL_PLUGGABLE_PROTOCOL = 0x40000000,
      /// <summary>
      /// Un-escape any escape sequences that the URLs contain, with two exceptions. The escape sequences for "?" and "#" are not un-escaped. If one of the URL_ESCAPE_XXX flags is also set, the two URLs are first un-escaped, then combined, then escaped.
      /// </summary>
      URL_UNESCAPE = 0x10000000
    }

    [DllImport("shlwapi.dll")]
    public static extern int UrlCanonicalize(
      string pszUrl,
      StringBuilder pszCanonicalized,
      ref int pcchCanonicalized,
      shlwapi_URL dwFlags
      );


    /// <summary>
    /// Takes a URL string and converts it into canonical form
    /// </summary>
    /// <param name="pszUrl">URL string</param>
    /// <param name="dwFlags">shlwapi_URL Enumeration. Flags that specify how the URL is converted to canonical form.</param>
    /// <returns>The converted URL</returns>
    public static string CannonializeURL(string pszUrl, shlwapi_URL dwFlags)
    {
      var buff = new StringBuilder(260);
      var s = buff.Capacity;
      var c = UrlCanonicalize(pszUrl, buff, ref s, dwFlags);
      if (c == 0)
        return buff.ToString();
      else
      {
        buff.Capacity = s;
        c = UrlCanonicalize(pszUrl, buff, ref s, dwFlags);
        return buff.ToString();
      }
    }


    public struct SYSTEMTIME
    {
      public Int16 Year;
      public Int16 Month;
      public Int16 DayOfWeek;
      public Int16 Day;
      public Int16 Hour;
      public Int16 Minute;
      public Int16 Second;
      public Int16 Milliseconds;
    }

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    static extern bool FileTimeToSystemTime
      (ref FILETIME FileTime, ref SYSTEMTIME SystemTime);


    /// <summary>
    /// Converts a file time to DateTime format.
    /// </summary>
    /// <param name="filetime">FILETIME structure</param>
    /// <returns>DateTime structure</returns>
    public static DateTime FileTimeToDateTime(FILETIME filetime)
    {
      var st = new SYSTEMTIME();
      FileTimeToSystemTime(ref filetime, ref st);
      return new DateTime(st.Year, st.Month, st.Day, st.Hour, st.Minute, st.Second, st.Milliseconds);

    }

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    static extern bool SystemTimeToFileTime([In] ref SYSTEMTIME lpSystemTime,
      out FILETIME lpFileTime);


    /// <summary>
    /// Converts a DateTime to file time format.
    /// </summary>
    /// <param name="datetime">DateTime structure</param>
    /// <returns>FILETIME structure</returns>
    public static FILETIME DateTimeToFileTime(DateTime datetime)
    {
      var st = new SYSTEMTIME
                 {
                   Year = (short) datetime.Year,
                   Month = (short) datetime.Month,
                   Day = (short) datetime.Day,
                   Hour = (short) datetime.Hour,
                   Minute = (short) datetime.Minute,
                   Second = (short) datetime.Second,
                   Milliseconds = (short) datetime.Millisecond
                 };
      FILETIME filetime;
      SystemTimeToFileTime(ref st, out filetime);
      return filetime;

    }
    //compares two file times.
    [DllImport("Kernel32.dll")]
    public static extern int CompareFileTime([In] ref FILETIME lpFileTime1, [In] ref FILETIME lpFileTime2);




    //Retrieves information about an object in the file system.
    [DllImport("shell32.dll")]
    public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

    public const uint SHGFI_ATTR_SPECIFIED =
      0x20000;
    public const uint SHGFI_ATTRIBUTES = 0x800;
    public const uint SHGFI_PIDL = 0x8;
    public const uint SHGFI_DISPLAYNAME =
      0x200;
    public const uint SHGFI_USEFILEATTRIBUTES
      = 0x10;
    public const uint FILE_ATTRIBUTRE_NORMAL =
      0x4000;
    public const uint SHGFI_EXETYPE = 0x2000;
    public const uint SHGFI_SYSICONINDEX =
      0x4000;
    public const uint ILC_COLORDDB = 0x1;
    public const uint ILC_MASK = 0x0;
    public const uint ILD_TRANSPARENT = 0x1;
    public const uint SHGFI_ICON = 0x100;
    public const uint SHGFI_LARGEICON = 0x0;
    public const uint SHGFI_SHELLICONSIZE =
      0x4;
    public const uint SHGFI_SMALLICON = 0x1;
    public const uint SHGFI_TYPENAME = 0x400;
    public const uint SHGFI_ICONLOCATION =
      0x1000;

    /// <summary>
    /// Contains information about a file object.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
      public IntPtr hIcon;
      public IntPtr iIcon;
      public uint dwAttributes;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
      public string szDisplayName;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
      public string szTypeName;
    };



    /// <summary>
    /// The helper class to sort in ascending order by FileTime(LastVisited).
    /// </summary>
    public class SortFileTimeAscendingHelper : IComparer
    {
      [DllImport("Kernel32.dll")]
      static extern int CompareFileTime([In] ref FILETIME lpFileTime1, [In] ref FILETIME lpFileTime2);


      int IComparer.Compare(object a, object b)
      {
        var c1 = (STATURL)a;
        var c2 = (STATURL)b;

        return (CompareFileTime(ref c1.ftLastVisited, ref c2.ftLastVisited));

      }

      public static IComparer SortFileTimeAscending()
      {
        return (IComparer)new SortFileTimeAscendingHelper();
      }

      /// <summary>
      /// Used by QueryUrl method
      /// </summary>
      public enum STATURL_QUERYFLAGS : uint
      {
        /// <summary>
        /// The specified URL is in the content cache.
        /// </summary>
        STATURL_QUERYFLAG_ISCACHED = 0x00010000,
        /// <summary>
        /// Space for the URL is not allocated when querying for STATURL.
        /// </summary>
        STATURL_QUERYFLAG_NOURL = 0x00020000,
        /// <summary>
        /// Space for the Web page's title is not allocated when querying for STATURL.
        /// </summary>
        STATURL_QUERYFLAG_NOTITLE = 0x00040000,
        /// <summary>
        /// //The item is a top-level item.
        /// </summary>
        STATURL_QUERYFLAG_TOPLEVEL = 0x00080000,

      }
      /// <summary>
      /// Flag on the dwFlags parameter of the STATURL structure, used by the SetFilter method.
      /// </summary>
      public enum STATURLFLAGS : uint
      {
        /// <summary>
        /// Flag on the dwFlags parameter of the STATURL structure indicating that the item is in the cache.
        /// </summary>
        STATURLFLAG_ISCACHED = 0x00000001,
        /// <summary>
        /// Flag on the dwFlags parameter of the STATURL structure indicating that the item is a top-level item.
        /// </summary>
        STATURLFLAG_ISTOPLEVEL = 0x00000002,
      }
      /// <summary>
      /// Used bu the AddHistoryEntry method.
      /// </summary>
      public enum ADDURL_FLAG : uint
      {
        /// <summary>
        /// Write to both the visited links and the dated containers. 
        /// </summary>
        ADDURL_ADDTOHISTORYANDCACHE = 0,
        /// <summary>
        /// Write to only the visited links container.
        /// </summary>
        ADDURL_ADDTOCACHE = 1
      }


      /// <summary>
      /// The structure that contains statistics about a URL. 
      /// </summary>
      [StructLayout(LayoutKind.Sequential)]
      public struct STATURL
      {
        /// <summary>
        /// Struct size
        /// </summary>
        public int cbSize;
        /// <summary>
        /// URL
        /// </summary>                                                                   
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwcsUrl;
        /// <summary>
        /// Page title
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwcsTitle;
        /// <summary>
        /// Last visited date (UTC)
        /// </summary>
        public FILETIME ftLastVisited;
        /// <summary>
        /// Last updated date (UTC)
        /// </summary>
        public FILETIME ftLastUpdated;
        /// <summary>
        /// The expiry date of the Web page's content (UTC)
        /// </summary>
        public FILETIME ftExpires;
        /// <summary>
        /// Flags. STATURLFLAGS Enumaration.
        /// </summary>
        public STATURLFLAGS dwFlags;

        /// <summary>
        /// sets a column header in the DataGrid control. This property is not needed if you do not use it.
        /// </summary>
        public string URL
        {
          get { return pwcsUrl; }
        }
        /// <summary>
        /// sets a column header in the DataGrid control. This property is not needed if you do not use it.
        /// </summary>
        public string Title
        {
          get
          {
            if (pwcsUrl.StartsWith("file:"))
              return Win32api.CannonializeURL(pwcsUrl, Win32api.shlwapi_URL.URL_UNESCAPE).Substring(8).Replace('/', '\\');
            else
              return pwcsTitle;
          }
        }
        /// <summary>
        /// sets a column header in the DataGrid control. This property is not needed if you do not use it.
        /// </summary>
        public DateTime LastVisited
        {
          get
          {
            return Win32api.FileTimeToDateTime(ftLastVisited).ToLocalTime();
          }
        }
        /// <summary>
        /// sets a column header in the DataGrid control. This property is not needed if you do not use it.
        /// </summary>
        public DateTime LastUpdated
        {
          get
          {
            return Win32api.FileTimeToDateTime(ftLastUpdated).ToLocalTime();
          }
        }
        /// <summary>
        /// sets a column header in the DataGrid control. This property is not needed if you do not use it.
        /// </summary>
        public DateTime Expires
        {
          get
          {
            try
            {
              return Win32api.FileTimeToDateTime(ftExpires).ToLocalTime();
            }
            catch (Exception)
            {
              return DateTime.Now;
            }
          }
        }

      }

      [StructLayout(LayoutKind.Sequential)]
      public struct UUID
      {
        public int Data1;
        public short Data2;
        public short Data3;
        public byte[] Data4;
      }

      //Enumerates the cached URLs
      [ComImport]
      [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      [Guid("3C374A42-BAE4-11CF-BF7D-00AA006946EE")]
      public interface IEnumSTATURL
      {
        void Next(int celt, ref STATURL rgelt, out int pceltFetched);	//Returns the next \"celt\" URLS from the cache
        void Skip(int celt);	//Skips the next \"celt\" URLS from the cache. doed not work.
        void Reset();	//Resets the enumeration
        void Clone(out IEnumSTATURL ppenum);	//Clones this object
        void SetFilter([MarshalAs(UnmanagedType.LPWStr)] string poszFilter, STATURLFLAGS dwFlags);	//Sets the enumeration filter

      }


      [ComImport]
      [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      [Guid("3C374A41-BAE4-11CF-BF7D-00AA006946EE")]
      public interface IUrlHistoryStg
      {
        void AddUrl(string pocsUrl, string pocsTitle, ADDURL_FLAG dwFlags);	//Adds a new history entry
        void DeleteUrl(string pocsUrl, int dwFlags);	//Deletes an entry by its URL. does not work!
        void QueryUrl([MarshalAs(UnmanagedType.LPWStr)] string pocsUrl, STATURL_QUERYFLAGS dwFlags, ref STATURL lpSTATURL);	//Returns a STATURL for a given URL
        void BindToObject([In] string pocsUrl, [In] UUID riid, IntPtr ppvOut); //Binds to an object. does not work!
        object EnumUrls { [return: MarshalAs(UnmanagedType.IUnknown)] get; }	//Returns an enumerator for URLs


      }

      [ComImport]
      [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      [Guid("AFA0DC11-C313-11D0-831A-00C04FD5AE38")]
      public interface IUrlHistoryStg2 : IUrlHistoryStg
      {
        new void AddUrl(string pocsUrl, string pocsTitle, ADDURL_FLAG dwFlags);	//Adds a new history entry
        new void DeleteUrl(string pocsUrl, int dwFlags);	//Deletes an entry by its URL. does not work!
        new void QueryUrl([MarshalAs(UnmanagedType.LPWStr)] string pocsUrl, STATURL_QUERYFLAGS dwFlags, ref STATURL lpSTATURL);	//Returns a STATURL for a given URL
        new void BindToObject([In] string pocsUrl, [In] UUID riid, IntPtr ppvOut);	//Binds to an object. does not work!
        new object EnumUrls { [return: MarshalAs(UnmanagedType.IUnknown)] get; }	//Returns an enumerator for URLs

        void AddUrlAndNotify(string pocsUrl, string pocsTitle, int dwFlags, int fWriteHistory, object poctNotify, object punkISFolder);//does not work!
        void ClearHistory();	//Removes all history items


      }

      //UrlHistory class
      [ComImport]
      [Guid("3C374A40-BAE4-11CF-BF7D-00AA006946EE")]
      public class UrlHistoryClass
      {
      }

    }
  }
}
