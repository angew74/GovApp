using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Core.Entity.Presentation
{
    public class AbilitazioniModel
    {
      public  string codice { get; set; }
      public string descrizione { get; set; }
      public string categoria { get; set; }
      public string id { get; set; }

        public string _rowVariant { get { return this.abilitata == false ? "danger" : "light"; } }
        public bool abilitata { get; set; }
    }
}
