using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
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
            _dbSet = _context.ConveniosMedicos;
        }

        // =========================
        // CREATE
        // =========================
        public async Task<ConvenioMedico> AddAsync(ConvenioMedico entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // =========================
        // READ
        // =========================
        public async Task<ConvenioMedico?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<ConvenioMedico>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
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

        // =========================
        // VALIDATIONS
        // =========================
        public async Task<bool> ExistsByNameAsync(string nome)
        {
            return await _dbSet
                .AnyAsync(c => c.Nome.ToUpper() == nome.ToUpper());
        }

        public async Task<bool> ExistsByNameExceptIdAsync(string nome, Guid id)
        {
            return await _dbSet
                .AnyAsync(c => c.Nome.ToUpper() == nome.ToUpper() && c.Id != id);
        }
    }
}
