using System.ComponentModel.DataAnnotations;

namespace ASPprojekt.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "ID Produktu")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Pole Nazwa Produktu jest wymagane.")]
        [Display(Name = "Nazwa Produktu")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole Cena Produktu jest wymagane.")]
        [Display(Name = "Cena")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
