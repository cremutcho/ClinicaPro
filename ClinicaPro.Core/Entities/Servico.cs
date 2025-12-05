namespace ClinicaPro.Core.Entities
{
    public class Servico
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? CodigoTuss { get; set; }
        public decimal ValorPadrao { get; set; }

    }
}
