using Gov.Core.Entity.Elezioni;
using Gov.Core.Identity;
using Gov.Structure.Contracts;
using Gov.Structure.Contracts.Elezioni;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Elezioni
{
    public class UserSezioneService : EntityService<UsersSezioni>, IUserSezioneService
    {

        readonly IContext _context;

        public UserSezioneService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<UsersSezioni>();
        }


        public void deleteAllBySezioneInAndUser(List<int> list, int u,int tipoelezioneid)
        {
          
            
              var l = _dbset.Where(x => x.UserId == u && x.Idtipoelezione == tipoelezioneid).ToList();
              var d =  l.Where(x => list.Contains((int)x.Sezioneid));
             _dbset.RemoveRange(d);          
             

        }

        public List<UsersSezioni> findAllBy()
        {
          
            {
              return _dbset.ToList();
            }
        }

        public UsersSezioni findById(int id)
        {
          
            {
              return _dbset.Find(id);
            }
        }

        public UsersSezioni findBySezioneAndTipoelezione(Sezioni sezione, Tipoelezione tipoElezione)
        {
          
            {
              return _dbset.Where(x => x.Idtipoelezione == tipoElezione.Id && x.Sezione == sezione).FirstOrDefault();
            }
        }

        public UsersSezioni findBySezioneIdAndTipoelezioneId(int sezioneid, int idtipoelezione)
        {
          
            {
              return _dbset.Where(x => x.Idtipoelezione == idtipoelezione && x.Sezioneid == sezioneid).Include(i=>i.User).FirstOrDefault();
            }
        }

        public List<UsersSezioni> findByTipoelezione(Tipoelezione tipoElezione)
        {
          
            {
              return _dbset.Where(x => x.Idtipoelezione == tipoElezione.Id).ToList();
            }
        }

        public List<UsersSezioni> findByTipoelezioneIdAndSezionePlessoId(int tipoelezioneid, int plessoid)
        {
          
            {
              return _dbset.Include(u=>u.Sezione).Include(i=>i.User).Where(x => x.Idtipoelezione == tipoelezioneid && x.Sezione.Idplesso == plessoid).ToList();
            }
        }

        public List<UsersSezioni> findByTipoelezioneIdAndUserId(int tipoelezioneid, int userid)
        {
          
            {
              return _dbset.Include(u=>u.Sezione).Include(i=>i.Sezione.IdplessoNavigation).Include(g=>g.Sezione.IdtiposezioneNavigation).Where(x => x.Idtipoelezione == tipoelezioneid && x.UserId == userid).ToList();
            }
        }

        public List<UsersSezioni> findByUser(ApplicationUser user)
        {
          
            {
              return _dbset.Where(x => x.User == user).ToList();
            }
        }

        public List<UsersSezioni> findByUserIdAndTipoelezioneId(int userid, int tipoElezione)
        {
          
            {
              return _dbset.Where(x => x.Idtipoelezione == tipoElezione && x.UserId == userid).ToList();
            }
        }
       
    }
}
