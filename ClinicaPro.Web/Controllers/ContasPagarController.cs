using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicaPro.Web.Controllers
{
    public class ContasPagarController : Controller
    {
        private readonly IContaPagarRepository _repository;

        public ContasPagarController(IContaPagarRepository repository)
        {
            _repository = repository;
        }

        // GET: /ContasPagar
        public async Task<IActionResult> Index()
        {
            var contas = await _repository.GetAllAsync();
            return View(contas);
        }

        // GET: /ContasPagar/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var conta = await _repository.GetByIdAsync(id);
            if (conta == null) return NotFound();
            return View(conta);
        }

        // GET: /ContasPagar/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ContasPagar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContaPagar model)
        {
            if (!ModelState.IsValid) return View(model);

            await _repository.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: /ContasPagar/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var conta = await _repository.GetByIdAsync(id);
            if (conta == null) return NotFound();
            return View(conta);
        }

        // POST: /ContasPagar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContaPagar model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid) return View(model);

            await _repository.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: /ContasPagar/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var conta = await _repository.GetByIdAsync(id);
            if (conta == null) return NotFound();
            return View(conta);
        }

        // POST: /ContasPagar/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
