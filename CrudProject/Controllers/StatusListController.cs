using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudProject.Controllers
{
    public class StatusListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Readonly Items List
        public async Task<IActionResult> Index(string searchString)
        {
            var statusListLitems = from c in _context.StatusList select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                statusListLitems = statusListLitems.Where(c => c.Name.Contains(searchString) || c.Status.Contains(searchString));
            }
            var statusList = await statusListLitems.ToListAsync();
            return View(statusList);
        }
    }
}