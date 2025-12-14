using ClinicaPro.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IConsultaService : IBaseService<Consulta>
    {
        // Cria uma consulta aplicando regras de negócio (conflito de horário, data futura, etc.)
        Task<Consulta> CriarAsync(Consulta consulta);

        // Atualiza uma consulta aplicando regras de negócio
        Task AtualizarAsync(Consulta consulta);

        // Exclui uma consulta
        Task ExcluirAsync(int id);

        // Retorna todas as consultas de um paciente
        Task<IEnumerable<Consulta>> GetByPacienteIdAsync(int pacienteId);

        // Retorna todas as consultas de um médico
        Task<IEnumerable<Consulta>> GetByMedicoIdAsync(int medicoId);

        Task<bool> VerificaConflitoHorarioAsync(int medicoId, DateTime dataHora, int? consultaIdIgnorada = null);
    }
}
