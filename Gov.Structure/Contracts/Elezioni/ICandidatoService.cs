
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Elezioni
{
    public interface ICandidatoService : IEntityService<Candidati>
    {

        Candidati findById(int id);
        List<Candidati> findAllBy();
        List<Candidati> findByCognomeAndNomeAndTipoelezioneId(String cognome, String nome, int tipoelezioneid);
        Candidati findByProgressivoAndListaIdAndTipoelezioneId(int progressivo, int listaid, int tipoelezioneid);
        List<Candidati> findByListaIdAndTipoelezioneId(int lista, int tipoelezioneid);
    }
}
