namespace ClinicaPro.Core.Entities
{
    public class Cargo
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        // Você pode adicionar propriedades de navegação para Funcionario no futuro, 
        // mas por agora, vamos mantê-lo simples.
    }
}