using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ASPprojekt.Models;

namespace ASPprojekt.Controllers
{
    public class ProductionOrderController : Controller
    {
        private readonly AppDbContext _context;

        public ProductionOrderController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productionOrders = await _context.ProductionOrders.ToListAsync();
            return View(productionOrders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductionOrder productionOrder)
        {
            if (ModelState.IsValid)
            {
                _context.ProductionOrders.Add(productionOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productionOrder);
        }


        // Wyświetlanie formularza edycji zlecenia produkcyjnego
        public async Task<IActionResult> Edit(int id)
        {
            var productionOrder = await _context.ProductionOrders.FindAsync(id);
            if (productionOrder == null)
            {
                return NotFound();
            }
            return View(productionOrder);
        }

        // Obsługa edycji zlecenia produkcyjnego
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductionOrder productionOrder)
        {
            if (id != productionOrder.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingProductionOrder = await _context.ProductionOrders.FindAsync(id);
                    if (existingProductionOrder == null)
                    {
                        return NotFound();
                    }

                    // Aktualizacja właściwości encji na podstawie przesłanych danych z formularza
                    existingProductionOrder.ProductID = productionOrder.ProductID;
                    existingProductionOrder.StartDate = productionOrder.StartDate;
                    existingProductionOrder.EndDate = productionOrder.EndDate;

                    _context.Entry(existingProductionOrder).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Obsługa wyjątku w przypadku wystąpienia konkurencyjnej zmiany danych
                    ModelState.AddModelError(string.Empty, "Inny użytkownik zmodyfikował dane zlecenia produkcyjnego. Proszę odświeżyć stronę i spróbować ponownie.");
                }
            }

            return View(productionOrder);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var productionOrder = await _context.ProductionOrders.FindAsync(id);
            if (productionOrder == null)
            {
                return NotFound();
            }

            bool isProductionOrderInAssignmentOrders = CheckIfProductionOrderInAssignmentOrders(id);
            ViewBag.IsProductionOrderInAssignmentOrders = isProductionOrderInAssignmentOrders;

            return View(productionOrder);
        }

        private bool CheckIfProductionOrderInAssignmentOrders(int productionOrderId)
        {
            return _context.AssignmentOrders.Any(a => a.OrderID == productionOrderId);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productionOrder = await _context.ProductionOrders.FindAsync(id);
            if (productionOrder == null)
            {
                return NotFound();
            }

            _context.ProductionOrders.Remove(productionOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
