using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data; // necessário para ClinicaDbContext
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class ConvenioMedicoRepository : IConvenioMedicoRepository
    {
        private readonly ClinicaDbContext _context;
        private readonly DbSet<ConvenioMedico> _dbSet;

        public ConvenioMedicoRepository(ClinicaDbContext context)
        {
            _context = context;
            _dbSet = _context.ConveniosMedicos; // referência direta à DbSet do DbContext
        }

        // =========================
        // CREATE
        // =========================
        public async Task<ConvenioMedico> AddAsync(ConvenioMedico entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // =========================
        // READ
        // =========================
        public async Task<ConvenioMedico?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<ConvenioMedico>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<ConvenioMedico?> GetByNomeAsync(string nome)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Nome == nome);
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<ConvenioMedico> UpdateAsync(ConvenioMedico entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // =========================
        // DELETE
        // =========================
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
