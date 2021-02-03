using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.Models
{
    public class Research
    {
        public Research()
        {

        }
        public string sezione { get; set; }
        public string cabina { get; set; }
        public string tipo { get; set; }
        public string municipio { get; set; }
        public string idlista { get; set; }

        public string idsindaco { get; set; }
        public string tipoInterrogazione { get; set; }
    }
}
