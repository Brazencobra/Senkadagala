using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Senkadagala.DAL;
using Senkadagala.Models;
using Senkadagala.Utilies.Extension;
using Senkadagala.ViewModels;
using System.Net.NetworkInformation;

namespace Senkadagala.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class InformationController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public InformationController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Information.ToList()); ;
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateInformationVM vm)
        {
            if(!ModelState.IsValid) return View();
            Information information = new Information
            {
                Name = vm.Name,
                Description = vm.Description,
                ImageUrl = vm.Photo.SaveFile(Path.Combine(_env.WebRootPath,"assets","images"))
            };
            await _context.Information.AddAsync(information);
            await _context.SaveChangesAsync();
            return  RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            Information information = await _context.Information.FirstOrDefaultAsync(i=>i.Id == id);
            if(information is null) return NotFound();
            _context.Information.Remove(information);
            information.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();
            Information information = await _context.Information.FirstOrDefaultAsync(i => i.Id == id);
            if (information is null) return NotFound();
            UpdateInformationVM uv = new UpdateInformationVM 
            {
                Name = information.Name,
                Description = information.Description,
                ImageUrl = information.ImageUrl
            };
            return View(uv);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,UpdateInformationVM vm)
        {
            if (id is null) return BadRequest();
            Information information = await _context.Information.FirstOrDefaultAsync(i => i.Id == id);
            if (information is null) return NotFound();
            if (!ModelState.IsValid) return View();
            information.Name = vm.Name;
            information.Description = vm.Description;
            if (vm.Photo != null)
            {
                information.ImageUrl.DeleteFile(_env.WebRootPath,"assets/images");
                information.ImageUrl = vm.Photo.SaveFile(Path.Combine(_env.WebRootPath,"assets","images"));
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
