using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TurniejTenisowy.Models
{
    public class Mecz
    {
        [Key]
        public int IdMecz { get; set; }

        [Required(ErrorMessage ="Prosze wprowadzic date")]
        [DataType(DataType.Date)]
        public DateTime Data{ get; set; }

        public int Zawodnik1{ get; set; }

        public int Zawodnik2 { get; set; }

        public int Wynik1 { get; set; }

        public int Wynik2 { get; set; }

        public int Sedzia { get; set; }

        public string informacja { get; set; }
    }
}
