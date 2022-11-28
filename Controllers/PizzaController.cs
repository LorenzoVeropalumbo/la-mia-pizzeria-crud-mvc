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
        public PizzaDbContext db;
        IdbPizzeriaRepository pizzaRepository;

        public PizzaController(IdbPizzeriaRepository _pizzaRepository) : base()
        {
            db = new PizzaDbContext();

            pizzaRepository = _pizzaRepository;
        }
        public IActionResult Index()
        {
            List<Pizza> listOfPizzas = pizzaRepository.All();

            return View(listOfPizzas);
        }

        public IActionResult Detail(int id)
        {

            Pizza pizza = pizzaRepository.GetById(id);

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

            pizzaRepository.Create(formData.Pizza, formData.SelectedIngredients);

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
            Pizza pizzaItem = pizzaRepository.GetById(id);

            if (pizzaItem == null)
            {
                return NotFound();
            }


            pizzaRepository.Update(pizzaItem, formData.Pizza, formData.SelectedIngredients);

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

            pizzaRepository.Delete(pizza);


            return RedirectToAction("Index");
        }

    }


}
