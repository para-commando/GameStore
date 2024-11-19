using GameStore.Api.Contracts;

namespace GameStore.Api.ExtensionClasses;

public static class ExtensionClasses
{

  private static string getGameEndpointName = "get-game-by-id";

  private static readonly List<GameContracts> gameContracts = [
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
    group.MapGet("/get-all-games", () =>
  {
    if (gameContracts == null || gameContracts.Count == 0)
    {
      return Results.NotFound("No games found."); // Return 404 if no games exist
    }

    return Results.Ok(gameContracts);
  });


    group.MapGet("/get-game-by-id/{id}", (int id) =>
    {
      var game = gameContracts.Find(g => g.id == id);

      if (game == null)
      {
        return Results.NotFound($"Game with ID {id} not found."); // Return 404 if the game is not found
      }

      return Results.Ok(game); // Return 200 OK with the game data
    }).WithName(getGameEndpointName);

    // Create a new game
    group.MapPost("/create-game", (CreateGameContract newGame) =>
    {
      try
      {

        var game = new GameContracts(
        gameContracts.Count + 1,
        newGame.name,
        newGame.genre,
        newGame.price,
        newGame.ReleaseDate
    );

        gameContracts.Add(game);

        // Return 201 Created with a "location" header pointing to the new game
        return Results.CreatedAtRoute(getGameEndpointName, new { id = game.id }, game);
      }
      catch (Exception ex)
      {
        return Results.Problem($"An error occurred while creating the game: {ex.Message}");
      }
    })

    // Update an existing game
    group.MapPut("/update-game/{id}", (int id, UpdateGameContract updateGame) =>
    {
      try
      {
        int gameIndex = gameContracts.FindIndex(game => game.id == id);

        if (gameIndex == -1)
        {
          return Results.NotFound($"Game with ID {id} not found."); // Return 404 if the game is not found
        }

        var updatedGame = new GameContracts(
        id,
        updateGame.name,
        updateGame.genre,
        updateGame.price,
        updateGame.ReleaseDate
    );

        gameContracts[gameIndex] = updatedGame;

        return Results.NoContent(); // Return 204 if the update is successful
      }
      catch (Exception ex)
      {
        return Results.Problem($"An error occurred while updating the game: {ex.Message}");
      }
    });

    // Delete a game
    group.MapDelete("/delete-game/{id}", (int id) =>
    {
      try
      {
        int removedCount = gameContracts.RemoveAll(game => game.id == id);

        if (removedCount == 0)
        {
          return Results.NotFound($"Game with ID {id} not found."); // Return 404 if no game was deleted
        }

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
