using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Elezioni
{
    public interface IPlessoService : IEntityService<Plessi>
    {
        Plessi findById(uint id);
        void deleteById(uint id);
        List<Plessi> findByTipoelezioneId(int idtipoelezione);
        List<Plessi> findByTipoelezione(Tipoelezione tipoelezione);

        List<Plessi> findByMunicipio(int mun);
        List<Plessi> findByTipoelezioneIdAndDescrizioneLike(int tipoElezioneId, String descrizione);
        List<Plessi> findByTipoelezioneIdAndUbicazioneLike(int tipoElezioneId, String ubicazione);
    }
}
