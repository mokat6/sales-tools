using Microsoft.EntityFrameworkCore;
using big_data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    // download all table rows 9k rows. Without it, errors out message too large
    options.MaxReceiveMessageSize = 20 * 1024 * 1024; // 20 MB
    options.MaxSendMessageSize = 20 * 1024 * 1024;    // optional, if server sends large responses
});


builder.Services.AddDbContext<BigDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
// app.MapGrpcService<GreeterService>();
app.MapGrpcService<BigDataService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run();

