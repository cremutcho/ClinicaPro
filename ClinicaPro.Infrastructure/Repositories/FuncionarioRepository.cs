using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ClinicaPro.Infrastructure.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly ClinicaDbContext _context;

        public FuncionarioRepository(ClinicaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Funcionario>> GetAllAsync()
        {
            return await _context.Funcionarios
                .Include(f => f.Cargo)
                .ToListAsync();
        }

        public async Task<Funcionario?> GetByIdAsync(int id)
        {
            return await _context.Funcionarios
                .Include(f => f.Cargo)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddAsync(Funcionario funcionario)
        {
            await _context.Funcionarios.AddAsync(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Funcionario funcionario)
        {
            _context.Funcionarios.Update(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var funcionario = await GetByIdAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();
            }
        }

        // -------------------------------------------------------
        // 🔥 MÉTODOS NOVOS PARA SUPORTAR OS QUERIES DO RH
        // -------------------------------------------------------

        public async Task<IEnumerable<Funcionario>> BuscarPorCargoAsync(int cargoId)
        {
            return await _context.Funcionarios
                .Include(f => f.Cargo)
                .Where(f => f.CargoId == cargoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Funcionario>> BuscarAtivosAsync()
        {
            return await _context.Funcionarios
                .Include(f => f.Cargo)
                .Where(f => f.Ativo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Funcionario>> BuscarInativosAsync()
        {
            return await _context.Funcionarios
                .Include(f => f.Cargo)
                .Where(f => !f.Ativo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Funcionario>> BuscarPorPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Funcionarios
                .Include(f => f.Cargo)
                .Where(f => f.DataAdmissao >= inicio && f.DataAdmissao <= fim)
                .ToListAsync();
        }
        public async Task<IEnumerable<Funcionario>> BuscarPorStatusAsync(bool ativo)
        {
            return await _context.Funcionarios
                .Include(f => f.Cargo)
                .Where(f => f.Ativo == ativo)
                .ToListAsync();
        } 
    }
}
