using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Interfaces
{
    // A interface ICargoRepository herda de IRepository<Cargo>, 
    // garantindo que ela tenha os métodos CRUD básicos (Add, GetById, Update, Delete).
    public interface ICargoRepository : IRepository<Cargo>
    {
        // 🆕 Aqui você adiciona métodos específicos para Cargo, se necessário.
        // Exemplo:
        // Task<Cargo> GetByNomeAsync(string nome);
        // Task<IReadOnlyList<Cargo>> GetCargosMaisPopularesAsync();

        // 🆕 ADICIONAR ESTA LINHA:
        Task<Cargo?> GetByNomeAsync(string nome);
    }
}