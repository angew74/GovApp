
using Gov.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Entity.Presentation
{
    public class RisultatoModel
    {
       
        public int Sezione { get; set; }

        public int NumeroSezione { get; set; }

        public int Cabina { get; set; }

        public string Tipo { get; set; }

        public string DescrizioneSezione { get; set; }

        public string Descrizione { get; set; }

        public string Municipio { get; set; }

        public AffluenzaModel Affluenza { get; set; }
        public ApplicationUser User { get; set; }
        public string DescrizioneElezione { get; set; }
        public List<VotiListaModel> VotiLista { get; set; }
    }
}
