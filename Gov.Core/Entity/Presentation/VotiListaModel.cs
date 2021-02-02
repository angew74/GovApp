using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Entity.Presentation
{
    public class VotiListaModel
    {
        public int Id { get;  set; }
        public string Denominazione { get;  set; }
        public int? Progressivo { get;  set; }
         
        public string votiLista { get;  set; }

        public string perecentualeLista { get; set; }
        public string Tipo { get;  set; }
        public int numeroSezione { get;  set; }        
        public int idLista { get;  set; }       
        public int idSindaco{ get;  set; }
       
    }
}
