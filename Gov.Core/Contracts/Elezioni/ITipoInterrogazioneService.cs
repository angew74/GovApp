using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
    public interface ITipoInterrogazioneService : IEntityService<TipoInterrogazione>
    {
        TipoInterrogazione findById(int id);
        void deleteById(int id);
        List<TipoInterrogazione> FindAll();
        List<TipoInterrogazione> findAllByTipoelezioneId(int tipoElezioneId);
        List<TipoInterrogazione> findAllByTipoelezioneIdAndCodiceFase(int tipoElezioneId, String codice);
        List<TipoInterrogazione> findAllByTipoelezioneIdAndCodice(int tipoElezioneId, String codice);
        List<TipoInterrogazione> findByCodicefase(String codice);
        List<TipoInterrogazione> findByCodice(String codice);

    }

}
