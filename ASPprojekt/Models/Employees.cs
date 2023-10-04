using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPprojekt.Models
{
    public class Employee
    {
        [Key]
        [Display(Name = "ID Pracownika")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Pole Imie Pracownika jest wymagane.")]
        [Display(Name = "Imie")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole Nazwisko Pracownika jest wymagane.")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pole Email Pracownika jest wymagane.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Stanowisko jest wymagane.")]
        [Display(Name = "Stanowisko")]
        public string Position { get; set; }

    }
}
