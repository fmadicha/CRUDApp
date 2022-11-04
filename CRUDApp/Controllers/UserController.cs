using CRUDApp.Data;
using CRUDApp.Models;
using CRUDApp.Models.ViewModels;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CRUDApp.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _db;
        public UserController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<User> user = _db.Users;
            return View(user);
            //return View(_db.Users.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        //to add record to db
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                 _db.Users.Add(user);

                _db.SaveChangesAsync();

                TempData["ResultOk"] = "User added Successfully";

                return RedirectToAction("Index");
            }


            return View(user);
        }

        public IActionResult Details(int? userId)
        {
            if(userId== null)
            {
                return NotFound();
            }

            var user = _db.Users.FirstOrDefault(u => u.UserTypeId == userId);
            if (userId == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //Edit
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var userfrmDb = _db.Users.Find(id);

            if (userfrmDb == null)
            {
                return NotFound();
            }
            return View(userfrmDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult  Edit(User user)
        {
            if (ModelState.IsValid)
            {
                 _db.Users.Update(user);
                _db.SaveChanges();
                TempData["ResultOk"] = "User updated Successfully";
                return RedirectToAction("Index");
            }
            return View(user);
        }


        public IActionResult Delete(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();

            }

            var userFromDb = _db.Users.Find(id);
            if(userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Display(Name="Delete")]
        public IActionResult DeleteUser(int? id)
        {
            var deleteUser = _db.Users.Find(id);
            if (deleteUser== null)
            {
                return NotFound();
            }

            _db.Users.Remove(deleteUser);
            _db.SaveChanges();
            TempData["ResultOk"] = "User is deleted successfully ! ";
            return RedirectToAction("Index");
        }

    }

}
