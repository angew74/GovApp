using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
    public interface ISindacoService : IEntityService<Sindaci>
    {
        Sindaci findById(int id);
        List<Sindaci> findAllBy();
        Sindaci findByNomeAndCognomeAndTipoelezioneId(String nome, String cognome, int tipoelezioneid);
        Sindaci findByProgressivoAndTipoelezioneId(int progressivo, int tipoelezioneid);
        List<Sindaci> findAllByTipoelezioneId(int tipoElezione);
    }

}
