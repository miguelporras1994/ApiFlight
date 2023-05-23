using Flights.Api;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);


//builder.Services.AddDbContext<ModelContext>(opciones =>
//{
//    opciones.UseOracle(builder.Configuration.GetConnectionString("conexionSql"));
//});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:8080",
                                              "http://localhost:8081",
                                              "http://localhost:4200").AllowAnyMethod().AllowAnyHeader().WithExposedHeaders();
                      });
});

var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();


// Configure the HTTP request pipeline.

startup.Configure(app, app.Environment, loggerFactory);

app.Run();