using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
  public static async Task MigrateDbContextOne(this WebApplication app)
  {
    using var serviceScope = app.Services.CreateScope();
    var contextTwo = serviceScope.ServiceProvider.GetRequiredService<GameStoreContext>();

    await contextTwo.Database.MigrateAsync();

  }
}
