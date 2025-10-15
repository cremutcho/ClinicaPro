// ClinicaPro.Core/Interfaces/IEspecialidadeRepository.cs
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Interfaces
{
    // IEspecialidadeRepository herda do repositório genérico para Especialidade
    // (A menos que você precise de métodos específicos para Especialidade)
    public interface IEspecialidadeRepository : IGenericRepository<Especialidade> 
    {
        // Se você não precisa de métodos específicos, a interface pode ficar vazia,
        // apenas para que o IoC Container (injeção de dependência) possa funcionar.
    }
}