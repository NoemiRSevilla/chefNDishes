using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using chefNDishes.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

// Other using statements
namespace HomeController.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult ShowAllChefs()
        {
            List<Chef> ChefWithDishAssociation = dbContext.Chefs
            .Include(c => c.CreatedDishes)
            .ToList();
            return View(ChefWithDishAssociation);
        }

        [HttpGet]
        [Route("chef/new")]
        public IActionResult NewChef()
        {
            return View("NewChefForm");
        }

        [HttpPost("chef/submit")]
        public IActionResult SubmitChef(Chef newChef)
        {
            if (ModelState.IsValid)
            {
                DateTime today = DateTime.Today;
                int years = today.Year - newChef.DOB.Year;
                if (years > 18)
                {
                    dbContext.Add(newChef);
                    dbContext.SaveChanges();
                    return RedirectToAction("ShowAllChefs", newChef);
                }
                else
                {
                    ModelState.AddModelError("DOB", "Chef has to be over 18 to register");
                    return View("NewChefForm");
                }
            }
            else
            {
                TempData["First Name"] = newChef.FirstName;
                TempData["Last Name"] = newChef.LastName;
                return View("NewChefForm");
            }
        }
        [HttpGet]
        [Route("dishes")]
        public IActionResult ShowAllDishes()
        {
            List<Dish> CreatedDishesWithChef = dbContext.Dishes
            // populates each Message with its related User object (Creator)
            .Include(c => c.CreatedBy)
            .ToList();
            return View(CreatedDishesWithChef);
        }

        [HttpGet("dish/new")]
        public IActionResult NewDish()
        {
            List<Chef> AllChefs = dbContext.Chefs.ToList();
            ViewBag.allChefs = AllChefs;
            return View("NewDishForm");
        }

        [HttpPost("dish/submit")]
        public IActionResult SubmitDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return Redirect("/dishes");
            }
            else
            {
                return View("NewDishForm");
            }
        }
    }
}