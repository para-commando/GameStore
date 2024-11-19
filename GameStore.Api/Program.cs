using GameStore.Api.ExtensionClasses;

var builder = WebApplication.CreateBuilder(args);
// Register Swagger generator with proper grouping logic
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
// extension method
builder.AddSwaggerGenCustExt();

var app = builder.Build();

app.MapControllers();
// extension method
app.UseSwaggerCustExt();
// via extension methods all the related minimal apis are grouped together
app.MapGamesEndpointsExt();

app.MapGet("/status-check", () => "Hello, Commando");

app.Run();

