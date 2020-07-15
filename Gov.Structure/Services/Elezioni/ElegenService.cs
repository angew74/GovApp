using Gov.Structure.Contracts.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.Core.Entity.Elezioni;
using Gov.Structure.Contracts;

namespace Gov.Structure.Services.Elezioni
{
    public class ElegenService : EntityService<Elegen>, IElegenService
    {
        readonly IContext _context;

        public ElegenService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Elegen>();
        }

        public void delete(int id)
        {          
           
                Elegen elegen = _dbset.Find(id);
                _dbset.Remove(elegen);               
           
        }

        public Elegen findById(int id)
        {
                     
                return _dbset.Find(id);                              
           
        }

        public Elegen findByIdTipoElezione(int idtipoelezione)
        {
          
            
                return _dbset.Where(x=>x.Idtipoelezione == idtipoelezione).FirstOrDefault();

           
        }

        public Elegen findByTipoElezione(Tipoelezione tipoelezione)
        {
          
                  
                    return _dbset.Where(x => x.Idtipoelezione == tipoelezione.Id).FirstOrDefault();                

           
        }
    }
}
