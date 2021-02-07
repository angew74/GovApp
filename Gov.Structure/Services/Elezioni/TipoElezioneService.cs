using Gov.Core.Entity.Elezioni;
using Gov.Core.Contracts;
using Gov.Core.Contracts.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Elezioni
{
    public class TipoElezioneService :  EntityService<Tipoelezione>, ITipoElezioneService
    {

        readonly IContext _context;

        public TipoElezioneService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Tipoelezione>();
        }

        public Tipoelezione findElezioneByDescrizione(string codice)
        {
          
            
               return _dbset.Where(x => x.Descrizione == codice).FirstOrDefault();
            
        }

        public Tipoelezione findTipoElezioneById(int id)
        {
          
            
               return _dbset.Find(id);
            
        }

        public List<Tipoelezione> getAll()
        {
          
            
               return _dbset.ToList();
            
        }
    }
}
