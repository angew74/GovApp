using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Entity.Presentation
{
    public class VotiSindacoModel
    {
      

        public string SoloSindaco { get; set; }
        public string NumeroSezione { get; set; }
        public string Id { get;  set; }
        public string Cognome { get;  set; }
        public string Progressivo { get;  set; }
        public string totaleSindaco { get; set; }
        public string percentualeTotale { get; set; }
        public string Bianche { get; set; }
        public string IdCoalizione { get; set; }     
      
     //    public string Voti { get;  set; }
        public string IsCoalizione { get;  set; }
        public string Nome { get;  set; }
        public string Tipo { get;  set; }
        public string IdSindaco { get; set; }

        public List<VotiListaModel> Liste { get; set; }
        public string soloListe { get; set; }
    }
}
