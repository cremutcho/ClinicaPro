using ClinicaPro.Core.Entities;
using System;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IConsultaService
    {
        Task<Consulta> CriarAsync(Consulta consulta);
        Task<Consulta> AtualizarAsync(Consulta consulta);
        Task ExcluirAsync(Guid id);
        Task<Consulta?> BuscarPorIdAsync(Guid id);
        Task<List<Consulta>> ListarAsync();

        // ✅ Novo método para o teste
        Task<bool> VerificaConflitoHorario(int medicoId, DateTime dataHora);
    }
}
