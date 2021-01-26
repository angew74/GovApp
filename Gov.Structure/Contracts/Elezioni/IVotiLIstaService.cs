using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Elezioni
{
   public interface IVotiListaService : IEntityService<VotiLista>
    {
       
        VotiLista findById(int id);
        List<VotiLista> findAllBy();
        List<VotiLista> findBySezioneNumerosezioneAndTipoelezioneId(int numerosezione, int tipoelezioneid);
        List<VotiLista> findByListaIdAndTipoelezioneId(int listaid, int tipoElezioneId);
        List<VotiLista> findByListaProgressivoAndTipoelezioneId(int progressivo, int tipoElezioneId);
        VotiLista findByListaIdAndSezioneNumerosezioneAndTipoelezioneId(int listaid, int numerosezione, int tipoElezioneId);
        VotiLista findByListaProgressivoAndSezioneNumerosezioneAndTipoelezioneId(int progressivo, int numerosezione, int tipoElezioneId);
        VotiLista findByListaDenominazioneAndSezioneNumerosezioneAndTipoelezioneId(String denominazione, int numerosezione, int tipoElezioneId);
        List<RicalcoloVotiLista> countListaByMunicipio(int tipoelezioneid, int municipio);
        CountResult countPervenuteByMunicipio(int tipoelezioneid, int municipio);
        CountResult countPervenute(int tipoelezioneid);
        List<RicalcoloVotiLista> countLista(int tipoelezioneid);
        List<RicalcoloVotiLista> countListaSingle(int tipoelezioneid, int idlista);
        List<RicalcoloVotiLista> countListaSingleMunicipio(int tipoelezioneid, int idlista);
        RicalcoloVotiLista countVotantiPervenute(int tipoelezioneid);
        RicalcoloVotiLista countVotantiPervenuteByMunicipio(int tipoelezioneid, int municipio);
        List<VotiLista> findBySezionePlessoIdAndTipoelezioneId(int plessoid, int tipoelezioneid);
        List<VotiLista> findByMunicipioAndTipoelezioneId(int municipio, int tipoelezioneid);


        List<VotiLista> findBySezioneNumerosezioneAndTipoelezioneIdAndListaCoalizioneId(int numerosezione, int tipoelezioneid, int coalizioneid);
    }
}
