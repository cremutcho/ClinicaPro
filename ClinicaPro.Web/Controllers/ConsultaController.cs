using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; // Resolve o erro CS0246: Controller e IActionResult
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediatR;
using ClinicaPro.Core.Entities;
using System.Collections.Generic;

// ✅ USINGS CRÍTICOS FALTANTES (Resolve ObterTodasConsultasQuery e outros comandos/queries)
using ClinicaPro.Core.Features.Consultas.Queries;
using ClinicaPro.Core.Features.Consultas.Commands;
using ClinicaPro.Core.Features.Medicos.Queries;
using ClinicaPro.Core.Features.Pacientes.Queries;

namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin,Medico,Recepcionista")]
    public class ConsultaController : Controller
    {
        // ✅ CAMPOS PRIVADOS FALTANTES (Resolve error CS0103: _mediator e _userManager)
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager; 

        // ✅ CONSTRUTOR FALTANTE (Garante que os campos sejam injetados)
        public ConsultaController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        // GET: Consulta (Refatorado com MediatR e Lógica de Segurança)
        public async Task<IActionResult> Index()
        {
            // 1. Usa Query para buscar todas as consultas (com Includes)
            var consultas = await _mediator.Send(new ObterTodasConsultasQuery());
            
            // 2. Garante que a lista não é nula (proteção contra falhas de DI/Handler)
            if (consultas == null)
            {
                consultas = new List<Consulta>();
            }

            // 3. Lógica de Segurança: Filtra consultas para o médico logado
            if (User.IsInRole("Medico"))
            {
                var user = await _userManager.GetUserAsync(User);
                
                // ✅ Tratamento de nulidade para Medico (Resolve o NullReferenceException anterior)
                consultas = consultas.Where(c => c.Medico != null && c.Medico.Email == user.Email);
            }

            return View(consultas.ToList()); 
        }

        // GET: Consulta/Details/5 (Refatorado com MediatR e Lógica de Segurança)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            // 1. Usa Query para buscar a consulta
            var consulta = await _mediator.Send(new ObterConsultaPorIdQuery(id.Value));

            if (consulta == null) return NotFound();

            // 2. Lógica de Segurança: Médicos só podem ver suas próprias consultas
            if (User.IsInRole("Medico"))
            {
                var user = await _userManager.GetUserAsync(User);
                // Certifica-se de que consulta.Medico não é nulo antes de acessar Email
                if (consulta.Medico == null || consulta.Medico.Email != user.Email)
                    return Forbid();
            }

            return View(consulta);
        }

        // GET: Consulta/Create (Refatorado para usar Queries de Dropdown)
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Create()
        {
            // Busca dados para dropdowns via MediatR/Queries
            var pacientes = await _mediator.Send(new ObterTodosPacientesQuery());
            var medicos = await _mediator.Send(new ObterTodosMedicosQuery());

            ViewBag.PacienteId = new SelectList(pacientes, "Id", "Nome");
            ViewBag.MedicoId = new SelectList(medicos, "Id", "Nome");
            return View();
        }

        // POST: Consulta/Create (Refatorado com Command)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Create([Bind("Id,DataHora,Status,PacienteId,MedicoId,Observacoes")] Consulta consulta)
        {
            if (!ModelState.IsValid)
            {
                // Lógica de erro: Recarrega dropdowns via Queries
                var pacientes = await _mediator.Send(new ObterTodosPacientesQuery());
                var medicos = await _mediator.Send(new ObterTodosMedicosQuery());

                ViewBag.PacienteId = new SelectList(pacientes, "Id", "Nome", consulta.PacienteId);
                ViewBag.MedicoId = new SelectList(medicos, "Id", "Nome", consulta.MedicoId);
                return View(consulta);
            }

            // ✅ REFATORADO: Envia o Command
            await _mediator.Send(new CriarConsultaCommand { Consulta = consulta });

            return RedirectToAction(nameof(Index));
        }

        // GET: Consulta/Edit/5 (Refatorado com Query e Queries de Dropdown)
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            // Busca a consulta via Query
            var consulta = await _mediator.Send(new ObterConsultaPorIdQuery(id.Value));

            if (consulta == null) return NotFound();

            // Recarrega dropdowns via Queries
            var pacientes = await _mediator.Send(new ObterTodosPacientesQuery());
            var medicos = await _mediator.Send(new ObterTodosMedicosQuery());

            ViewBag.PacienteId = new SelectList(pacientes, "Id", "Nome", consulta.PacienteId);
            ViewBag.MedicoId = new SelectList(medicos, "Id", "Nome", consulta.MedicoId);
            return View(consulta);
        }

        // POST: Consulta/Edit/5 (Refatorado com Command e Query de Concorrência)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHora,Status,PacienteId,MedicoId,Observacoes")] Consulta consulta)
        {
            if (id != consulta.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                // Lógica de erro: Recarrega dropdowns via Queries
                var pacientes = await _mediator.Send(new ObterTodosPacientesQuery());
                var medicos = await _mediator.Send(new ObterTodosMedicosQuery());
                ViewBag.PacienteId = new SelectList(pacientes, "Id", "Nome", consulta.PacienteId);
                ViewBag.MedicoId = new SelectList(medicos, "Id", "Nome", consulta.MedicoId);
                return View(consulta);
            }

            try
            {
                // ✅ REFATORADO: Envia o Command
                await _mediator.Send(new UpdateConsultaCommand { Consulta = consulta });
            }
            catch (DbUpdateConcurrencyException)
            {
                // ✅ REFATORADO: Usa Query para verificar existência
                var existe = await _mediator.Send(new ConsultaExisteQuery(consulta.Id));
                if (!existe) return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Consulta/Delete/5 (Refatorado com Query)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            // Busca a consulta via Query
            var consulta = await _mediator.Send(new ObterConsultaPorIdQuery(id.Value));

            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // POST: Consulta/Delete/5 (Refatorado com Command)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // ✅ REFATORADO: Envia o Command
            await _mediator.Send(new DeleteConsultaCommand(id));

            return RedirectToAction(nameof(Index));
        }
    }
}