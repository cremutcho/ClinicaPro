using ClinicaPro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<Funcionario>> GetAllAsync();
        Task<Funcionario?> GetByIdAsync(int id);
        Task AddAsync(Funcionario funcionario);
        Task UpdateAsync(Funcionario funcionario);
        Task DeleteAsync(int id);

        // 🔥 Novos métodos para RH
        Task<IEnumerable<Funcionario>> BuscarPorCargoAsync(int cargoId);
        Task<IEnumerable<Funcionario>> BuscarAtivosAsync();
        Task<IEnumerable<Funcionario>> BuscarInativosAsync();
        Task<IEnumerable<Funcionario>> BuscarPorPeriodoAsync(DateTime inicio, DateTime fim);
         Task<IEnumerable<Funcionario>> BuscarPorStatusAsync(bool ativo);  
    }
}
