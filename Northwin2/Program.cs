
using Northwin2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Northwin2.Services;
using System.Text.Json.Serialization;
using Northwin2.Middlewares;

namespace Northwin2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Enregistre la classe de contexte de données comme service
            // En lui indiquant la connexion à utiliser, et désactive le suivi des modifications
            string? connect = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ContexteNorthwind>(opt => opt.UseSqlServer(connect).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            // Enregistre les services metiers
            builder.Services.AddScoped<IEmployeService, EmployeServices>();

            builder.Services.AddControllers()
                .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Middleware de gestion des érreurs
            app.UseMiddleware<CustomErrorResponseMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
