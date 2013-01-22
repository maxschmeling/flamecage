using System;
using System.Linq.Expressions;
using GrayHills.FlameCage.Client.Models;

namespace GrayHills.FlameCage.Client.ViewModels
{
  public class AppearanceSettingsViewModel
  {
    private readonly IRepository repository;

    public bool UseAeroColor
    {
      get { return StringToBool(GetValue(() => UseAeroColor), true); }
    }

    public AppearanceSettingsViewModel(IRepository repository)
    {
      this.repository = repository;
    }

    private string GetValue<TReturn>(Expression<Func<TReturn>> expression)
    {
      var setting = repository.SingleOrDefault<Setting>(s => s.Name == expression.Name);

      return setting == null ? string.Empty : setting.Value;
    }

    private bool StringToBool(string val, bool defaultValue)
    {
      if (string.IsNullOrWhiteSpace(val))
        return defaultValue;

      return bool.Parse(val);
    }

    private void SetValue<TReturn>(Expression<Func<TReturn>> expression, string value)
    {
      var setting = repository.SingleOrDefault<Setting>(s => s.Name == expression.Name);

      if (setting == null)
      {
        setting = new Setting
        {
          Name = expression.Name
        };
        repository.Add(setting);
      }

      setting.Value = value;
    }
  }
}
