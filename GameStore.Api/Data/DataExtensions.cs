using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
  public static void MigrateDbContextOne(this WebApplication app)
  {
    using var serviceScope = app.Services.CreateScope();
    var contextTwo = serviceScope.ServiceProvider.GetRequiredService<GameStoreContext>();

    contextTwo.Database.MigrateAsync();

  }
}
