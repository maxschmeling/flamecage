using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Database;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;

namespace GrayHills.FlameCage.Client.Models
{
  public class EfCfRepository : DbContext, IRepository
  {
    public EfCfRepository()
      : base(GetDbConnectionString())
    {
      DbDatabase.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
      DbDatabase.SetInitializer(new DropCreateDatabaseIfModelChanges<EfCfRepository>());
    }

    public DbSet<Site> Sites { get; set; }
    public DbSet<Setting> Settings { get; set; }

    #region IRepository Members

    public T SingleOrDefault<T>(Func<T, bool> predicate) where T : class
    {
      return Set<T>().SingleOrDefault(predicate);
    }

    public IEnumerable<T> All<T>() where T : class
    {
      return Set<T>().ToList();
    }

    public void Add<T>(T item) where T : class
    {
      Set<T>().Add(item);
    }

    public void Delete<T>(T item) where T : class
    {
      Set<T>().Remove(item);
    }

    public void Save()
    {
      SaveChanges();
    }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Site>().Ignore(s => s.Color);
    }

    public static string GetDbConnectionString()
    {
      var sdfPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "FlameCage.LocalStore.sdf");

      var builder = new SqlCeConnectionStringBuilder { DataSource = sdfPath };
      return builder.ToString();
    }
  }
}