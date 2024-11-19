using GameStore.Api.ExtensionClasses;

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

// via extension methods all the related minimal apis are grouped together
app.MapGamesEndpoints();

app.MapGet("/status-check", () => "Hello, Commando");

app.Run();

