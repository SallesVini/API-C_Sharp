# 📌 API ASP.NET Core - Estrutura Básica

Este projeto é uma **API Web desenvolvida com ASP.NET Core utilizando Entity Framework Core** para acesso ao banco de dados.

O objetivo deste projeto é demonstrar uma **estrutura simples e organizada para criação de APIs**, utilizando padrões comuns como **Repository Pattern** e separação de responsabilidades.

---

# 🧱 Estrutura do Projeto

A estrutura da aplicação segue uma organização comum para APIs em projetos pequenos e médios:

Controllers
Models
Data
Repositories


Cada parte possui uma responsabilidade específica dentro da aplicação.

---

# 📦 Models

Nos **Models** são criadas as entidades da aplicação.

Essas entidades representam **como os dados serão estruturados na aplicação e no banco de dados**. Ou seja, cada Model normalmente representa **uma tabela no banco**, enquanto suas propriedades representam **as colunas dessa tabela**.

Exemplo:

```csharp
public class UsuarioModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
}
```
Além disso, no próprio Model também podem ser definidas algumas configurações, como:

tamanho máximo de campos

campos obrigatórios

tipos de dados

outras regras básicas da entidade


🗄 Data (DbContext)

Na pasta Data fica o DbContext, que é a classe responsável por:

gerenciar a conexão com o banco de dados

mapear as entidades criadas nos Models para as tabelas no banco

permitir consultas, inserções, atualizações e exclusões de dados

Exemplo simplificado:

public class SistemaTarefasDBContext : DbContext
{
    public DbSet<UsuarioModel> Usuarios { get; set; }
}

Ou seja, o DbContext é o ponto central de comunicação entre a aplicação e o banco de dados.


📂 Repositories

Nos Repositórios é onde organizamos o acesso aos dados.

Primeiro criamos uma interface, que define quais métodos estarão disponíveis para manipular uma entidade no banco de dados.

Exemplo de interface:

public interface IUsuarioRepositorio
{
    Task<List<UsuarioModel>> BuscarTodosUsuarios();
    Task<UsuarioModel> BuscarPorId(int id);
    Task<UsuarioModel> Adicionar(UsuarioModel usuario);
    Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id);
    Task<bool> Apagar(int id);
}

Essa interface funciona como um contrato, ou seja:

Qualquer classe que implementar essa interface precisa obrigatoriamente implementar esses métodos.


⚙ Implementação do Repositório

Depois criamos a classe que implementa essa interface, onde colocamos a lógica responsável por acessar o banco de dados utilizando o DbContext.

Exemplo:

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly SistemaTarefasDBContext _dbContext;

    public UsuarioRepositorio(SistemaTarefasDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
    {
        return await _dbContext.Usuarios.ToListAsync();
    }
}

Essa classe é responsável por executar as operações no banco de dados.

🗺 Mapeamento de Entidades (Map)

Em projetos pequenos, algumas configurações da entidade podem ficar dentro do próprio Model.

Porém, em projetos maiores é mais comum separar essas configurações criando uma pasta chamada Map dentro da pasta Data.

Essas classes são responsáveis por configurar como as entidades serão mapeadas no banco de dados.

Exemplo:

public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
{
    public void Configure(EntityTypeBuilder<UsuarioModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
    }
}

Com isso conseguimos definir regras como:

chave primária

tamanho máximo de campos

campos obrigatórios

tipos de dados

relacionamentos entre tabelas

🌐 Controllers

Por fim, criamos os Controllers, que são responsáveis por definir os endpoints da API.

Os controllers recebem as requisições HTTP e chamam os métodos dos Repositórios para manipular os dados no banco.

Exemplo:

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }

    [HttpGet]
    public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
    {
        return await _usuarioRepositorio.BuscarTodosUsuarios();
    }
}

Fluxo da aplicação:

Controller → Repository → DbContext → Banco de Dados


🚀 Evoluindo a API

Após criar uma API básica funcionando, podemos torná-la mais robusta adicionando novas camadas e boas práticas, como:

DTOs (Data Transfer Objects)

Utilizados para controlar quais dados entram e saem da API.

Services

Camada responsável por conter regras de negócio da aplicação.

Fluxo passa a ser:


Controller → Service → Repository → DbContext → Banco

Autenticação com JWT

Podemos adicionar autenticação utilizando JSON Web Token (JWT) para proteger endpoints da API.

Isso permite:

autenticação de usuários

autorização de acesso

segurança nos endpoints

📚 Tecnologias Utilizadas

ASP.NET Core

Entity Framework Core

SQL Server

C#

REST API

📌 Conclusão

Essa estrutura é uma forma simples e organizada de construir APIs utilizando ASP.NET Core e Entity Framework Core.

Ela permite separar responsabilidades da aplicação e facilita a manutenção e evolução do projeto, podendo crescer para arquiteturas mais complexas conforme necessário.

