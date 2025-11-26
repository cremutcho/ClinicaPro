using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class ContaPagarRepository : GenericRepository<ContaPagar>, IContaPagarRepository
    {
        public ContaPagarRepository(ClinicaDbContext context) : base(context)
        {
        }
    }
}
