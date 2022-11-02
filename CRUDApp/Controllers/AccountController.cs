using CRUDApp.Models;
using CRUDApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Register()
        {
            //checking if role exist
            if(!_roleManager.RoleExistsAsync(Utility.Helper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.Doctor));
                await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.Patient));
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Register(RegisterViewModel model)
        {
            //Server side validation
            if (ModelState.IsValid)
            { 
                //if isvalid create user
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name= model.Name
            };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    //since acc is created automatically sign in the new user
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
        }
            return View();
        }
    }
}
