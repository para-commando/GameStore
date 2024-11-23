using GameStore.Api.Data;
using GameStore.Api.ExtensionClasses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Register Swagger generator with proper grouping logic
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
// extension method
builder.AddSwaggerGenCustExt();

var sqliteConnectionString = builder.Configuration.GetConnectionString("GameStoreSqlite");
builder.Services.AddSqlite<GameStoreContext> (sqliteConnectionString);
// builder.Services.AddSqlite<MigrationDbContext> (sqliteConnectionString);

var app = builder.Build();

app.MapControllers();
// extension method
app.UseSwaggerCustExt();
// via extension methods all the related minimal apis are grouped together
app.MapGamesEndpointsExt();

// look into these properly and time 2:20
app.MigrateDbContextOne();
// app.MigrateDbContextTwo();
app.MapGet("/status-check", () => "Hello, Commando");

app.Run();

