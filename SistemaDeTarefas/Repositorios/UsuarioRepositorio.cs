using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        // E essa classe vai implementar (utilizar) o contrato, a interface IUsuarioRepositorio

        // A classe UsuarioRepositorio, implementa a interface e coloca a lógica real nos métodos que criamos
        // na interface IUsuarioRepositorio e que vão acessar o banco


        // Então agora conseguimos pegar nosso contexto e buscar nossos usuários
        /* 
             Como o DbContex é a classe responsável por gerenciar a conexão com o banco de dados e mapear as 
             entidades para as tabelas, precisamos utilizá-lo dentro do repositório para acessar os dados.
             Por isso injetamos o contexto no construtor da classe UsuarioRepositorio. 
             Dessa forma conseguimos acessar o _dbContext, consultar o banco de dados, acessar a tabela de 
             usuários (Usuarios) definida no contexto e 
             então realizar operações como buscar, inserir, atualizar ou remover usuários
         */

        private readonly SistemaTarefasDBContex _dbContext;
        public UsuarioRepositorio(SistemaTarefasDBContex sistemaTarefasDbContex)
        {
            _dbContext = sistemaTarefasDbContex;
        }

        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);
            
            if(usuarioPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }
            
            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
