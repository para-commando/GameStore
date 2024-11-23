using GameStore.Api.Contracts;
using GameStore.Api.Data;
using GameStore.Api.Entities;
using GameStore.Api.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.ExtensionClasses;

public static class ExtensionClasses
{

  private static string getGameEndpointName = "get-game-by-id";

// was used initially as database proxy
  private static readonly List<GameSummaryContracts> gameContracts = [
    new (1, "The Legend of Zelda: Breath of the Wild", "Adventure", 59.99m, new DateOnly(2017, 3, 3)),
  new (2, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
  new  (3, "Halo Infinite", "Shooter", 69.99m, new DateOnly(2021, 12, 8)),
  new  (4, "God of War: Ragnarok", "Action", 69.99m, new DateOnly(2022, 11, 9)),
  new  (5, "Elden Ring", "RPG", 59.99m, new DateOnly(2022, 2, 25)),
  new  (6, "Cyberpunk 2077", "RPG", 29.99m, new DateOnly(2020, 12, 10)),
  new  (7, "Animal Crossing: New Horizons", "Simulation", 49.99m, new DateOnly(2020, 3, 20)),
  new  (8, "Super Mario Odyssey", "Platformer", 49.99m, new DateOnly(2017, 10, 27)),
];
  public static WebApplicationBuilder AddSwaggerGenCustExt(this WebApplicationBuilder builder)
  {
    builder.Services.AddSwaggerGen(options =>
  {
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
      Title = "My API",
      Version = "v1",
      Description = "API for managing resources.",
      Contact = new Microsoft.OpenApi.Models.OpenApiContact
      {
        Name = "Ghatak Commando",
      }
    });

    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
      Title = "My API",
      Version = "v2",
      Description = "API for managing resources.",
      Contact = new Microsoft.OpenApi.Models.OpenApiContact
      {
        Name = "Para Commando",
      }
    });
  });
    return builder;
  }
  public static WebApplication UseSwaggerCustExt(this WebApplication app)
  {
    app.UseSwagger(options =>
    {
      options.RouteTemplate = "swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c =>
       {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
         c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API v2");
         c.RoutePrefix = string.Empty; // Swagger at root
       });
    return app;
  }

  public static RouteGroupBuilder MapGamesEndpointsExt(this WebApplication app)
  {
    // return type of this method changed to RouteGroupBuilder from WebApplication because we used MapGroup

    var group = app.MapGroup("games").WithParameterValidation();
    // below one can be app.MapGet but we wanted to append a route name hence we used MapGroup
    group.MapGet("/get-all-games", async(GameStoreContext gameStoreDbContext) =>
  {
   return await gameStoreDbContext.Games.Include(game=> game.genre).Select( game => game.ToEntity()).AsNoTracking().ToListAsync();
  //  added AsNoTracking as we are not going to update the above fetched data using db.save or any modifs will be made, hence to free up the efcore tracking of this data we used it
    // if (games == null || games.Count == 0)
    // {
    //   return Results.NotFound("No games found."); // Return 404 if no games exist
    // }

    // return Results.Ok(games);
  });


    group.MapGet("/get-game-by-id/{id}", async(int id, GameStoreContext gameStoreDbContext) =>
    {
      // we can even use var as datatype
      Game? game = await gameStoreDbContext.Games.FindAsync(id);

      if (game == null)
      {
        return Results.NotFound($"Game with ID {id} not found."); // Return 404 if the game is not found
      }

      return Results.Ok(game.ToCGameDetailsContracts()); // Return 200 OK with the game data
    }).WithName(getGameEndpointName);


    // When you define a parameter like GameStoreContext gameStoreDbContext in your handler, ASP.NET Core uses constructor injection to provide an instance of GameStoreContext automatically.
    // For this to work:
    // The GameStoreContext needs to be registered in the DI container, typically in the Program.cs file.
    // The DI system resolves the dbContext and passes it as an argument when invoking the handler.

    // Create a new game
    group.MapPost("/create-game", async(CreateGameContract newGame, GameStoreContext gameStoreDbContext) =>
    {
      try
      {
        // an extension method is defined on CreateGameContract so that we can get the entity object matching db table schema so that db operations can be performed, as db op demands an entity object
        Game game = newGame.ToEntity();
        game.genre = await gameStoreDbContext.Genre.FindAsync(newGame.genreId);

        // creating a record using entity, now this is no longer necessary as extension method is defined on CreateGameContract
        // Game game = new()
        // {
        //   name = newGame.name,
        //   genre = gameStoreDbContext.Genre.Find(newGame.genreId),
        //   genreId = newGame.genreId,
        //   price = newGame.price,
        //   releaseDate = DateOnly.FromDateTime(DateTime.Now),
        // };
        // after creating record we need to add it to the dbContext
        gameStoreDbContext.Games.Add(game);
        // this will create and execute the changes made to the dbContext so far by creating corresponding sql and executing them
        await gameStoreDbContext.SaveChangesAsync();

        // this is no longer necessary as extension method defined on the Game entity "ToCGameSummaryContracts" does it for us behind the scenes
        //  GameContracts gg = new(game.id, game.name, game.genre!.Name, game.price, game.releaseDate);
        // Return 201 Created with a "location" header pointing to the new game
        return Results.CreatedAtRoute(getGameEndpointName, new { id = game.id }, game.ToEntity());
      }
      catch (Exception ex)
      {
        return Results.Problem($"An error occurred while creating the game: {ex.Message}");
      }
    });

    // Update an existing game
    group.MapPut("/update-game/{id}", async (int id, UpdateGameContract updateGame, GameStoreContext gameStoreDbContext) =>
    {
      try
      {
        var existingGame = await gameStoreDbContext.Games.FindAsync(id);


        if (existingGame is null)
        {
          return Results.NotFound($"Game with ID {id} not found."); // Return 404 if the game is not found
        }
        gameStoreDbContext.Entry(existingGame).CurrentValues.SetValues(updateGame.ToUpdateGameDetailsContracts(id));
        await gameStoreDbContext.SaveChangesAsync();
        return Results.NoContent(); // Return 204 if the update is successful
      }
      catch (Exception ex)
      {
        return Results.Problem($"An error occurred while updating the game: {ex.Message}");
      }
    });

    // Delete a game
    group.MapDelete("/delete-game/{id}", async(int id, GameStoreContext gameStoreDbContext) =>
    {
      try
      {
        var existingGame = await gameStoreDbContext.Games.FindAsync(id);


        if (existingGame is null)
        { return Results.NotFound("game not found"); }

        // var removedCount = gameStoreDbContext.Remove(existingGame);
        // Console.WriteLine(removedCount);
        // gameStoreDbContext.SaveChanges();
       await gameStoreDbContext.Games.Where(game => game.id == id).ExecuteDeleteAsync();

        return Results.NoContent(); // Return 204 if the delete is successful
      }
      catch (Exception ex)
      {
        return Results.Problem($"An error occurred while deleting the game: {ex.Message}");
      }
    });
    return group;
  }
}
