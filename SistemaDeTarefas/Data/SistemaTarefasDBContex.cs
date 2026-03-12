using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data.Map;
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Data
{
    public class SistemaTarefasDBContex : DbContext
    {
        // Aqui estamos trabalhando com o ORM do Entity - que vai nos facilitar a trabalhar com qualquer banco
        // Então aqui vamos criar todas as nossas entidades do banco de dados e depois criar nosso banco
        // Se baseando por essas entidades (tabelas) que criamos aqui

        // Quando você cria o DbContext no Entity Framework Core, você está definindo como o banco de dados
        // será estruturado com base nas entidades da API

        // O DbContex é a classe responsável por:
        // Gerenciar a conexão com o banco
        // Mapear entidades → tabelas
        // Permitir consultas e gravações

        public SistemaTarefasDBContex(DbContextOptions<SistemaTarefasDBContex> options): base(options)
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<TarefaModel> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
