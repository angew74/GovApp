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
    public class SezioneService : EntityService<Sezioni>, ISezioneService
    {

        readonly IContext _context;

        public SezioneService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Sezioni>();
        }
        public List<CountResult> countAllByTipoelezioneIdAndMunicipioAndTipoelezioneIdIn(int tipoElezioneId, int municipio)
        {
            
                return _dbset.Where(x => x.Idtipoelezione == tipoElezioneId).GroupBy(x => new { x.Idtipoelezione, x.Municipio }).Select(g => new CountResult
                {
                    Key = g.Key.Idtipoelezione.ToString(),
                    SecondKey = g.Key.Municipio.ToString(),
                    Value = g.Count().ToString()
                }).ToList();
            
        }

        public List<CountResult> countAllByTipoelezioneIdAndTipoelezioneIdIn(int tipoElezione)
        {

            
                return _dbset.Where(x => x.Idtipoelezione == tipoElezione).GroupBy(x => new { x.Idtipoelezione }).Select(g => new CountResult
                {
                    Key = g.Key.Idtipoelezione.ToString(),
                    Value = g.Count().ToString()
                }).ToList();
            
        }

        public List<Sezioni> findAllBy()
        {
            return _dbset.ToList();
        }

        public Sezioni findById(int id)
        {


            return _dbset.Find(id);

        }

        public Sezioni findByNumerosezioneAndCabinaAndTipoelezioneId(int numeroSezione, int cabina, int tipoElezioneId)
        {

            
                return _dbset.Where(x => x.Numerosezione == numeroSezione && x.Cabina == cabina && x.IdtipoelezioneNavigation.Id == tipoElezioneId).FirstOrDefault();
            
        }

        public Sezioni findByNumerosezioneAndTipoelezioneId(int numeroSezione, int tipoElezioneId)
        {

            
                return _dbset.Include(i => i.Iscritti).Include(i => i.IdtipoelezioneNavigation).Include(i => i.IdtiposezioneNavigation).Include(i=>i.IdplessoNavigation).Include(i=>i.UsersSezioni).Where(x => x.Numerosezione == numeroSezione && x.IdtipoelezioneNavigation.Id == tipoElezioneId).FirstOrDefault();
            
        }

        public List<Sezioni> findByPlessoIdAndTipoelezioneId(int plessoid, int tipoElezioneId)
        {
                        
                return _dbset.Where(x => x.IdplessoNavigation.Id == plessoid && x.IdtipoelezioneNavigation.Id == tipoElezioneId).ToList();
            
        }
    }
}
