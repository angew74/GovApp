using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
   public interface IAbilitazioniService :IEntityService<FaseElezione>
    {

        FaseElezione findFaseElezioneById(int id);
        FaseElezione findFaseElezioneByCodice(String codice);
        List<FaseElezione> getAll();       
        FaseElezione findByCodiceAndTipoelezioneId(String codice, int tipoElezioneId);
        List<FaseElezione> findAllByTipoelezioneId(int tipoelezioneid,int skip,int take);
        List<FaseElezione> findByAbilitataAndTipoelezioneId(int abil, int tipoelezioneid, int skip, int take);
        List<FaseElezione> findByAbilitataAndTipoelezioneId(int abilitata, int tipoelezioneid);
        List<FaseElezione> findByCategoria(string categoria, int tipoelezioneid, int skip, int take);
        List<FaseElezione> findByDescrizioneLike(string descrizione, int tipoelezioneid, int skip, int take);
        int Count(int tipoelezioniid);
        List<FaseElezione> getRightsSortingBy(int tipoelezione, int skip, int take, string sortBy, bool sortDesc, string filter, string[] types);
        int GetRightsCountLike(string filter,int tipoelezione, string[] types);

        int GetRightsCount(int tipoelezione);


    }
}
