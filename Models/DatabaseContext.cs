using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RhythmApp.Models
{
  public partial class DatabaseContext : DbContext
  {

    public DbSet<Song> Songs { get; set; }
    public DbSet<Band> Bands { get; set; }
    public DbSet<Album> Albums { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseNpgsql("server=localhost;database=RhythmAppDatabase");
      }
    }
  }
}
