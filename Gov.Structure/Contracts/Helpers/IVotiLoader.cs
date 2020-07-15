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
        public List<VotiListaModel> ConvertToJson(List<VotiLista> l, int sezione, string tipo);
        public List<VotiSindacoModel> ConvertToJsonSindaci(List<VotiSindaco> l, int sezione, string tipo,int idtipoelezione);
        List<VotiLista> prepareVoti(List<VotiListaModel> liste, string user, int tipoElezione);
        List<VotiLista> prepareVotiR(List<VotiListaModel> liste, string user);
        List<VotiSindaco> prepareVotiSindaco(List<VotiSindacoModel> list, string user, int tipoelezioneid);
        List<VotiSindaco> prepareVotiSindacoR(List<VotiSindacoModel> list,VotiGenerali vr, string user, int tipoelezioneid);
        VotiGenerali prepareVotiG(VotiModel form, string user, int tipoelezioneid);
        VotiGenerali prepareVotiGR(VotiModel form, string user, int tipoelezioneid);
        List<VotiLista> prepareVotiListaR(List<VotiSindacoModel> sindaci, int tipoelezioneid, string user, List<VotiListaModel> l);
        List<VotiLista> prepareVotiLista(List<VotiSindacoModel> sindaci, int tipoelezioneid, string user, List<VotiListaModel> l);
    }
}
