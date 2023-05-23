using flights.Domain.Interfaces.Repositories;
using flights.Domain.Interfaces.Services;
using flights.Domain.Services;
using flights.services.Respositories;
using Fligth.Common;
using Microsoft.OpenApi.Models;

namespace Flights.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }



        public IConfiguration Configuration { get; private set; }
        //private AppSettings _appSettings;


        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();



            services.AddScoped<IFlightsDomService, FlightsDomService>();
            services.AddScoped<IFlightsRespository, FlightsRespository>();

            services.AddCors(opciones =>

            opciones.AddDefaultPolicy(builder =>

            builder.WithOrigins("http://localhost:4200").AllowAnyOrigin().AllowAnyHeader().WithExposedHeaders()
            ));


            var appSettingsSection = Configuration.GetSection("AppSettings");

            if (appSettingsSection == null)
                throw new InvalidOperationException("No appsettings section has been found");

            services.Configure<AppSettings>(appSettingsSection);

            //_appSettings = appSettingsSection.Get<AppSettings>();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(opcions => opcions.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.keyToken)),
            //        ClockSkew = TimeSpan.Zero,


            //    });

            //services.AddApplicationInsightsTelemetry(_appSettings.ApplicationInsights.InstrumentationKey);
            
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }

            });
            });
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)

        {





            if (env.IsDevelopment())
            {
                //loggerFactory.AddFile("Log/Log-{Date0}.txt");

            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(Endpoint =>
            {
                Endpoint.MapControllers();

            });



        }
    }
}
