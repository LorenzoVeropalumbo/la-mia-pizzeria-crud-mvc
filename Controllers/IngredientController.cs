using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class IngredientController : Controller
    {
        public PizzaDbContext db = new PizzaDbContext();

        public IActionResult Index()
        {

            List<Ingredient> listOfIngredients = db.ingredients.ToList();

            return View(listOfIngredients);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            db.ingredients.Add(ingredient);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Ingredient ingredient = db.ingredients.Where(i => i.Id == id).FirstOrDefault();

            if (ingredient == null)
                return NotFound();

            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Ingredient Updateingredient)
        {

            if (!ModelState.IsValid)
            {
                return View(Updateingredient);
            }

            Ingredient ingredient = db.ingredients.Where(i => i.Id == id).FirstOrDefault();

            if (ingredient == null)
            {
                return NotFound();
            }

            ingredient.Title = Updateingredient.Title;

            db.ingredients.Update(ingredient);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Ingredient ingredient = db.ingredients.Where(i => i.Id == id).FirstOrDefault();

            if (ingredient == null)
            {
                return NotFound();
            }

            db.ingredients.Remove(ingredient);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}
