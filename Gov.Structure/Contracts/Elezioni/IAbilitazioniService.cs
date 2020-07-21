using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Elezioni
{
   public interface IAbilitazioniService :IEntityService<FaseElezione>
    {

        FaseElezione findFaseElezioneById(int id);
        FaseElezione findFaseElezioneByCodice(String codice);
        List<FaseElezione> getAll();       
        FaseElezione findByCodiceAndTipoelezioneId(String codice, int tipoElezioneId);
        List<FaseElezione> findAllByTipoelezioneId(int tipoelezioneid,int page,int pagesize);
        List<FaseElezione> findByAbilitataAndTipoelezioneId(int abil, int tipoelezioneid, int page, int pagesize);
        List<FaseElezione> findByAbilitataAndTipoelezioneId(int abilitata, int tipoelezioneid);
        List<FaseElezione> findByCategoria(string categoria, int tipoelezioneid);
        int Count(int tipoelezioniid);
       
    }
}
