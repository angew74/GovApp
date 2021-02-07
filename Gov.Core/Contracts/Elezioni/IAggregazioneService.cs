using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
    public interface IAggregazioneService : IEntityService<AggregazioneInterrogazioni>
    {

        AggregazioneInterrogazioni findById(int id);
        void deleteById(int id);
        List<AggregazioneInterrogazioni> FindAllBytipoElezioneId(int tipoElezioneId);
    }

}
