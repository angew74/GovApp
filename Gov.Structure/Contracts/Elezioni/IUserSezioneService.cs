using Gov.Core.Entity.Elezioni;
using Gov.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Contracts.Elezioni
{
    public interface IUserSezioneService : IEntityService<UsersSezioni>
    {

        UsersSezioni findById(int id);
        UsersSezioni findBySezioneAndTipoelezione(Sezioni sezione, Tipoelezione tipoElezione);
        UsersSezioni findBySezioneIdAndTipoelezioneId(int sezioneid, int idtipoelezione);
        List<UsersSezioni> findAllBy();
        List<UsersSezioni> findByUser(ApplicationUser user);
        List<UsersSezioni> findByUserIdAndTipoelezioneId(int userid, int tipoElezione);
        List<UsersSezioni> findByTipoelezione(Tipoelezione tipoElezione);
        List<UsersSezioni> findByTipoelezioneIdAndUserId(int tipoelezioneid, uint userid);     
        List<UsersSezioni> findByTipoelezioneIdAndSezionePlessoId(int tipoelezioneid, int plessoid);
        void deleteAllBySezioneInAndUser(List<int> list, uint u,int tipoelezioneid);
    }
}
