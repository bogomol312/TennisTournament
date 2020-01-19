using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TurniejTenisowy.Models
{
    public class Zawodnik
    {
        [Key]
        public int IdGosc { get; set; }

        [Required(ErrorMessage ="Imie jest wymagane")]
        [MaxLength(25)]
        public string Imie { get; set; }

        [Required(ErrorMessage ="Nazwisko jest wymagane")]
        [MaxLength(25)]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage ="DataUrodzenia jest wymagana")]
        [DataType(DataType.Date)]
        public DateTime DataUrodzenia { get; set; }

        [MaxLength(3)]
        public string Kraj { get; set; }

        public int Wiek { get; set; }

        [MaxLength(1)]
        public string Plec { get; set; }

        public int Trener { get; set; }

    }
}
