using System;
using System.ComponentModel.DataAnnotations;

namespace ASPprojekt.Models
{
    public class ProductionOrder
    {
        [Key]
        [Display(Name = "ID Zlecenia")]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Pole ID Produktu jest wymagane.")]
        [Display(Name = "ID Produktu")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Pole Planowana Data Rozpoczęcia jest wymagane.")]
        [Display(Name = "Data rozpoczęcia")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Pole Planowana Data Zakończenia jest wymagane.")]
        [Display(Name = "Data zakończenia")]
        public DateTime EndDate { get; set; }
    }
}
