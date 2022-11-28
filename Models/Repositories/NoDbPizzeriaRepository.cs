using Azure;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class NoDbPizzeriaRepository : IdbPizzeriaRepository
    {
        public static List<Pizza> Pizzas = new List<Pizza>();

        public List<Pizza> All()
        {
            return Pizzas;
        }

        public void Create(Pizza pizza, List<int> selectedIngredients)
        {
            pizza.Id = Pizzas.Count;
            pizza.Category = new Category() { Id = 1, Title = "Fake cateogry" };

            pizza.Ingredients = new List<Ingredient>();

            IngredientOnPizza(pizza, selectedIngredients);

            Pizzas.Add(pizza);
        }

        private static void IngredientOnPizza(Pizza pizza, List<int> selectedIngredients)
        {
            pizza.Category = new Category() { Id = 1, Title = "Fake cateogry" };

            foreach (int IngredientId in selectedIngredients)
            {
                pizza.Ingredients.Add(new Ingredient() { Id = IngredientId, Title = "Fake ingredient " + IngredientId });
            }
        }

        public void Delete(Pizza pizza)
        {
            Pizzas.Remove(pizza);
        }

        public Pizza GetById(int id)
        {
            Pizza pizza = Pizzas.Where(p => p.Id == id).FirstOrDefault();

            pizza.Category = new Category() { Id = 1, Title = "Fake cateogry" };
            return pizza;
        }

        public void Update(Pizza pizza, Pizza formData, List<int>? selectedIngredients)
        {
            pizza = formData;
            pizza.Category = new Category() { Id = 1, Title = "Fake cateogry" };

            pizza.Ingredients = new List<Ingredient>();


            IngredientOnPizza(pizza, selectedIngredients);
        }
    }
}
