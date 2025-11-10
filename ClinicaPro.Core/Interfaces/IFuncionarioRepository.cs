using ClinicaPro.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Interfaces
{
    // Assume que você tem um IRepository<T> genérico.
    public interface IFuncionarioRepository : IRepository<Funcionario> 
    {
        // Método específico, se necessário para o seu CRUD.
        // Se precisar de algo, vamos adicionar. Por enquanto, só a interface base.
        Task<IEnumerable<Funcionario>> ObterFuncionariosAtivosAsync();

        // 🎯 NOVO MÉTODO: Checa se um CPF já existe
        Task<bool> ExisteCpfAsync(string cpf);
    }
}