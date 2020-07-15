using Gov.Core.Entity.Elezioni;
using Gov.Structure.Contracts;
using Gov.Structure.Contracts.Elezioni;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Elezioni
{
    public class IscrittiService : EntityService<Iscritti>, IIscrittiService
    {

        readonly IContext _context;

        public IscrittiService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Iscritti>();
        }

        public List<CountResult> countAllByTipoelezioneId(int TipoElezioneId)
        {
          
            
              return _dbset.Where(x=>x.Idtipoelezione == TipoElezioneId).GroupBy(x=>new { x.Idtipoelezione }).Select(g => new CountResult {Key= g.Key.Idtipoelezione.ToString(), Value = g.Count().ToString() }).ToList();
            
        }

        public List<CountResult> countAllByTipoelezioneIdAndMunicipio(int mun, int TipoElezioneId)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == TipoElezioneId && x.Municipio == mun).GroupBy(x => new { x.Idtipoelezione, x.Municipio }).Select(g => new CountResult { Key = g.Key.Idtipoelezione.ToString(), SecondKey = g.Key.Municipio.ToString(), Value = g.Count().ToString() }).ToList();
            
        }

        public List<Iscritti> countIscrittiPervenute(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).GroupBy(x => new { x.Idtipoelezione }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Idtipoelezione,
                    Iscrittimaschigen = g.Sum(i => i.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscrittitotaligen),
                }).ToList();
            
        }

        public List<Iscritti> countIscrittiPervenuteByMun(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).GroupBy(x => new { x.Municipio }).Select(g => new Iscritti
                {
                    Municipio = g.Key.Municipio,
                    Iscrittimaschigen = g.Sum(i => i.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscrittitotaligen),
                }).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteAllAffluenza1(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Affluenza1 == 1).GroupBy(x => new { x.Tipoelezioneid }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteAllAffluenza2(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Affluenza2 == 1).GroupBy(x => new { x.Tipoelezioneid }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteAllAffluenza3(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Affluenza3 == 1).GroupBy(x => new { x.Tipoelezioneid }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteAllApertura1(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Apertura1 == 1).GroupBy(x => new { x.Tipoelezioneid }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteAllApertura2(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Apertura2 == 1).GroupBy(x => new { x.Tipoelezioneid }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteAllCostituzione1(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Costituzione1 == 1).GroupBy(x => new { x.Tipoelezioneid }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteAllCostituzione2(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Costituzione2 == 1).GroupBy(x => new { x.Tipoelezioneid }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteMunicipioAffluenza1(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Affluenza1 == 1).GroupBy(x => new { x.Tipoelezioneid, x.Sezione.Municipio }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Municipio = (int)g.Key.Municipio,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteMunicipioAffluenza2(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Affluenza2 == 1).GroupBy(x => new { x.Tipoelezioneid, x.Sezione.Municipio }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Municipio = (int)g.Key.Municipio,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteMunicipioAffluenza3(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Affluenza3 == 1).GroupBy(x => new { x.Tipoelezioneid, x.Sezione.Municipio }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Municipio = (int)g.Key.Municipio,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteMunicipioApertura1(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Apertura1 == 1).GroupBy(x => new { x.Tipoelezioneid, x.Sezione.Municipio }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Municipio = (int)g.Key.Municipio,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteMunicipioApertura2(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Apertura2 == 1).GroupBy(x => new { x.Tipoelezioneid, x.Sezione.Municipio }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Municipio = (int)g.Key.Municipio,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteMunicipioCostituzione1(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Costituzione1 == 1).GroupBy(x => new { x.Tipoelezioneid, x.Sezione.Municipio }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Municipio = (int)g.Key.Municipio,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public List<Iscritti> countIscrittiSezioniPervenuteMunicipioCostituzione2(int tipoelezioneid)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.Affluenze.Where(a => a.Costituzione2 == 1).GroupBy(x => new { x.Tipoelezioneid, x.Sezione.Municipio }).Select(g => new Iscritti
                {
                    Idtipoelezione = g.Key.Tipoelezioneid,
                    Municipio = (int)g.Key.Municipio,
                    Iscrittimaschigen = g.Sum(i => i.Iscritti.Iscrittimaschigen),
                    Iscrittifemminegen = g.Sum(i => i.Iscritti.Iscrittifemminegen),
                    Iscrittitotaligen = g.Sum(i => i.Iscritti.Iscrittitotaligen),
                })).ToList();
            
        }

        public void delete(int id)
        {
          
            
                Iscritti iscritti = _dbset.Find(id);
                _dbset.Remove(iscritti);                
            
        }

        public List<Iscritti> findByIdTipoElezione(int idtipoelezione)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == idtipoelezione).ToList();
            
        }

        public Iscritti findBySezioneIdAndTipoElezioneId(int idsezione, int tipoElezioneId)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoElezioneId &&  x.IdsezioneNavigation.Id == idsezione).FirstOrDefault();
            
        }

        public List<Iscritti> findByTipoElezione(Tipoelezione tipoelezione)
        {
          
            
              return _dbset.Where(x => x.Idtipoelezione == tipoelezione.Id).ToList();
            
        }

        public Iscritti findByTipoelezioneIdAndSezioneNumerosezione(int tipoElezioneId, int numerosezione)
        {
          
            
              return _dbset.Include(i=>i.IdtipoelezioneNavigation).Include(i => i.IdsezioneNavigation).Include(i=>i.IdtiposezioneNavigation).Where(x => x.Idtipoelezione == tipoElezioneId && x.IdsezioneNavigation.Numerosezione == numerosezione).ToList().First();
            
        }

        public Iscritti findIscrittiById(int id)
        {
          
            
              return _dbset.Find(id);
            
        }

        public List<Iscritti> findIscrittiByMunicipioAndTipoElezioneId(int mun, int tipoElezioneId)
        {
          
           
              return _dbset.Where(x => x.Idtipoelezione == tipoElezioneId && x.Municipio== mun).ToList();
            
        }
    }
}
