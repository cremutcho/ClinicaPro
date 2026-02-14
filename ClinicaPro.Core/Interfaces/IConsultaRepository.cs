using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Interfaces
{
    public interface IConsultaRepository : IRepository<Consulta, int>
    {
        Task<IEnumerable<Consulta>> GetByMedicoAsync(int medicoId);

        Task<IEnumerable<Consulta>> GetByPacienteAsync(int pacienteId);

        Task<bool> ExisteConsultaNoHorarioAsync(int medicoId, DateTime dataHora, int? consultaId);
           
    }
}
