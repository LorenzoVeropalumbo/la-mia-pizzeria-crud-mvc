using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models.FormData
{
    public class FormData
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? Ingredients { get; set; }
        public List<int>? SelectedIngredients { get; set; }
    }
}
