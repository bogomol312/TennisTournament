using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurniejTenisowy.Models
{
    public class KibicDetails
    {
        public int IdKibic { get; set; }

        public DateTime DataMeczu { get; set; }

        public string Zawodnik1 { get; set; }

        public string Zawodnik2 { get; set; }

        public int NumerMiejsca { get; set; }

    }
}
