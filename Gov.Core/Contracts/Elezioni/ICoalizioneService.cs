
using Gov.Core;
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
    public interface IRaggruppamentoService : IEntityService<Raggruppamento>
    {

        Raggruppamento findById(int id);
        List<Raggruppamento> findAllBy();
        Raggruppamento findByDenominazioneAndTipoelezioneId(String denominazione, int tipoelezioneid);
        Raggruppamento findBySindacoIdAndTipoelezioneId(int sindacoid, int tipoelezioneid);
        Raggruppamento findBySindacoNomeAndSindacoCognomeAndTipoelezioneId(String nome, String cognome, int tipoelezioneid);
        List<Raggruppamento> findAllByTipoelezioneId(int tipoElezione);
    }
}
