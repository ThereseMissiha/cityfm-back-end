
using Products.API;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
var environment = builder.Environment;

var startup = new Startup(config, environment, builder);

startup.ConfigureServices(services);

var app = builder.Build();

startup.Configure(app);

app.Run();