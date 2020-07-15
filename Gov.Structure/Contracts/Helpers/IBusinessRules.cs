using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Helpers
{
    public interface IBusinessRules
    {
        public string IsInsertable(int sezione, string codiceFase, int cabina, int idtipoelezione);
        public bool IsEnabled(string codiceFase, int idtipoelezione);
        public string getTitoloByFase(String codice, String tipo);
      
    }
}
