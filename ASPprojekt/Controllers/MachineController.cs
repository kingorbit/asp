using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ASPprojekt.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPprojekt.Controllers
{
    public class MachineController : Controller
    {
        private readonly AppDbContext _context;

        public MachineController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var machines = await GetMachinesFromDatabase();
            return View(machines);
        }

        private async Task<List<Machine>> GetMachinesFromDatabase()
        {
            return await _context.Machines.ToListAsync();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Machine machine)
        {
            if (ModelState.IsValid)
            {
                _context.Machines.Add(machine);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(machine);
        }

        public ActionResult Edit(int id)
        {
            var machine = _context.Machines.Find(id);
            if (machine == null)
            {
                return NotFound();
            }
            return View(machine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Machine machine)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(machine).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(machine);
        }

        public ActionResult Delete(int id)
        {
            var machine = _context.Machines.Find(id);
            if (machine == null)
            {
                return NotFound();
            }

            bool isMachineInAssignmentOrders = CheckIfMachineInAssignmentOrders(id);
            ViewBag.IsMachineInAssignmentOrders = isMachineInAssignmentOrders;

            return View(machine);
        }

        private bool CheckIfMachineInAssignmentOrders(int machineId)
        {
            return _context.AssignmentOrders.Any(a => a.MachineID == machineId);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var machine = _context.Machines.Find(id);
            _context.Machines.Remove(machine);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
