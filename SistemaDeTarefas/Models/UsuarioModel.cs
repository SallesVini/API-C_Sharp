namespace SistemaDeTarefas.Models
{
    public class UsuarioModel
    {
        // Aqui vamos representar a nossa entidade (tabela do banco de dados) usuario

        // [Key]
        public int Id { get; set; }

        /*
          [Required]
          [MaxLength(255)]
         */
        public string? Nome { get; set; }
        public string? Email { get; set; }
    }
}
