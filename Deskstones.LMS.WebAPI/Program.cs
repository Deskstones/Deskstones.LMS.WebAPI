using Deskstones.LMS.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Register all services
builder.ConfigureServices();

var app = builder.Build();

// Configure all middleware
app.ConfigureMiddleware();

app.Run();