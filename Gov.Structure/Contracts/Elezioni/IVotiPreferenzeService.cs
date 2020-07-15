
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Elezioni
{
    public interface IVotiPreferenzeService : IEntityService<VotiPreferenze>
    {
     
        VotiPreferenze findById(int id);
        List<VotiPreferenze> findAllBy();
        List<VotiPreferenze> findBySezioneNumerosezioneAndTipoelezioneId(int numerosezione, int tipoelezioneid);
        List<VotiPreferenze> findByListaIdAndTipoelezioneId(int listaid, int tipoElezioneId);
        List<VotiPreferenze> findByListaProgressivoAndTipoelezioneId(int progressivo, int tipoElezioneId);
        List<VotiPreferenze> findByListaIdAndSezioneNumerosezioneAndTipoelezioneId(int listaid, int numerosezione, int tipoElezioneId);
        List<VotiPreferenze> findByListaProgressivoAndSezioneNumerosezioneAndTipoelezioneId(int progressivo, int numerosezione, int tipoElezioneId);
        List<VotiPreferenze> findByListaDenominazioneAndSezioneNumerosezioneAndTipoelezioneId(String denominazione, int numerosezione, int tipoElezioneId);
        List<VotiPreferenze> findByListaDenominazioneAndTipoelezioneId(String denominazione, int tipoElezioneId);
        CountResult countPervenuteByMunicipio(int tipoelezioneid, int municipio);
        CountResult countPervenute(int tipoelezioneid);       
        List<VotiPreferenze> findByCandidatoIdAndTipoelezioneId(int id, int tipoElezioneId);
        List<VotiPreferenze> findByCandidatoIdAndTipoelezioneIdAndSezioneId(int id, int tipoElezioneId, int sezioneId);
        List<VotiPreferenze> findByCandidatoIdAndTipoelezioneIdAndSezioneMunicipio(int id, int tipoElezioneId, int municipio);
    }
}
