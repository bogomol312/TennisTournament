using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurniejTenisowy.Models
{
    public class Sedzia
    {
        public int IdSedzia { get; set; }

        public string Imie { get; set; }

        public string Nazwisko{ get; set; }

        public DateTime DataUrodzenia { get; set; }
    }
}
