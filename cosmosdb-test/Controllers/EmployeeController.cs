using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cosmosdb_test.Models;
using cosmosdb_test.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cosmosdb_test.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IconsmosDbService _cosmosDbService;
        public EmployeeController( IconsmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {

            var result = await _cosmosDbService.GetEmployeesAsync("SELECT * FROM c");
            return View(result);
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Employee  employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddEmployeeAsync(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null) return BadRequest();
            Employee employee = await _cosmosDbService.GetEmployeeAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.UpdateEmployeeAsync(employee.Id, employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null) return BadRequest();
            Employee employee = await _cosmosDbService.GetEmployeeAsync(id);

            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync(string id)
        {
            await _cosmosDbService.DelateEmployeeAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _cosmosDbService.GetEmployeeAsync(id));
        }
    }
}