using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPprojekt.Models
{
    public class AssignmentOrder
    {
        [Key]
        public int AssignmentOrdersID { get; set; }

        [Required(ErrorMessage = "Pole ID Zlecenia jest wymagane.")]
        [Display(Name = "ID Zlecenia")]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Pole ID Maszyny jest wymagane.")]
        [Display(Name = "ID Maszyny")]
        public int MachineID { get; set; }

        [Required(ErrorMessage = "Pole ID Pracownika jest wymagane.")]
        [Display(Name = "ID Pracownika")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Pole Data Rozpoczęcia jest wwymagane.")]
        [Display(Name = "Data Rozpoczęcia")]
        public DateTime AssignmentDate { get; set; }
    }
}
