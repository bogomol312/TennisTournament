using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurniejTenisowy.Models
{
    public class ZawodnikDetails
    {
        public DateTime Data{ get; set; }

        public string Zawodnik1 { get; set; }

        public string Zawodnik2{ get; set; }

        public int Wynik1 { get; set; }

        public int Wynik2 { get; set; }

        public string Sedzia{ get; set; }

    }
}
