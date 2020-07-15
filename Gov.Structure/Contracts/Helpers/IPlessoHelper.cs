using Gov.Core.Entity.Elezioni;
using Gov.Core.Entity.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Helpers
{
    public interface IPlessoHelper
    {
        public List<PlessoModel> Convert(List<Plessi> pp, string username, int userid);

        public List<PlessoModel> ConvertFromSezioni(List<Sezioni> pp, string username, int userid);
       
       
    }
}
