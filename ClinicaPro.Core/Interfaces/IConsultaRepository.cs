using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Interfaces
{
    public interface IConsultaRepository : IRepository<Consulta>
    {
        Task<IEnumerable<Consulta>> GetByMedicoAsync(int medicoId);
        Task<IEnumerable<Consulta>> GetByPacienteAsync(int pacienteId);
        Task<bool> VerificaConflitoHorario(int medicoId, DateTime dataHora);
    }
}
