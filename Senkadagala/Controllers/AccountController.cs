using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Senkadagala.Models;
using Senkadagala.ViewModels.User;

namespace Senkadagala.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _signInManager= signInManager;
            _userManager= userManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserVM uv)
        {
            AppUser user =await  _userManager.FindByEmailAsync(uv.UsernameOrEmail);
            if (user is null)
            {
                user =await  _userManager.FindByNameAsync(uv.UsernameOrEmail);
                if (user is null)
                {
                    ModelState.AddModelError("","Username,email or password invalid");
                    return View();
                }
            }
            var result =await  _signInManager.PasswordSignInAsync(user, uv.Password,uv.IsPersistance,true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username,email or password invalid");
                return View();
            }
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new AppUser
            {
                Name= registerVM.Name,
                Surname=registerVM.Surname,
                Email=registerVM.Email,
                UserName = registerVM.Name,
            };
            var result = await _userManager.CreateAsync(user,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
