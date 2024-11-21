using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

// Passes the options to the base DbContext class, enabling EF Core to configure the database connection.
public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
  //  Represents a Database Table called Game
  public DbSet<Game> Games => Set<Game>();

  public DbSet<Genre> Genre => Set<Genre>();

  public DbSet<Clients> Clients => Set<Clients>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Genre>().HasData(
      new {Id=1, Name="Fighting"},
      new {Id=2, Name="Arcade"},
      new {Id=3, Name="Adventure"},
      new {Id=4, Name="Simulation"}
    );
  }

}
