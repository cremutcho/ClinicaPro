namespace ClinicaPro.Core.Features.Cargos.Queries
{
    // DTO usado para retornar os dados minimos e formatados para a camada Web
    public class CargoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}