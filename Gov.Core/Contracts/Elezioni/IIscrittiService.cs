﻿
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
    public interface IIscrittiService :IEntityService<Iscritti>
    {

        Iscritti findIscrittiById(int id);

        List<Iscritti> countIscrittiMunicipioPervenuteByMun(int tipoelezioneid, int municipio);
        Iscritti findBySezioneIdAndTipoElezioneId(int idsezione, int tipoElezioneId);

        Iscritti findByTipoelezioneIdAndSezioneNumerosezione(int tipoElezioneId, int numerosezione);

        List<CountResult> countAllByTipoelezioneIdAndMunicipio(int mun, int TipoElezioneId);
        List<CountResult> countAllByTipoelezioneId(int TipoElezioneId);

        void delete(int id);
       
        List<Iscritti> countIscrittiSezioniPervenuteMunicipioAffluenza1(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteAllAffluenza1(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteMunicipioAffluenza2(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteAllAffluenza2(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteMunicipioAffluenza3(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteAllAffluenza3(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteMunicipioApertura1(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteAllApertura1(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteMunicipioApertura2(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteAllApertura2(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteMunicipioCostituzione1(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteAllCostituzione1(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteMunicipioCostituzione2(int tipoelezioneid);


        List<Iscritti> countIscrittiSezioniPervenuteAllCostituzione2(int tipoelezioneid);

        List<Iscritti> countIscrittiPervenute(int tipoelezioneid);

        List<Iscritti> countIscrittiPervenuteByMun(int tipoelezioneid);

    }

}
