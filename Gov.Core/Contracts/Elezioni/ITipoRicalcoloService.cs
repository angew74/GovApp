using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
    public interface ITipoRicalcoloService : IEntityService<TipoRicalcolo>
    {

        TipoRicalcolo findById(int id);
        void deleteById(int id);
        List<TipoRicalcolo> FindAll();
        List<TipoRicalcolo> findAllByTipoelezioneId(int tipoElezioneId);
        List<TipoRicalcolo> findAllByTipoelezioneIdAndCodiceFase(int tipoElezioneId, String codice);
        List<TipoRicalcolo> findAllByTipoelezioneIdAndCodice(int tipoElezioneId, String codice);
        List<TipoRicalcolo> findByCodicefase(String codice);
        List<TipoRicalcolo> findByCodice(String codice);
    }

}
