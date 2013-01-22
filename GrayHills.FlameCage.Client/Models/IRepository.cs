using System;
using System.Collections.Generic;

namespace GrayHills.FlameCage.Client.Models
{
  public interface IRepository
  {
      T SingleOrDefault<T>(Func<T, bool> predicate) where T : class;
    IEnumerable<T> All<T>() where T : class;
    void Add<T>(T item) where T : class;
    void Delete<T>(T item) where T : class;
    void Save();
  }
}
