
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
    public interface IMunicipioService : IEntityService<Municipi>
    {

        Municipi findById(int id);
       
        void delete(int id);
        Municipi findBymunicipio(int id);
        List<Municipi> getAll();
    }
}
