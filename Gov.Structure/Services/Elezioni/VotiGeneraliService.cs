using Gov.Core.Entity.Elezioni;
using Gov.Structure.Contracts;
using Gov.Structure.Contracts.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Elezioni
{
    public class VotiGeneraliService : EntityService<VotiGenerali>, IVotiGeneraliService
    {

        readonly IContext _context;

        public VotiGeneraliService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<VotiGenerali>();
        }

        public List<VotiGenerali> findAllBy()
        {
          
            
                return _dbset.ToList();
            
        }

        public VotiGenerali findById(int id)
        {
          
            
                return _dbset.Find(id);
            
        }

        public VotiGenerali findBySezioneNumerosezioneAndTipoelezioneId(int numerosezione, int tipoelezioneid)
        {
          
            
                return _dbset.Where(x=> x.Sezione.Numerosezione == numerosezione && x.Tipoelezioneid == tipoelezioneid).SingleOrDefault();
            
        }

        public List<VotiGenerali> findBySezionePlessoIdAndTipoelezioneId(int plessoid, int tipoelezioneid)
        {
          
            
                return _dbset.Where(x => x.Sezione.Idplesso == plessoid && x.Tipoelezioneid == tipoelezioneid).ToList();
            
        }     

      

       
    }
}
