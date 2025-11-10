using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaPro.Infrastructure.Repositories
{
    // Esta classe herda do seu repositório genérico (Repository<T>)
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
    {
        // Assume que o construtor do Repositório genérico exige o ClinicaDbContext
        public FuncionarioRepository(ClinicaDbContext context) : base(context) {}

        public async Task<IEnumerable<Funcionario>> ObterFuncionariosAtivosAsync()
        {
            // Usando o DbSet diretamente (acessando _context.Set<Funcionario>() ou o DbSet exposto no contexto)
            return await _context.Set<Funcionario>()
                                 .Where(f => f.Ativo)
                                 .ToListAsync();
        }

        // ✅ MÉTODO ADICIONADO PARA RESOLVER O ERRO CS0535
        /// <summary>
        /// Verifica se um funcionário com o CPF fornecido já existe no banco de dados.
        /// </summary>
        /// <param name="cpf">O CPF a ser verificado.</param>
        /// <returns>True se o CPF já existir, False caso contrário.</returns>
        public async Task<bool> ExisteCpfAsync(string cpf)
        {
            // O acesso é feito através da coleção DbSet (Funcionarios) ou do _context.Set<Funcionario>()
            // Assumindo que o _context expõe a coleção Funcionarios ou que você usa o Set<T>
            return await _context.Set<Funcionario>()
                                 .Where(f => f.CPF == cpf)
                                 .AnyAsync();
        }
    }
}