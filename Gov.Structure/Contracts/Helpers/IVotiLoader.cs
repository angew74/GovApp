﻿using Gov.Core.Entity.Elezioni;
using Gov.Core.Entity.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Helpers
{
    public interface IVotiLoader
    {
        //  public HashSet<VotiLista> prepareVoti(List<ListaSemplice> list)

        MunicipioModel ConvertToJsonLista(List<VotiLista> l, VotiGenerali votiGenerali);
        MunicipioModel ConvertToJsonSindacoRicalcolo(List<RicalcoloVotiSindaco> ricalcoloVotiSindacos, Voti voti);
        public List<VotiSindacoModel> ConvertToJsonSindaci(List<VotiSindaco> l, int sezione, string tipo,int idtipoelezione);
        List<VotiLista> prepareVoti(List<VotiListaModel> liste, string user, int tipoElezione);
        List<VotiLista> prepareVotiR(List<VotiListaModel> liste, string user);
        List<VotiSindaco> prepareVotiSindaco(List<VotiSindacoModel> list, string user, int tipoelezioneid);
        List<VotiSindaco> prepareVotiSindacoR(List<VotiSindacoModel> list,VotiGenerali vr, string user, int tipoelezioneid);
        VotiGenerali prepareVotiG(VotiModel form, int tipoelezioneid);
        VotiGenerali prepareVotiGR(VotiModel form, string user, int tipoelezioneid);
        List<VotiLista> prepareVotiListaR(List<VotiSindacoModel> sindaci, int tipoelezioneid, string user, List<VotiListaModel> l);
        List<VotiSindaco> prepareVoti(VotiModel form, int tipoelezioneid);
        List<VotiSindacoModel> ConvertToJsonSindaciEmpty(List<Sindaci> sindaci, string sezione, string tipo);
        MunicipioModel ConvertToJsonListeRicalcolo(List<RicalcoloVotiLista> ricalcoloVotiListas, Voti voti);
        List<RicalcoloVotiLista> ConvertToListeRicalcolo(DatiModel json, int tipoelezioneid,int totalesezioni, int tiporicalcolo);
        List<RicalcoloVotiSindaco> ConvertToSindacoRicalcolo(DatiModel json, int tipoelezioneid,int totalesezioni, int tiporicalcolo);
        MunicipioModel ConvertToJsonListaMunicipio(List<RicalcoloVotiLista> votis);
    }
}
