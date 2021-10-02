using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace DotNetCoreApi
{
  public class MainContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    public static MainContext Instance => new MainContext();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseMySQL(@"host=localhost; user=root; password=; port=3306; database=dotnet; sslmode=none");
  }
}