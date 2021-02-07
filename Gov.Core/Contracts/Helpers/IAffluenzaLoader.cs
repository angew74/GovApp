
using Gov.Core.Entity.Elezioni;
using Gov.Core.Entity.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Helpers
{
    public interface IAffluenzaLoader
    {
        public RicalcoliAffluenza affluenzaSplit(Affluenze a, String tipoInterrogazione);

        public string calculatePercentage(double obtained, double total);
        public RicalcoloAperturaCostituzione costituzioneSplit(Affluenze a, String tipoInterrogazione);

        public AffluenzaModel convertToJson(Affluenze a, Iscritti i, String tipo);
    }


}
