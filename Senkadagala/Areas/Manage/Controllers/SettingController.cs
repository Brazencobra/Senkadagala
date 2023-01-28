using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Senkadagala.DAL;
using Senkadagala.Models;
using Senkadagala.ViewModels;

namespace Senkadagala.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class SettingController : Controller
    {
        readonly AppDbContext _context;
        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Settings.ToList());
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();
            Setting setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (setting is null) return NotFound();
            UpdateSettingVM vm = new UpdateSettingVM 
            {
                Key= setting.Key,
                Value = setting.Value,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,UpdateSettingVM vm)
        {
            if (id is null) return BadRequest();
            Setting setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (setting is null) return NotFound();
            setting.Key = vm.Key;
            setting.Value = vm.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
