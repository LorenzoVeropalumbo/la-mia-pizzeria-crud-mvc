using Azure;
using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models.FormData;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbPizzeriaRepository : IdbPizzeriaRepository
    {
        private PizzaDbContext db;

        public DbPizzeriaRepository()
        {
            db = new PizzaDbContext();
        }
        public List<Pizza> All()
        {
            return db.Pizzas.Include(p => p.Category).Include(p => p.Ingredients).ToList();
        }

        public void Create(Pizza pizza, List<int> selectedIngredients)
        {
            pizza.Ingredients = new List<Ingredient>();

            foreach (int ingredientID in selectedIngredients)
            {
                Ingredient ingredient = db.ingredients.Where(i => i.Id == ingredientID).FirstOrDefault();
                pizza.Ingredients.Add(ingredient);
            }

            db.Pizzas.Add(pizza);
            db.SaveChanges();
        }

        public void Delete(Pizza pizza)
        {
            db.Pizzas.Remove(pizza);
            db.SaveChanges();
        }

        public Pizza GetById(int id)
        {
            return db.Pizzas.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();
        }

        public void Update(Pizza pizza, Pizza formData, List<int>? selectedIngredients)
        {
            if (selectedIngredients == null)
            {
                selectedIngredients = new List<int>();
            }


            pizza.Title = formData.Title;
            pizza.Description = formData.Description;
            pizza.Image = formData.Image;
            pizza.CategoryId = formData.CategoryId;
            pizza.Price = formData.Price;

            pizza.Ingredients.Clear();


            foreach (int ingredientId in selectedIngredients)
            {
                Ingredient ingredient = db.ingredients.Where(i => i.Id == ingredientId).FirstOrDefault();
                pizza.Ingredients.Add(ingredient);
            }

            db.SaveChanges();
        }

    }
}
