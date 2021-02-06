using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Structure.Contracts.Elezioni
{
    public interface IVotiSindacoStoricoService : IEntityService<VotiSindacoStorico>
    {
        VotiSindacoStorico findById(int id);
        List<VotiSindacoStorico> findBySezioneNumerosezioneAndTipoelezioneId(int numerosezione, int tipoelezioneid);
        VotiSindacoStorico findBySindacoIdAndSezioneNumerosezioneAndTipoelezioneId(int sindacoid, int numerosezione, int tipoElezioneId);
        List<VotiSindacoStorico> findBySindacoIdAndTipoelezioneId(int sindacoid, int tipoElezioneId);
    }
}
