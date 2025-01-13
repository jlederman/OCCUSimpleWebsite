using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CrudProject.Controllers
{
    public class CrudItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CrudItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CrudItems
        public async Task<IActionResult> Index(string searchString)
        {
            var crudItems = from c in _context.CrudItems select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                crudItems = crudItems.Where(c => c.Name.Contains(searchString) || c.Description.Contains(searchString));
            }
            var crudItemsList = await crudItems.ToListAsync();
            return View(crudItemsList);
        }

        // GET: CrudItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crudItem = await _context.CrudItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crudItem == null)
            {
                return NotFound();
            }

            return View(crudItem);
        }

        // GET: CrudItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CrudItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Status,LastEdit")] CrudItem crudItem)
        {
            if (ModelState.IsValid)
            {
                var existingCrudItem = await _context.CrudItems
                        .FirstOrDefaultAsync(i => i.Name == crudItem.Name);
                if (existingCrudItem != null)
                {
                    ModelState.AddModelError("Name", "The Name must be unique.");
                    return View(crudItem);
                }
                crudItem.LastEdit = DateTime.Now;
                _context.Add(crudItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crudItem);
        }

        // GET: CrudItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crudItem = await _context.CrudItems.FindAsync(id);
            if (crudItem == null)
            {
                return NotFound();
            }
            return View(crudItem);
        }
        
        public async Task<IActionResult> Copy(int? id)
        {
            if (id == null || _context.CrudItems == null)
            {
                return NotFound();
            }

            var crudItem = await _context.CrudItems.FindAsync(id);
            if (crudItem == null || _context.CrudItems == null)
            {
                return NotFound();
            }

            var copiedCrudItem = new CrudItem
            {
                Name = crudItem.Name + "(Copy)",
                Description = crudItem.Description,
                Status = crudItem.Status,
                LastEdit = crudItem.LastEdit
            };

            await _context.SaveChangesAsync();

            return View("Create", copiedCrudItem);
        }

        // POST: CrudItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Status")] CrudItem crudItem)
        {
            if (id != crudItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    {
                        var existingCrudItem = await _context.CrudItems.FindAsync(id);
                        if (existingCrudItem == null) {
                            return NotFound();
                        }
                        existingCrudItem.Id = crudItem.Id;
                        existingCrudItem.Name = crudItem.Name;
                        existingCrudItem.Description = crudItem.Description;
                        existingCrudItem.Status = crudItem.Status;
                        existingCrudItem.LastEdit = DateTime.Now;
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrudItemExists(crudItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(crudItem);
        }

        // GET: CrudItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crudItem = await _context.CrudItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crudItem == null)
            {
                return NotFound();
            }

            return View(crudItem);
        }

        // POST: CrudItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crudItem = await _context.CrudItems.FindAsync(id);
            if (crudItem != null)
            {
                _context.CrudItems.Remove(crudItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrudItemExists(int id)
        {
            return _context.CrudItems.Any(e => e.Id == id);
        }

        public IActionResult Compare()
        {
            var crudItems = _context.CrudItems
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToList();

            var viewModel = new CrudItemComparisonViewModel
            {
                CrudItemOptions = crudItems
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Compare(CrudItemComparisonViewModel viewModel)
        {
            if (viewModel.CrudItemId1.HasValue)
            {
                viewModel.CrudItem1 = _context.CrudItems.Find(viewModel.CrudItemId1.Value);
            }

            if (viewModel.CrudItemId2.HasValue)
            {
                viewModel.CrudItem2 = _context.CrudItems.Find(viewModel.CrudItemId2.Value);
            }

            viewModel.CrudItemOptions = _context.CrudItems
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToList();

            return View(viewModel);
        }
    }
}
