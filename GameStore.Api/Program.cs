using GameStore.Api.Contracts;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register Swagger generator with proper grouping logic
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
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



var app = builder.Build();
app.MapControllers();
// Configure Swagger middleware
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
// Sample data
List<GameContracts> gameContracts = [
  new (1, "The Legend of Zelda: Breath of the Wild", "Adventure", 59.99m, new DateOnly(2017, 3, 3)),
  new (2, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
  new  (3, "Halo Infinite", "Shooter", 69.99m, new DateOnly(2021, 12, 8)),
  new  (4, "God of War: Ragnarok", "Action", 69.99m, new DateOnly(2022, 11, 9)),
  new  (5, "Elden Ring", "RPG", 59.99m, new DateOnly(2022, 2, 25)),
  new  (6, "Cyberpunk 2077", "RPG", 29.99m, new DateOnly(2020, 12, 10)),
  new  (7, "Animal Crossing: New Horizons", "Simulation", 49.99m, new DateOnly(2020, 3, 20)),
  new  (8, "Super Mario Odyssey", "Platformer", 49.99m, new DateOnly(2017, 10, 27)),
];
// Map endpoints with group metadata for Swagger
app.MapGet("/games/get-all-games", () => gameContracts)
   .WithMetadata(new Microsoft.AspNetCore.Mvc.ApiExplorerSettingsAttribute { GroupName = "game-api/v1" });

app.MapGet("/games/get-game-by-id/{id}", (int id) => gameContracts.Find(game => game.id == id))
   .WithMetadata(new Microsoft.AspNetCore.Mvc.ApiExplorerSettingsAttribute { GroupName = "game-api/v1" });

app.MapGet("/status-check", () => "Hello, Commando")
   .WithMetadata(new Microsoft.AspNetCore.Mvc.ApiExplorerSettingsAttribute { GroupName = "game-api/v1" });

app.Run();
