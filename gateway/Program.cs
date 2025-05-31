using System.Text.Json.Serialization;
using ProtoApi = big_data.Proto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); ; // Add services for controllers
builder.Services.AddEndpointsApiExplorer(); // Add OpenAPI (Swagger) support
builder.Services.AddSwaggerGen(); // Add Swagger generator

builder.Services.AddOpenApi();


//----------------
builder.Services.AddGrpcClient<ProtoApi.BigDataSuckers.BigDataSuckersClient>(o =>
{
    o.Address = new Uri("https://localhost:7115"); // Or wherever your gRPC service is
});

builder.Services.AddScoped<gatewayRoot.Services.BigDataGrpcClient>();
//--------------------
// CORS ---------------

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllLocalhost", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
                new Uri(origin).Host == "localhost")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//  -------





var app = builder.Build();


app.UseCors("AllowAllLocalhost");  // apply CORS policy

// Configure the HTTP request pipeline.
Console.WriteLine("!!!!!!! dev env");
Console.WriteLine(app.Environment.IsDevelopment());
Console.WriteLine(app.Environment);
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi(); needed in minimal API
    app.UseSwagger(); // Enable Swagger
    app.UseSwaggerUI(); // Enable Swagger UI

}





app.UseHttpsRedirection();
app.MapControllers(); // This will scan for controllers and set up the routes for them

app.Run();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

// app.Run();


