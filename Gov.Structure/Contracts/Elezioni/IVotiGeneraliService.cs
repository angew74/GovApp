using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Elezioni
{
    public interface IVotiGeneraliService : IEntityService<VotiGenerali>
    {


        Voti countGenerale(int tipoelezioneid);
        Voti countGeneraleByMunicipio(int tipoelezioneid);
        Voti countGeneraleOverMunicipio(int tipoelezioneid, int municipio);
        VotiGenerali findById(int id);
        List<VotiGenerali> findAllBy();
        VotiGenerali findBySezioneNumerosezioneAndTipoelezioneId(int numerosezione, int tipoelezioneid);
        List<VotiGenerali> findBySezionePlessoIdAndTipoelezioneId(int plessoid, int tipoelezioneid);
    
    }
}
