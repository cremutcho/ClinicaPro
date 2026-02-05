using ClinicaPro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IConsultaService
    {
        Task<Consulta> CriarAsync(Consulta consulta);
        Task<Consulta> AtualizarAsync(Consulta consulta);
        Task ExcluirAsync(int id);
        Task<Consulta?> BuscarPorIdAsync(int id);
        Task<List<Consulta>> ListarAsync();

        // ✅ agora tudo em INT
        Task<bool> VerificaConflitoHorario(
            int medicoId,
            DateTime dataHora,
            int? consultaId = null
        );
    }
}
