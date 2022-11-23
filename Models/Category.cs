using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il titolo non può essere oltre i 50 caratteri")]
        public string Title { get; set; }
        public List<Pizza>? Pizzas { get; set; }
    }
}
