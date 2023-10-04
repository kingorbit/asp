using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ASPprojekt.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPprojekt.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = await GetProductsFromDatabase();
            return View(products);
        }

        private async Task<List<Product>> GetProductsFromDatabase()
        {
            return await _context.Products.ToListAsync();
        }


        // Wyświetlanie formularza tworzenia nowego produktu
        public ActionResult Create()
        {
            return View();
        }

        // Obsługa tworzenia nowego produktu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Wyświetlanie formularza edycji produktu
        // Wyświetlanie formularza edycji produktu
        public ActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Obsługa edycji produktu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = _context.Products.Find(id);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    // Aktualizacja właściwości encji na podstawie przesłanych danych z formularza
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;

                    _context.Entry(existingProduct).State = EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Obsługa wyjątku w przypadku wystąpienia konkurencyjnej zmiany danych
                    ModelState.AddModelError(string.Empty, "Inny użytkownik zmodyfikował dane produktu. Proszę odświeżyć stronę i spróbować ponownie.");
                }
            }

            return View(product);
        }



        // Wyświetlanie potwierdzenia przed usunięciem produktu
        public ActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Obsługa usuwania produktu
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
