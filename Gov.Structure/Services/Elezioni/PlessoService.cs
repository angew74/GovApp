using Gov.Core.Entity.Elezioni;
using Gov.Structure.Contracts;
using Gov.Structure.Contracts.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Elezioni
{
    public class PlessoService : EntityService<Plessi>, IPlessoService
    {

        readonly IContext _context;

        public PlessoService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Plessi>();
        }

        public void deleteById(uint id)
        {
                     
                Plessi p = _dbset.Find(id);
                _dbset.Remove(p);            
          
        }

        public Plessi findById(uint id)
        {
          
            
               return _dbset.Find(id);
            
        }

        public List<Plessi> findByMunicipio(int mun)
        {
          
            
               return _dbset.Where(x => x.Municipio==mun).ToList();
            
        }

        public List<Plessi> findByTipoelezione(Tipoelezione tipoelezione)
        {
          
            
               return _dbset.Where(x => x.Idtipoelezione == tipoelezione.Id).ToList();
            
        }

        public List<Plessi> findByTipoelezioneId(int idtipoelezione)
        {
          
            
               return _dbset.Where(x => x.Idtipoelezione == idtipoelezione).ToList();
            
        }

        public List<Plessi> findByTipoelezioneIdAndDescrizioneLike(int tipoElezioneId, string descrizione)
        {
          
            
               return _dbset.Where(x => x.Idtipoelezione == tipoElezioneId && x.Descrizione.ToUpper().Contains(descrizione.ToUpper())).ToList();
            
        }

        public List<Plessi> findByTipoelezioneIdAndUbicazioneLike(int tipoElezioneId, string ubicazione)
        {
          
            
               return _dbset.Where(x => x.Idtipoelezione == tipoElezioneId && x.Ubicazione.ToUpper().Contains(ubicazione.ToUpper())).ToList();
            
        }
    }
}
