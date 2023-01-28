using Microsoft.AspNetCore.Mvc;
using Senkadagala.DAL;
using Senkadagala.Models;
using Senkadagala.ViewModels;
using System.Diagnostics;

namespace Senkadagala.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM home = new HomeVM { Information = _context.Information};
            return View(home);
        }

    }
}