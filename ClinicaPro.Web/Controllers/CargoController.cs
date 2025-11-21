using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin,RH,Recepcionista")]
    public class CargoController : Controller
    {
        private readonly ICargoRepository _cargoRepository;

        public CargoController(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        // GET: Cargo
        public async Task<IActionResult> Index()
        {
            var cargos = await _cargoRepository.GetAllAsync();
            return View(cargos);
        }

        // GET: Cargo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cargo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Salario")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                await _cargoRepository.AddAsync(cargo);
                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        // GET: Cargo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cargo = await _cargoRepository.GetByIdAsync(id);
            if (cargo == null)
                return NotFound();
            return View(cargo);
        }

        // POST: Cargo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Salario")] Cargo cargo)
        {
            if (id != cargo.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _cargoRepository.UpdateAsync(cargo);
                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        // GET: Cargo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cargo = await _cargoRepository.GetByIdAsync(id);
            if (cargo == null)
                return NotFound();
            return View(cargo);
        }

        // POST: Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cargoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
