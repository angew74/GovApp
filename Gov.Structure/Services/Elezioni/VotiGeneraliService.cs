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

        public List<VotiGenerali> findBySezionePlessoIdAndTipoelezioneId(int plessoid, int tipoelezioneid)
        {         
            
              return _dbset.Where(x => x.Sezione.Idplesso == plessoid && x.Tipoelezioneid == tipoelezioneid).ToList();            
        }
        public Voti countGenerale(int tipoelezioneid)
        {           
            return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).GroupBy(o => "1")
                  .Select(g => new Voti {Bianche= g.Sum(s=> s.Bianche), Contestate = g.Sum(s=>s.Contestate), Nulle= g.Sum(s=>s.Nulle),Municipio = 99, SoloSindaco = g.Sum(s=>s.SoloSindaco), Totale = g.Sum(s=>s.Totale), TotaleValide = g.Sum(s=>s.TotaleValide),  SezioniPervenute = g.Count(),Iscritti = g.Sum(s=>s.Iscritti)}).FirstOrDefault();
        }
        public Voti countGeneraleByMunicipio(int tipoelezioneid)
        {
            return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).GroupBy(g => g.Municipio)
                  .Select(g => new Voti { Bianche = g.Sum(s => s.Bianche), Contestate = g.Sum(s => s.Contestate), Nulle = g.Sum(s => s.Nulle), Municipio = (int)g.Key, SoloSindaco = g.Sum(s => s.SoloSindaco), Totale = g.Sum(s => s.Totale), TotaleValide = g.Sum(s => s.TotaleValide), SezioniPervenute = g.Count(), Iscritti = g.Sum(s => s.Iscritti) }).FirstOrDefault();
        }

        public Voti countGeneraleOverMunicipio(int tipoelezioneid, int municipio)
        {
            return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == municipio).GroupBy(g => g.Municipio)
                .Select(g => new Voti { Bianche = g.Sum(s => s.Bianche), Contestate = g.Sum(s => s.Contestate), Nulle = g.Sum(s => s.Nulle), Municipio = (int)g.Key, SoloSindaco = g.Sum(s => s.SoloSindaco), Totale = g.Sum(s => s.Totale), TotaleValide = g.Sum(s => s.TotaleValide), SezioniPervenute = g.Count(), Iscritti = g.Sum(s => s.Iscritti) }).FirstOrDefault();
        }


    }
    
}
