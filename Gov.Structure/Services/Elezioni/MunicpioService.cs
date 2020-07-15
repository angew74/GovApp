using Gov.Core.Entity.Elezioni;
using Gov.Structure.Contracts;
using Gov.Structure.Contracts.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Elezioni
{
    public class MunicpioService :  EntityService<Municipi>, IMunicipioService
    {

        readonly IContext _context;

        public MunicpioService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Municipi>();
        }


        public void delete(int id)
        {
          
           

                var municipio = _dbset.Find(id);
                _dbset.Remove(municipio);            
           
        }

        public Municipi findById(int id)
        {
          
            
              return _dbset.Find(id);
            
        }

        public Municipi findBymunicipio(int id)
        {
          
            
                return _dbset.Where(x=>x.Municipio == id).SingleOrDefault();
            
        }

        public List<Municipi> getAll()
        {
          
            
                return _dbset.ToList();
            
        }
    }
}
