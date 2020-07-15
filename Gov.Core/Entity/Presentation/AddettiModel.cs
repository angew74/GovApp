using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Entity.Presentation
{
    public class AddettiModel
    {
        public string NomeCognome{get; set;}
        public string UserName{ get; set; }
        public List<PlessoModel> Plessi { get; set; }
    }
}
