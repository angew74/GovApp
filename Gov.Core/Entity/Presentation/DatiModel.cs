using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Core.Entity.Presentation
{
    public class DatiModel
    {
        public string Bianche { get; set; }
        public string Totale { get; set; }
        public string Contestate { get; set; }
        public string soloSindaco { get; set; }
        public string totaleValide { get; set; }
        public string Nulle { get; set; }
        public string Votanti { get; set; }
        public int Id { get; set; }
        public string valideListe { get; set; }
        public List<VotiSindacoModel> Sindaci { get; set; }
        public List<VotiListaModel> Liste { get; set; }
        public string Iscritti { get; set; }
        public string Tipo { get; set; }
        public string NumeroSezione { get; set; }
        public string municipio { get; set; }
        public string SezioniPervenute { get; set; }
    }
}
