using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Services
{
    public class PacienteService : BaseService<Paciente>, IPacienteService
    {
        private readonly IRepository<Paciente> _pacienteRepository;

        public PacienteService(IRepository<Paciente> pacienteRepository)
            : base(pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        // Métodos específicos de Paciente
        public async Task<IEnumerable<Paciente>> GetByNomeAsync(string nome)
        {
            var all = await _pacienteRepository.GetAllAsync();
            return all.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
        }

        // 🔍 Método temporário para testar se o banco está salvando corretamente
        public async Task TesteConexaoAsync()
        {
            var novo = new Paciente
            {
                Nome = "Teste " + DateTime.Now.ToString("HHmmss"),
                CPF = "00000000000",
                DataNascimento = DateTime.Now.AddYears(-30),
                Endereco = "Rua de Teste 123",
                Telefone = "999999999",
                Email = "teste" + DateTime.Now.ToString("HHmmss") + "@teste.com"
            };

            await _pacienteRepository.AddAsync(novo);
        }
    }
}