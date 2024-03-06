using Net.WebCore.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureApplicationServices(builder.Configuration);

// Build WebApplication
var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureRequestPipeline();

app.StartEngine();

// Start WebApplication
app.Run();
