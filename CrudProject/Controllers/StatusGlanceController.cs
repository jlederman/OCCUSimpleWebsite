using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudProject.Controllers
{
    public class StatusGlanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusGlanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Readonly Items List
        public async Task<IActionResult> Index(string searchString)
        {
            var crudItems = from c in _context.CrudItems select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                crudItems = crudItems.Where(c => c.Name.Contains(searchString) || c.Description.Contains(searchString) || c.Status.Contains(searchString));
            }
            var crudItemsList = await crudItems.ToListAsync();
            return View(crudItemsList);
        }
    }
}