using DesafioWebApi.Client;
using DesafioWebApi.Client.Interfaces;
using DesafioWebApi.Data;
using DesafioWebApi.Repositorios;
using DesafioWebApi.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioWebApi
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
            builder.Services.AddSwaggerGen();


            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<ContextoDB>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );

            builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            builder.Services.AddScoped<ICotacaoClient, CotacaoClient>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            
                app.UseSwagger();
                app.UseSwaggerUI();
            

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}