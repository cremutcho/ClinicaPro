namespace ClinicaPro.Core.Entities
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Sigla { get; set; } = null!;
        // Outras propriedades, como um chefe de departamento, podem ser adicionadas.
    }
}