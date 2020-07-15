using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Entity.Presentation
{
    public class AffluenzaModel
    {
        public int NumeroSezione { get; set; }

        public int Cabina { get; set; }

        public string Tipo { get; set; }
        public int VotantiFemmine { get;  set; }
        public int VotantiMaschi { get;  set; }
        public int VotantiTotali { get;  set; }      
        public int IscrittiMaschi { get; set; }
        public int IscrittiFemmine { get; set; }     
        public int IscrittiTotali { get; set; }     

        public int VotantiMaschiAffP { get; set; }
    
        public int VotantiFemmineAffP { get; set; }      

        public int VotantiTotaliAffP { get; set; }
    }
}
