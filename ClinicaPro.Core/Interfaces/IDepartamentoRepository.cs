using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Interfaces
{
    // A interface IDepartamentoRepository herda de IRepository<Departamento>, 
    // ganhando automaticamente todos os métodos CRUD definidos (GetById, GetAll, Add, Update, Delete, Exists).
    public interface IDepartamentoRepository : IRepository<Departamento>
    {
        // 🆕 Métodos Específicos do Domínio Departamento:
        
        // Se no futuro você precisar buscar um Departamento pelo nome (que é único)
        Task<Departamento?> GetByNomeAsync(string nome);
        
        // Se precisar de alguma busca especializada
        // Task<IEnumerable<Departamento>> GetDepartamentosComMaisFuncionariosAsync();
    }
}