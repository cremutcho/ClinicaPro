using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class PagamentoRepository : GenericRepository<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(ClinicaDbContext context) : base(context)
        {
        }
    }
}
