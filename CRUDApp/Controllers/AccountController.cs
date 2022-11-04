using CRUDApp.Models;
using CRUDApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly Models.CRUDApp _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(Models.CRUDApp db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index" ,"Home");
                }
                ModelState.AddModelError("", "Invalid Login attempt");
            }
            return View(model);
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

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //since acc is created automatically sign in the new user
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
        }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
