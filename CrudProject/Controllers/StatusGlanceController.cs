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
        public async Task<IActionResult> Index()
        {
            return View(await _context.CrudItems.ToListAsync());
        }
    }
}