using InfoTrack.Refactoring.Api.Middleware;
using InfoTrack.Refactoring.Api.Utility;
using InfoTrack.Refactoring.Application;
using InfoTrack.Refactoring.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using InfoTrack.Refactoring.Persistence;
using InfoTrack.Refactoring.Application.Contracts;
using InfoTrack.Refactoring.Api.Services;
//using Serilog;

namespace InfoTrack.Refactoring.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
        {
            AddSwagger(builder.Services);
            builder.Services.AddApiVersioning(
                setupAction =>
                {
                    setupAction.AssumeDefaultVersionWhenUnspecified = true;
                    setupAction.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
                    setupAction.ReportApiVersions = true;
                }
                );

            //builder.Services.AddApplicationInsightsTelemetry();
            builder.Services.AddApplicationServices();           
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddInfoTrackWCFServices();

            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

            builder.Services.AddHttpContextAccessor();
            

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            return builder.Build();

        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "InfoTrack Management API");
                });
            }

            app.UseHttpsRedirection();

            //app.UseRouting();
            
            app.UseAuthentication();

            app.UseCustomExceptionHandler();

            app.UseCors("Open");

            app.UseAuthorization();

            app.MapControllers();// it implements IEndpoint 

            return app;

        }
        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
              
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });



                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Infotrack Management API",

                });

                c.OperationFilter<FileResultContentTypeOperationFilter>();
            });
        }

        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<InfoTrackDbContext>();
                if (context != null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}