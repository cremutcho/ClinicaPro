using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class ContaReceberRepository : GenericRepository<ContaReceber>, IContaReceberRepository
    {
        public ContaReceberRepository(ClinicaDbContext context) : base(context)
        {
        }
    }
}
