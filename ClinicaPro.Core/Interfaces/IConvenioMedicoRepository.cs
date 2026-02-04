using ClinicaPro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IConvenioMedicoRepository
    {
        Task<ConvenioMedico> AddAsync(ConvenioMedico entity);
        Task<ConvenioMedico> UpdateAsync(ConvenioMedico entity);
        Task DeleteAsync(Guid id);
        Task<ConvenioMedico?> GetByIdAsync(Guid id);
        Task<List<ConvenioMedico>> GetAllAsync();
    }
}
