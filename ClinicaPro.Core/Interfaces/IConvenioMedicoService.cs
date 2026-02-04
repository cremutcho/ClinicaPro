using ClinicaPro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IConvenioMedicoService
    {
        Task<ConvenioMedico> CriarAsync(ConvenioMedico convenio);
        Task<ConvenioMedico> AtualizarAsync(ConvenioMedico convenio);
        Task<bool> DeletarAsync(Guid id);
        Task<List<ConvenioMedico>> ListarAsync();
        Task<ConvenioMedico?> BuscarPorIdAsync(Guid id);
    }
}
