
using AcadaTA.Infrastructure;
using AcadaTA.Repositories.Implementations;
using AcadaTA.Repositories.Interfaces;
using AcadaTA.Services.Implementations;
using AcadaTA.Services.Interfaces;
using AcadaTA.WebApi.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace AcadaTA.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Catalog API", Version = "v1" });

                var assemblyNames = AppDomain.CurrentDomain
                                .GetAssemblies()
                                .Where(a => !a.IsDynamic)
                                .Select(a => a.GetName().Name)
                                .Distinct();

                foreach (var assemblyName in assemblyNames)
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");
                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath);
                    }
                }
            });

            // Add DbContext with SQL Server provider
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        sqlServerOptions =>
                        {
                            sqlServerOptions.MigrationsAssembly("AcadaTA.Infrastructure");
                            sqlServerOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(10),
                                errorNumbersToAdd: null);
                        }));

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(RequestMappingProfile));
            builder.Services.AddAutoMapper(typeof(ResponseMappingProfile));

            // Register repositories
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            // Register services
            builder.Services.AddScoped<IProductService, ProductService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // redirect to swagger/index.html
                app.Use(async (context, next) =>
                {
                    if (context.Request.Path == "/")
                    {
                        context.Response.Redirect("/swagger/index.html");
                        return;
                    }
                    await next();
                });
            }

            // If database doesn't exists on the machine, it will deploy it
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }

            app.MapGet("/health", () => Results.Ok("API is running."));

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
