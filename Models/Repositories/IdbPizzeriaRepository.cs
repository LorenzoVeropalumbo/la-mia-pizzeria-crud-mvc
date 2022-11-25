using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public interface IdbPizzeriaRepository
    {
        List<Pizza> All();
        void Create(Pizza pizza, List<int> selectedIngredients);
        void Delete(Pizza pizza);
        Pizza GetById(int id);
        void Update(Pizza pizza, Pizza formData, List<int>? selectedIngredients);
    }
}
