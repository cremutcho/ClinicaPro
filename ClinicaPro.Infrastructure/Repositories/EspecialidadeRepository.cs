// ClinicaPro.Infrastructure/Repositories/EspecialidadeRepository.cs
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class EspecialidadeRepository : GenericRepository<Especialidade>, IEspecialidadeRepository
    {
        public EspecialidadeRepository(ClinicaDbContext context) : base(context)
        {
            // O GenericRepository deve implementar a lógica de CRUD (GetAllAsync, GetByIdAsync, etc.)
            // herdada pela interface IEspecialidadeRepository.
        }

        // Se você precisar de métodos específicos, adicione-os aqui.
    }
}