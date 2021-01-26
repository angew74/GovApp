using Gov.Core.Entity.Elezioni;
using Gov.Structure.Contracts;
using Gov.Structure.Contracts.Elezioni;
using Microsoft.EntityFrameworkCore;
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
             return _dbset.Where(x=> x.Sezione.Numerosezione == numerosezione && x.Tipoelezioneid == tipoelezioneid).Include(i=>i.Sezione).ThenInclude(i=>i.Iscritti).SingleOrDefault();            
        }

        public List<Voti> findByTipoelezioneId(int tipoelezioneid)
        {
            var result = _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).GroupBy(g => new { g.Nulle, g.SoloSindaco, g.Totale, g.TotaleValide, g.Contestate, g.Bianche }).Select(z => new { z.Key.Bianche, z.Key.Contestate, z.Key.Nulle, z.Key.SoloSindaco, z.Key.Totale, z.Key.TotaleValide });
            var voti = result.Select(x => new Voti(x.Bianche, x.Contestate, 99, x.Nulle, x.SoloSindaco, x.Totale, x.TotaleValide)).ToList();
            return voti;
        }

        public List<Voti> findByMunicipioAndTipoelezioneId(int municipio, int tipoelezioneid)
        {

            var result = _dbset.Where(x => x.Sezione.Municipio == municipio && x.Tipoelezioneid == tipoelezioneid).GroupBy(g => new { g.Sezione.Municipio, g.Nulle, g.SoloSindaco, g.Totale, g.TotaleValide, g.Contestate, g.Bianche }).Select(z => new {z.Key.Bianche, z.Key.Contestate, z.Key.Municipio, z.Key.Nulle, z.Key.SoloSindaco, z.Key.Totale, z.Key.TotaleValide});
            var voti = result.Select(x => new Voti(x.Bianche, x.Contestate, x.Municipio, x.Nulle, x.SoloSindaco, x.Totale, x.TotaleValide)).ToList();
            return voti;
        }

        public List<VotiGenerali> findBySezionePlessoIdAndTipoelezioneId(int plessoid, int tipoelezioneid)
        {         
            
              return _dbset.Where(x => x.Sezione.Idplesso == plessoid && x.Tipoelezioneid == tipoelezioneid).ToList();            
        }

      
    }
    
}
