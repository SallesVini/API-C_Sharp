using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        // A interface IUsuarioRepositorio serve para definir os métodos (contrato) que serão usados para
        // manipular os usuários no banco de dados, mas sem implementar a lógica nesses métodos

        // Ela basicamente diz:
        // Qualquer classe que implemente esse repositório precisa ter esses métodos para trabalhar com usuários
        // Os métodos para inserir usuarios, deletar e entre outros

        // E vamos definir os metodos aqui no contrato como assincronos por isso colocamos em uma task<>

        Task<List<UsuarioModel>> BuscarTodosUsuarios();
        Task<UsuarioModel> BuscarPorId(int id);
        Task<UsuarioModel> Adicionar(UsuarioModel usuario);
        Task<UsuarioModel> Atualizar(UsuarioModel usuario,int id);
        Task<bool> Apagar(int id);
    }
}
