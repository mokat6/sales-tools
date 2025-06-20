using System.Reflection;
using gatewayRoot.SwaggerFilters;
using Newtonsoft.Json.Converters;

// using System.Text.Json.Serialization;
using ProtoApi = big_data.Proto;

var builder = WebApplication.CreateBuilder(args);


// THIS IS default serializer System.Text.Json  . you can use only one serializer. Not using default, using Newton because need JSON patch.
// builder.Services.AddControllers().AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//     });


// changed to this, to enable JSON patch. needed for partial updates - PATCH.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter()); // enums as strings in HTTP responses only. not swagger.json
    });



builder.Services.AddEndpointsApiExplorer(); // Add OpenAPI (Swagger) support
builder.Services.AddSwaggerGen(c =>
{

    // enables XML description. /// comments in controllers
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    // SERIALIZE enum to string (not int) in swagger.json (also swagger docs)   
    c.SchemaFilter<StringEnumSchemaFilter>();

});//.AddSwaggerGenNewtonsoftSupport();    // this last .AddSwaggerGenNewtonSoftSupport() enables enum string values in swagger.json



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
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()    // <-- or use .WithOrigins("http://your-frontend-ip:port")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//  -------





var app = builder.Build();


app.UseCors("AllowAll");  // apply CORS policy

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


