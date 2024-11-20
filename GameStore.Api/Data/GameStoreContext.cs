using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

// Passes the options to the base DbContext class, enabling EF Core to configure the database connection.
public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
  //  Represents a Database Table called Game
  public DbSet<Game> Games => Set<Game>();

  public DbSet<Genre> Genre => Set<Genre>();
}
