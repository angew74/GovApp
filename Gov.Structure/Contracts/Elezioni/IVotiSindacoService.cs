
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Elezioni
{
   public interface IVotiSindacoService : IEntityService<VotiSindaco>
    {
       
        VotiSindaco findById(int id);
        List<VotiSindaco> findAllBy();
        List<VotiSindaco> findBySezioneNumerosezioneAndTipoelezioneId(int numerosezione, int tipoelezioneid);
        List<VotiSindaco> findBySindacoIdAndTipoelezioneId(int sindacoid, int tipoElezioneId);
        List<VotiSindaco> findBySindacoProgressivoAndTipoelezioneId(int progressivo, int tipoElezioneId);
        VotiSindaco findBySindacoIdAndSezioneNumerosezioneAndTipoelezioneId(int sindacoid, int numerosezione, int tipoElezioneId);
        VotiSindaco findBySindacoProgressivoAndSezioneNumerosezioneAndTipoelezioneId(int progressivo, int numerosezione, int tipoElezioneId);
        VotiSindaco findBySindacoNomeAndSindacoCognomeAndSezioneNumerosezioneAndTipoelezioneId(String nome, String cognome, int numerosezione, int tipoElezioneId);
        List<RicalcoloVotiSindaco> countSindacoByMunicipio(int tipoelezioneid, int municipio);
        CountResult countPervenuteByMunicipio(int tipoelezioneid, int municipio);
        CountResult countPervenute(int tipoelezioneid);
        List<RicalcoloVotiSindaco> countSindaco(int tipoelezioneid);
        RicalcoloVotiSindaco countSindacoSingle(int tipoelezioneid, int idlista);
        List<RicalcoloVotiSindaco> countSindacoSingleMunicipio(int tipoelezioneid, int idlista);
        List<RicalcoloVotiSindaco> countVotantiPervenute(int tipoelezioneid);
        List<RicalcoloVotiSindaco> countVotantiPervenuteByMunicipio(int tipoelezioneid, int municipio);
        List<VotiSindaco> findBySezionePlessoIdAndTipoelezioneId(int plessoid, int tipoelezioneid);
    }
}
