using System.ComponentModel.DataAnnotations;

namespace ASPprojekt.Models
{
    public class Machine
    {
        [Key]
        public int MachineID { get; set; }

        [Required(ErrorMessage = "Pole Nazwa Maszyny jest wymagane.")]
        [Display(Name = "Nazwa Maszyny")]
        public string MachineName { get; set; }

        [Required(ErrorMessage = "Pole Opis Maszyny jest wymagane.")]
        [Display(Name = "Opis Maszyny")]
        public string Description { get; set; }
    }
}
