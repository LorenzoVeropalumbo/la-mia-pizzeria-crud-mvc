using Azure;
using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.FormData;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public PizzaDbContext db = new PizzaDbContext();
        IdbPizzeriaRepository DbPizzeriaRepository = new DbPizzeriaRepository();

        public IActionResult Index()
        {
            List<Pizza> listOfPizzas = DbPizzeriaRepository.All();

            return View(listOfPizzas);
        }

        public IActionResult Detail(int id)
        {

            Pizza pizza = DbPizzeriaRepository.GetById(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        public IActionResult Create()
        {
            FormData formData = new FormData();
            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();

            formData.Ingredients = new List<SelectListItem>();
            List<Ingredient> Ingredients = db.ingredients.ToList();

            foreach (Ingredient ingredient in Ingredients)
            {
                formData.Ingredients.Add(new SelectListItem(ingredient.Title, ingredient.Id.ToString()));
            }
            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FormData formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                
                formData.Ingredients = new List<SelectListItem>();
                List<Ingredient> ingredients = db.ingredients.ToList();

                foreach (Ingredient ingredient in ingredients)
                {
                    formData.Ingredients.Add(new SelectListItem(ingredient.Title, ingredient.Id.ToString()));
                }
                return View(formData);
            }

            DbPizzeriaRepository.Create(formData.Pizza, formData.SelectedIngredients);

            return RedirectToAction("Detail", new { Id = formData.Pizza.Id });
        }

        public IActionResult Update(int id)
        {
            Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).Include(i => i.Ingredients).FirstOrDefault();

            if (pizza == null)
                return NotFound();

            FormData formData = new FormData();
            formData.Pizza = pizza;
            formData.Categories = db.Categories.ToList();

            formData.Ingredients = new List<SelectListItem>();
            List<Ingredient> Ingredients = db.ingredients.ToList();

            foreach (Ingredient ingredient in Ingredients)
            {
                formData.Ingredients.Add(new SelectListItem(ingredient.Title, ingredient.Id.ToString(), pizza.Ingredients.Any(i => i.Id == ingredient.Id)));
            }

            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, FormData formData)
        {


            if (!ModelState.IsValid)
            {
                formData.Pizza.Id = id;
                //return View(postItem);
                formData.Categories = db.Categories.ToList();
                formData.Ingredients = new List<SelectListItem>();

                List<Ingredient> IngredientList = db.ingredients.ToList();

                foreach (Ingredient ingredient in IngredientList)
                {
                    formData.Ingredients.Add(new SelectListItem(ingredient.Title, ingredient.Id.ToString()));
                }

                return View(formData);
            }

            //update esplicito con nuovo oggetto
            Pizza pizzaItem = db.Pizzas.Where(pizza => pizza.Id == id).Include(i => i.Ingredients).FirstOrDefault();

            if (pizzaItem == null)
            {
                return NotFound();
            }


            pizzaItem.Title = formData.Pizza.Title;
            pizzaItem.Description = formData.Pizza.Description;
            pizzaItem.Image = formData.Pizza.Image;
            pizzaItem.Price = formData.Pizza.Price;
            pizzaItem.CategoryId = formData.Pizza.CategoryId;

            pizzaItem.Ingredients.Clear();

            if (formData.SelectedIngredients == null)
            {
                formData.SelectedIngredients = new List<int>();
            }

            foreach (int IngredientId in formData.SelectedIngredients)
            {
                Ingredient ingredient = db.ingredients.Where(t => t.Id == IngredientId).FirstOrDefault();
                pizzaItem.Ingredients.Add(ingredient);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizza == null)
            {
                return NotFound();
            }

            DbPizzeriaRepository.Delete(pizza);


            return RedirectToAction("Index");
        }

    }


}
