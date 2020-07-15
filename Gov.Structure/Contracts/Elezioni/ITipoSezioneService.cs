
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Elezioni
{
    public interface ITipoSezioneService : IEntityService<Tiposezione>
    {

        public Tiposezione findTipoSezioneById(int id);
        public Tiposezione findTipoSezioneByCodice(String codice);
        public List<Tiposezione> getAll();

    }
}
