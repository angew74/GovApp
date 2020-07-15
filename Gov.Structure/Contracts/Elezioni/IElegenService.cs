
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Elezioni
{
    public interface IElegenService : IEntityService<Elegen>
    {

        public Elegen findById(int id);     
        public void delete(int id);
        public Elegen findByIdTipoElezione(int idtipoelezione);
        public Elegen findByTipoElezione(Tipoelezione tipoelezione);

    }

}
