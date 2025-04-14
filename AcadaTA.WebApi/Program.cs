
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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Acada TA Api", Version = "v1" });

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

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(RequestMappingProfile));
            builder.Services.AddAutoMapper(typeof(ResponseMappingProfile));

            // Register repositories
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            // Register services
            builder.Services.AddScoped<IProductService, ProductService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
