
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
   public interface IAffluenzaService : IEntityService<Affluenze>
    {
      
        void delete(long id);

        Affluenze findById(int id);


        List<Affluenze> findByTipoelezioneId(int idTipoElezione);
        Affluenze findBySezioneNumerosezioneAndTipoelezioneId(int sezione, int tipoElezioneId);

        Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza1(int sezione, int tipoElezioneId, int a);
        Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza2(int sezione, int tipoElezioneId, int a);
        Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza3(int sezione, int tipoElezioneId, int a);

        Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza4(int sezione, int tipoElezioneId, int a);
        Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza5(int sezione, int tipoElezioneId, int a);
        Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndApertura1(int sezione, int tipoElezioneId, int a);
        Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndCostituzione1(int sezione, int tipoElezioneId, int a);

        // ricerca per plesso
        List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndAffluenza1(int plessoid, int tipoElezioneId, int a);
        List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndAffluenza2(int plessoid, int tipoElezioneId, int a);
        List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndAffluenza3(int plessoid, int tipoElezioneId, int a);
        List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndAffluenza4(int plessoid, int tipoElezioneId, int a);
        List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndAffluenza5(int plessoid, int tipoElezioneId, int a);
        List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndCostituzione1(int plessoid, int tipoElezioneId, int a);
        List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndApertura1(int plessoid, int tipoElezioneId, int a);

        /* count totali */
        /* interrogazioni per sezione */
        List<Affluenze> findByAffluenza1AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione);

        List<Affluenze> findByAffluenza2AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione);

        List<Affluenze> findByAffluenza3AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione);

        List<Affluenze> findByCostituzione1AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione);

        List<Affluenze> findByCostituzione2AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione);

        List<Affluenze> findByApertura1AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione);

        List<Affluenze> findByApertura2AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione);

        /* interrogazione per Municipio*/
        List<Affluenze> findByAffluenza1AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio);

        List<Affluenze> findByAffluenza2AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio);

        List<Affluenze> findByAffluenza3AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio);

        List<Affluenze> findByCostituzione1AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio);

        List<Affluenze> findByCostituzione2AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio);

        List<Affluenze> findByApertura1AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio);

        List<Affluenze> findByApertura2AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio);

        List<CountResult> countByAffluenza1AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione);
        List<CountResult> countByAffluenza2AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione);
        List<CountResult> countByAffluenza3AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione);
        List<CountResult> countByApertura1AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione);
        List<CountResult> countByApertura2AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione);
        List<CountResult> countByCostituzione1AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione);
        List<CountResult> countByCostituzione2AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione);
        /* count per sola tipologia */
        List<CountResult> countByAffluenza1AndTipoelezioneId(int a, int tipoElezione);
        List<CountResult> countByAffluenza2AndTipoelezioneId(int a, int tipoElezione);
        List<CountResult> countByAffluenza3AndTipoelezioneId(int a, int tipoElezione);
        List<CountResult> countByApertura1AndTipoelezioneId(int a, int tipoElezione);
        List<CountResult> countByApertura2AndTipoelezioneId(int a, int tipoElezione);
        List<CountResult> countByCostituzione1AndTipoelezioneId(int a, int tipoElezione);
        List<CountResult> countByCostituzione2AndTipoelezioneId(int a, int tipoElezione);
        List<RicalcoliAffluenza> countAffluenza1(int tipoelezioneid);
        List<RicalcoliAffluenza> countAffluenza1ByMunicipio(int tipoelezioneid);
        List<RicalcoliAffluenza> countAffluenza2(int tipoelezioneid);
        List<RicalcoliAffluenza> countAffluenza2ByMunicipio(int tipoelezioneid);
        List<RicalcoliAffluenza> countAffluenza3(int tipoelezioneid);
        List<RicalcoliAffluenza> countAffluenza3ByMunicipio(int tipoelezioneid);
        List<RicalcoloAperturaCostituzione> countApertura1(int tipoelezioneid);
        List<RicalcoloAperturaCostituzione> countApertura2(int tipoelezioneid);
        List<RicalcoloAperturaCostituzione> countCostituzione1(int tipoelezioneid);
        List<RicalcoloAperturaCostituzione> countCostituzione2(int tipoelezioneid);
        List<RicalcoloAperturaCostituzione> countApertura1ByMunicipio(int tipoelezioneid);
        List<RicalcoloAperturaCostituzione> countApertura2ByMunicipio(int tipoelezioneid);
        List<RicalcoloAperturaCostituzione> countCostituzione1ByMunicipio(int tipoelezioneid);
        List<RicalcoloAperturaCostituzione> countCostituzione2ByMunicipio(int tipoelezioneid);
    }
}
