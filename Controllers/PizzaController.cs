using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.FormData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public PizzaDbContext db = new PizzaDbContext();

        public IActionResult Index()
        {          

            List<Pizza> listOfPizzas = db.Pizzas.ToList();

            return View(listOfPizzas);
        }

        public IActionResult Detail(int id)
        {

            Pizza pizza = db.Pizzas.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefault();

            return View(pizza);
        }

        public IActionResult Create()
        {
            FormData formData = new FormData();
            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();


            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FormData formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                return View(formData);
            }

            db.Pizzas.Add(formData.Pizza);
            db.SaveChanges();

            return RedirectToAction("Detail", new { Id = formData.Pizza.Id });
        }

        public IActionResult Update(int id)
        {
            Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizza == null)
                return NotFound();

            FormData formData = new FormData();
            formData.Pizza = pizza;
            formData.Categories = db.Categories.ToList();

            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, FormData formData)
        {

            formData.Pizza.Id = id;

            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                return View(formData);
            }
          
            db.Pizzas.Update(formData.Pizza);
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

            db.Pizzas.Remove(pizza);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

    }


}
