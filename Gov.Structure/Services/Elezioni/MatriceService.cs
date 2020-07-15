using Gov.Core.Entity.Elezioni;
using Gov.Structure.Contracts;
using Gov.Structure.Contracts.Elezioni;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Elezioni
{
    public class MatriceService : EntityService<Matrice>, IMatriceService
    {
        readonly IContext _context;

        public MatriceService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Matrice>();
        }


        public void delete(int id)
        {         
            

             var matrice = _dbset.Find(id);
                _dbset.Remove(matrice);          
           
        }

        public Matrice findByIdTipoElezione(int idtipoelezione)
        {
          
            
                return _dbset.Where(x => x.Idtipoelezione == idtipoelezione).SingleOrDefault();
            
        }

        public Matrice findByTipoElezione(Tipoelezione tipoelezione)
        {
          
            
                return _dbset.Where(x => x.Idtipoelezione == tipoelezione.Id).SingleOrDefault();
            
        }

        public Matrice findMatriceById(int id)
        {
          
            
                return _dbset.Find(id);
            
        }

        public Matrice findMatriceByMun(int mun)
        {
          
            
                return _dbset.Where(x => x.Municipio == mun).SingleOrDefault();
            
        }
    }
}
