
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
    public interface IMatriceService : IEntityService<Matrice>
    {
        public Matrice findMatriceById(int id);
        public Matrice findMatriceByMun(int mun);       
        public void delete(int id);
        public Matrice findByIdTipoElezione(int idtipoelezione);
        public Matrice findByTipoElezione(Tipoelezione tipoelezione);
    }

}
