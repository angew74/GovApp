
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
    public interface IListaService : IEntityService<Liste>
    {

        Liste findById(int id);
        List<Liste> findAllBy();
        Liste findByDenominazioneAndTipoelezioneId(String denominazione, int tipoelezioneid);
        Liste findByProgressivoAndTipoelezioneId(int progressivo, int tipoelezioneid);
        List<Liste> findAllByTipoelezioneId(int tipoElezione);
        List<Liste> findByCoalizioneIdAndTipoelezioneId(int idcoalizione, int tipoelezioneid);
        List<Liste> findByCoalizioneDenominazioneAndTipoelezioneId(String denominazione, int tipoelezioneid);
        List<Liste> findByCoalizioneSindacoIdAndTipoelezioneId(int sindacoid, int tipoelezioneid);
        List<Liste> findBySindacoIdAndTipoelezioneId(int sindacoid, int tipoelezioneid);
        List<Liste> findBySindacoNomeAndSindacoCognomeAndTipoelezioneId(String nome, String cognome, int tipoelezioneid);
        List<Liste> findBySindacoId(int sindacoid);
    }

}
