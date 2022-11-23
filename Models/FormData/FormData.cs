using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models.FormData
{
    public class FormData
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
