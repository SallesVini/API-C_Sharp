
using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Repositorios;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Configuração do Entity Framework para usar o SQL Server
            // Aqui registramos nosso DbContext (SistemaTarefasDBContex)
            // configurando ele (DbContext) para utilizar o SQL Server como banco de dados
            // DbContext é a classe responsável por gerenciar a conexão com o banco de dados e
            // mapear as entidades para as tabelas
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<SistemaTarefasDBContex>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );


            // Configuração da Injeção de Dependência
            // Sempre que a interface IUsuarioRepositorio for solicitada,
            // o ASP.NET Core irá fornecer uma instância da classe UsuarioRepositorio
            // que é a classe que implementa essa interface
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

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
