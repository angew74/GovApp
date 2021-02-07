
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
    public interface ISezioneService : IEntityService<Sezioni>
    {

        Sezioni findById(int id);
        Sezioni findByNumerosezioneAndTipoelezioneId(int numeroSezione, int tipoElezioneId);
        List<Sezioni> findAllBy();
        List<CountResult> countAllByTipoelezioneIdAndTipoelezioneIdIn(int tipoElezione);
        List<CountResult> countAllByTipoelezioneIdAndMunicipioAndTipoelezioneIdIn(int tipoElezioneId, int municipio);
        Sezioni findByNumerosezioneAndCabinaAndTipoelezioneId(int numeroSezione, int cabina, int tipoElezioneId);
        List<Sezioni> findByPlessoIdAndTipoelezioneId(int plessoid, int tipoElezioneId);
    }
}
