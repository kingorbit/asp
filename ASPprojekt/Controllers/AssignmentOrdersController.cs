using ASPprojekt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class AssignmentOrdersController : Controller
{
    private readonly AppDbContext _context;

    public AssignmentOrdersController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var assignmentOrders = await _context.AssignmentOrders.ToListAsync();
        return View(assignmentOrders);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AssignmentOrder assignmentOrder)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var newAssignmentOrder = new AssignmentOrder
                {
                    OrderID = assignmentOrder.OrderID,
                    MachineID = assignmentOrder.MachineID,
                    EmployeeID = assignmentOrder.EmployeeID,
                    AssignmentDate = assignmentOrder.AssignmentDate
                };

                // Sprawdzenie poprawności ID
                var existingMachine = await _context.Machines.FindAsync(newAssignmentOrder.MachineID);
                var existingEmployee = await _context.Employees.FindAsync(newAssignmentOrder.EmployeeID);
                var existingOrder = await _context.ProductionOrders.FindAsync(newAssignmentOrder.OrderID);
                if (existingMachine == null || existingEmployee == null || existingOrder == null)
                {
                    ViewBag.ErrorMessage = "Nieprawidłowe ID maszyny, pracownika lub zlecenia.";
                    return View(assignmentOrder);
                }

                _context.AssignmentOrders.Add(newAssignmentOrder);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(assignmentOrder);
        }
        catch (Exception ex)
        {
            // Obsługa błędu, np. logowanie, zwrócenie widoku z komunikatem o błędzie, itp.
            return View("Error");
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var assignmentOrder = await _context.AssignmentOrders.FindAsync(id);
        if (assignmentOrder == null)
        {
            return NotFound();
        }

        // Sprawdzenie poprawności ID
        var existingMachine = await _context.Machines.FindAsync(assignmentOrder.MachineID);
        var existingEmployee = await _context.Employees.FindAsync(assignmentOrder.EmployeeID);
        var existingOrder = await _context.ProductionOrders.FindAsync(assignmentOrder.OrderID);
        if (existingMachine == null || existingEmployee == null || existingOrder == null)
        {
            ViewBag.ErrorMessage = "Nieprawidłowe ID maszyny, pracownika lub zlecenia.";
            return View("Edit", assignmentOrder); // Przekazanie modelu do widoku
        }

        return View("Edit", assignmentOrder); // Przekazanie modelu do widoku
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AssignmentOrder assignmentOrder)
    {
        if (id != assignmentOrder.AssignmentOrdersID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Sprawdzenie poprawności ID
                var existingMachine = await _context.Machines.FindAsync(assignmentOrder.MachineID);
                var existingEmployee = await _context.Employees.FindAsync(assignmentOrder.EmployeeID);
                var existingOrder = await _context.ProductionOrders.FindAsync(assignmentOrder.OrderID);
                if (existingMachine == null || existingEmployee == null || existingOrder == null)
                {
                    ViewBag.ErrorMessage = "Nieprawidłowe ID maszyny, pracownika lub zlecenia.";
                    return View(assignmentOrder);
                }

                _context.Update(assignmentOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                // Obsługa błędu konkurencyjnej edycji
                ViewBag.ErrorMessage = "Wystąpił błąd podczas zapisu zmian. Sprawdź, czy rekord nie został zmodyfikowany przez innego użytkownika.";
                return View(assignmentOrder);
            }
            catch (Exception ex)
            {
                // Obsługa innych błędów
                ViewBag.ErrorMessage = "Wystąpił błąd podczas zapisywania zmian.";
                return View(assignmentOrder);
            }
        }

        return View(assignmentOrder);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var assignmentOrder = await _context.AssignmentOrders.FindAsync(id);
        if (assignmentOrder == null)
        {
            return NotFound();
        }
        return View(assignmentOrder);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var assignmentOrder = await _context.AssignmentOrders.FindAsync(id);
        _context.AssignmentOrders.Remove(assignmentOrder);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
