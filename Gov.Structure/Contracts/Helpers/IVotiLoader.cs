using Gov.Core.Entity.Elezioni;
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

        MunicipioModel ConvertToJsonListaSezione(List<VotiLista> l, VotiGenerali votiGenerali);
        MunicipioModel ConvertToJsonSindacoRicalcolo(List<RicalcoloVotiSindaco> ricalcoloVotiSindacos, Voti voti);
        public List<VotiSindacoModel> ConvertToJsonSindaci(List<VotiSindaco> l, int sezione, string tipo,int idtipoelezione);
       
        List<VotiSindaco> prepareVoti(VotiModel form, int tipoelezioneid);
        List<VotiSindacoModel> ConvertToJsonSindaciEmpty(List<Sindaci> sindaci, string sezione, string tipo);
        MunicipioModel ConvertToJsonListeRicalcolo(List<RicalcoloVotiLista> ricalcoloVotiListas, Voti voti);
        List<RicalcoloVotiLista> ConvertToListeRicalcolo(DatiModel json, int tipoelezioneid,int totalesezioni, int tiporicalcolo);
        List<RicalcoloVotiSindaco> ConvertToSindacoRicalcolo(DatiModel json, int tipoelezioneid,int totalesezioni, int tiporicalcolo);
        MunicipioModel ConvertToJsonListaMunicipio(List<RicalcoloVotiLista> votis);
        MunicipioModel ConvertToJsonLista(List<RicalcoloVotiLista> votis);
        MunicipioModel ConvertToJsonSindacoMunicipio(List<RicalcoloVotiSindaco> votis);
        MunicipioModel ConvertToJsonSindaco(List<RicalcoloVotiSindaco> votis);
        MunicipioModel ConvertToJsonSindacoSezione(List<VotiSindaco> votis, VotiGenerali votiGenerali);
    }
}
