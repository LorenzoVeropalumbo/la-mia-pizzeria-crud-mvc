using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models.FormData;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class CategoryController : Controller
    {
        public PizzaDbContext db = new PizzaDbContext();

        public IActionResult Index()
        {

            List<Category> listOfCategories = db.Categories.ToList();

            return View(listOfCategories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Category category = db.Categories.Where(c => c.Id == id).FirstOrDefault();

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Category Updatecategory)
        {

            if (!ModelState.IsValid)
            {
                return View(Updatecategory);
            }

            Category category = db.Categories.Where(c => c.Id == id).FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }

            category.Title = Updatecategory.Title;

            db.Categories.Update(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Category category = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            
            if (category == null)
            {
                return NotFound();
            }

            foreach (Pizza pizza in category.Pizzas)
            {
                pizza.CategoryId = 1;
            }
            db.Categories.Remove(category);
            db.SaveChanges();


            return RedirectToAction("Index");
        }


    }
}
