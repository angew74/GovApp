using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Entity.Presentation
{
    public class PlessoModel
    {
       

        public PlessoModel()
        {
        }

        public PlessoModel(int id, string descrizione)
        {
            Id = id;
            Descrizione = descrizione;
        }

        public PlessoModel(int id, string descrizione, string ubicazione)
        {
            Id = id;
            Descrizione = descrizione;
            Ubicazione = ubicazione;
        }
        public int Id { get; set; }

        public string Sezione { get; set; }
        public string Tipo { get; set; }
        public string Municipio { get; set; }

        public string Cabina { get; set; }

        public string Descrizione { get; set; }

        public string Ubicazione { get; set; }      
        public int Numero { get; set; }
        public string TipoRicerca { get; set; }
        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
