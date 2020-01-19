using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurniejTenisowy.Models
{
    public class MeczDetails 
    {
        public int IdMecz { get; set; }
        public List<Zawodnik> Kibice { get; set; }
        public Sedzia Sedzia { get; set; }
    }
}
