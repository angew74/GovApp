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
    public class AffluenzaService : EntityService<Affluenze>, IAffluenzaService
    {

        readonly IContext _context;

        public AffluenzaService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Affluenze>();
        }

        public List<RicalcoliAffluenza> countAffluenza1(int tipoelezioneid)
        {
            return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Affluenza1 == 1).GroupBy(g => new { g.Affluenza1 }).
                    Select(g => new RicalcoliAffluenza {NumeroAffluenza = 1, NumeroSezioni = g.Count(), AffluenzaTotale = (int)g.Sum(i => i.Votantitotali1), AffluenzaFemmine = (int)g.Sum(i => i.Votantifemmine1),AffluenzaMaschi = (int)g.Sum(i => i.Votantimaschi1)}).ToList();
            
        }

        public List<RicalcoliAffluenza> countAffluenza1ByMunicipio(int tipoelezioneid)
        {
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Affluenza1 == 1).GroupBy(g => new { g.Affluenza1, g.Sezione.Municipio }).
                    Select(g => new RicalcoliAffluenza { NumeroAffluenza = 1, Municipio = (int)g.Key.Municipio, NumeroSezioni = g.Count(), AffluenzaTotale = (int)g.Sum(i => i.Votantitotali1), AffluenzaFemmine = (int)g.Sum(i => i.Votantifemmine1), AffluenzaMaschi = (int)g.Sum(i => i.Votantimaschi1) }).ToList();
            
        }

        public List<RicalcoliAffluenza> countAffluenza2(int tipoelezioneid)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Affluenza2 == 1).GroupBy(g => new { g.Affluenza2 }).
                    Select(g => new RicalcoliAffluenza { NumeroAffluenza = 2, NumeroSezioni = g.Count(), AffluenzaTotale = (int)g.Sum(i => i.Votantitotali2), AffluenzaFemmine = (int)g.Sum(i => i.Votantifemmine2), AffluenzaMaschi = (int)g.Sum(i => i.Votantimaschi2) }).ToList();
            
        }

        public List<RicalcoliAffluenza> countAffluenza2ByMunicipio(int tipoelezioneid)
        {           
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Affluenza2 == 1).GroupBy(g => new { g.Affluenza2, g.Sezione.Municipio }).
                    Select(g => new RicalcoliAffluenza { NumeroAffluenza = 2, Municipio = (int)g.Key.Municipio, NumeroSezioni = g.Count(), AffluenzaTotale = (int)g.Sum(i => i.Votantitotali2), AffluenzaFemmine = (int)g.Sum(i => i.Votantifemmine2), AffluenzaMaschi = (int)g.Sum(i => i.Votantimaschi2) }).ToList();
           
        }

        public List<RicalcoliAffluenza> countAffluenza3(int tipoelezioneid)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Affluenza3 == 1).GroupBy(g => new { g.Affluenza3 }).
                    Select(g => new RicalcoliAffluenza { NumeroAffluenza = 3, NumeroSezioni = g.Count(), AffluenzaTotale = (int)g.Sum(i => i.Votantitotali3), AffluenzaFemmine = (int)g.Sum(i => i.Votantifemmine3), AffluenzaMaschi = (int)g.Sum(i => i.Votantimaschi3) }).ToList();
        }

        public List<RicalcoliAffluenza> countAffluenza3ByMunicipio(int tipoelezioneid)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Affluenza3 == 1).GroupBy(g => new { g.Affluenza3, g.Sezione.Municipio }).
                    Select(g => new RicalcoliAffluenza { NumeroAffluenza = 3, Municipio = (int)g.Key.Municipio, NumeroSezioni = g.Count(), AffluenzaTotale = (int)g.Sum(i => i.Votantitotali3), AffluenzaFemmine = (int)g.Sum(i => i.Votantifemmine3), AffluenzaMaschi = (int)g.Sum(i => i.Votantimaschi3) }).ToList();
            
        }

        public List<RicalcoloAperturaCostituzione> countApertura1(int tipoelezioneid)
        {
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Apertura1 == 1).GroupBy(g => new { g.Apertura1}).
                    Select(g => new RicalcoloAperturaCostituzione {NumeroCostituite = 0,  NumeroAperte = g.Count()}).ToList();
            
        }

        public List<RicalcoloAperturaCostituzione> countApertura1ByMunicipio(int tipoelezioneid)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Apertura1 == 1).GroupBy(g => new { g.Apertura1, g.Sezione.Municipio }).
                    Select(g => new RicalcoloAperturaCostituzione { NumeroCostituite = 0, NumeroAperte = g.Count(),Municipio = (int)g.Key.Municipio }).ToList();
            
        }

        public List<RicalcoloAperturaCostituzione> countApertura2(int tipoelezioneid)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Apertura2 == 1).GroupBy(g => new { g.Apertura2 }).
                    Select(g => new RicalcoloAperturaCostituzione { NumeroCostituite = 0, NumeroAperte = g.Count() }).ToList();
           
        }

        public List<RicalcoloAperturaCostituzione> countApertura2ByMunicipio(int tipoelezioneid)
        {
          
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Apertura2 == 1).GroupBy(g => new { g.Apertura2, g.Sezione.Municipio }).
                    Select(g => new RicalcoloAperturaCostituzione { NumeroCostituite = 0, NumeroAperte = g.Count(), Municipio = (int)g.Key.Municipio }).ToList();
            
        }

        public List<CountResult> countByAffluenza1AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione && x.Affluenza1 == a).GroupBy(g => new { g.Affluenza1, g.Sezione.Municipio }).
                    Select(g => new CountResult { Key = g.Key.Affluenza1.ToString(), SecondKey = g.Key.Municipio.ToString(), Value = g.Count().ToString() }).ToList();
          
        }

        public List<CountResult> countByAffluenza1AndTipoelezioneId(int a, int tipoElezione)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione).GroupBy(g => new { g.Affluenza1 }).
                    Select(g => new CountResult { Key = g.Key.Affluenza1.ToString(), Value = g.Count().ToString() }).ToList();
            
        }

        public List<CountResult> countByAffluenza2AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione && x.Affluenza2 == a).GroupBy(g => new { g.Affluenza1,g.Sezione.Municipio }).
                    Select(g => new CountResult { Key = g.Key.Affluenza1.ToString(),SecondKey =g.Key.Municipio.ToString(), Value = g.Count().ToString() }).ToList();
         
        }

        public List<CountResult> countByAffluenza2AndTipoelezioneId(int a, int tipoElezione)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione).GroupBy(g => new { g.Affluenza2 }).
                    Select(g => new CountResult { Key = g.Key.Affluenza2.ToString(), Value = g.Count().ToString() }).ToList();
           
        }

        public List<CountResult> countByAffluenza3AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione && x.Affluenza3 == a).GroupBy(g => new { g.Affluenza3, g.Sezione.Municipio }).
                    Select(g => new CountResult { Key = g.Key.Affluenza3.ToString(), SecondKey = g.Key.Municipio.ToString(), Value = g.Count().ToString() }).ToList();
          
        }

        public List<CountResult> countByAffluenza3AndTipoelezioneId(int a, int tipoElezione)
        {
         
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione).GroupBy(g => new { g.Affluenza3}).
                    Select(g => new CountResult { Key = g.Key.Affluenza3.ToString(), Value = g.Count().ToString() }).ToList();
           
        }

        public List<CountResult> countByApertura1AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione && x.Apertura1 == a).GroupBy(g => new { g.Apertura1, g.Sezione.Municipio }).
                    Select(g => new CountResult { Key = g.Key.Apertura1.ToString(), SecondKey = g.Key.Municipio.ToString(), Value = g.Count().ToString() }).ToList();
          
        }

        public List<CountResult> countByApertura1AndTipoelezioneId(int a, int tipoElezione)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione && x.Apertura1 == a).GroupBy(g => new { g.Apertura1 }).
                    Select(g => new CountResult { Key = g.Key.Apertura1.ToString(), Value = g.Count().ToString() }).ToList();
           
        }

        public List<CountResult> countByApertura2AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione)
        {
          
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione && x.Apertura2 == a).GroupBy(g => new { g.Apertura2, g.Sezione.Municipio }).
                    Select(g => new CountResult { Key = g.Key.Apertura2.ToString(), SecondKey = g.Key.Municipio.ToString(), Value = g.Count().ToString() }).ToList();
           
        }

        public List<CountResult> countByApertura2AndTipoelezioneId(int a, int tipoElezione)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione).GroupBy(g => new { g.Apertura2 }).
                    Select(g => new CountResult { Key = g.Key.Apertura2.ToString(), Value = g.Count().ToString() }).ToList();
       
        }

        public List<CountResult> countByCostituzione1AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione)
        {
          
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione && x.Costituzione1 == a).GroupBy(g => new { g.Costituzione1, g.Sezione.Municipio }).
                    Select(g => new CountResult { Key = g.Key.Costituzione1.ToString(), SecondKey = g.Key.Municipio.ToString(), Value = g.Count().ToString() }).ToList();
          
        }

        public List<CountResult> countByCostituzione1AndTipoelezioneId(int a, int tipoElezione)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione).GroupBy(g => new { g.Costituzione1 }).
                    Select(g => new CountResult { Key = g.Key.Costituzione1.ToString(), Value = g.Count().ToString() }).ToList();
            
        }

        public List<CountResult> countByCostituzione2AndSezione_MunicipioAndTipoelezioneId(int a, int municipio, int tipoElezione)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione && x.Costituzione2 == a).GroupBy(g => new { g.Costituzione2, g.Sezione.Municipio }).
                    Select(g => new CountResult { Key = g.Key.Costituzione2.ToString(), SecondKey = g.Key.Municipio.ToString(), Value = g.Count().ToString() }).ToList();
           
        }

        public List<CountResult> countByCostituzione2AndTipoelezioneId(int a, int tipoElezione)
        {
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezione && x.Costituzione2 == a).GroupBy(g => new { g.Costituzione2 }).
                    Select(g => new CountResult { Key = g.Key.Costituzione2.ToString(), Value = g.Count().ToString() }).ToList();
           
        }

        public List<RicalcoloAperturaCostituzione> countCostituzione1(int tipoelezioneid)
        {
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Costituzione1 == 1).GroupBy(g => new { g.Costituzione1 }).
                    Select(g => new RicalcoloAperturaCostituzione { NumeroCostituite = g.Count(), NumeroAperte = 0 }).ToList();
          
        }

        public List<RicalcoloAperturaCostituzione> countCostituzione1ByMunicipio(int tipoelezioneid)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Costituzione1 == 1).GroupBy(g => new { g.Costituzione1, g.Sezione.Municipio }).
                    Select(g => new RicalcoloAperturaCostituzione { NumeroCostituite = g.Count(), NumeroAperte = 0, Municipio = (int)g.Key.Municipio }).ToList();
          
        }

        public List<RicalcoloAperturaCostituzione> countCostituzione2(int tipoelezioneid)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Costituzione2 == 1).GroupBy(g => new { g.Costituzione2 }).
                    Select(g => new RicalcoloAperturaCostituzione { NumeroCostituite = g.Count(), NumeroAperte = 0 }).ToList();
          
        }

        public List<RicalcoloAperturaCostituzione> countCostituzione2ByMunicipio(int tipoelezioneid)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Costituzione2 == 1).GroupBy(g => new { g.Costituzione2, g.Sezione.Municipio }).
                    Select(g => new RicalcoloAperturaCostituzione { NumeroCostituite = g.Count(), NumeroAperte = 0, Municipio = (int)g.Key.Municipio }).ToList();
         
        }

        public void delete(long id)
        {            
               var a = _dbset.Find(id);
               _dbset.Remove(a);           
          
        }

        public List<Affluenze> findByAffluenza1AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Municipio == municipio && x.Affluenza1 == a).ToList();
        
        }

        public List<Affluenze> findByAffluenza1AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Numerosezione == numeroSezione && x.Affluenza1 == a).ToList();
           
        }

        public List<Affluenze> findByAffluenza2AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio)
        {
           
                return _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Municipio == municipio && x.Affluenza2 == a).ToList();
          
        }

        public List<Affluenze> findByAffluenza2AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione)
        {
          
            {
               return _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Numerosezione == numeroSezione && x.Affluenza2 == a).ToList();
            }
        }

        public List<Affluenze> findByAffluenza3AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio)
        {
          
            {
               return _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Municipio == municipio && x.Affluenza3 == a).ToList();
            };
        }

        public List<Affluenze> findByAffluenza3AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione)
        {
          
            {
               return _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Numerosezione == numeroSezione && x.Affluenza3 == a).ToList();
            }
        }

        public List<Affluenze> findByApertura1AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio)
        {
          
            {
               return _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Municipio == municipio && x.Apertura1 == a).ToList();
            };
        }

        public List<Affluenze> findByApertura1AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione)
        {
          
           
            return  _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Numerosezione == numeroSezione && x.Apertura1 == a).ToList();
          
        }

        public List<Affluenze> findByApertura2AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio)
        {
          
           
            return  _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Municipio == municipio && x.Apertura2 == a).ToList();
           
        }

        public List<Affluenze> findByApertura2AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione)
        {
          
          
              return _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Numerosezione == numeroSezione && x.Apertura2 == a).ToList();
            
        }

        public List<Affluenze> findByCostituzione1AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio)
        {
            throw new NotImplementedException();
        }

        public List<Affluenze> findByCostituzione1AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione)
        {
          
           
              return _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Numerosezione == numeroSezione && x.Costituzione1 == a).ToList();
            
        }

        public List<Affluenze> findByCostituzione2AndTipoelezioneIdAndSezione_Municipio(int a, int idTipoElezione, int municipio)
        {
          
           
             return  _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Municipio == municipio && x.Costituzione2 == a).ToList();
            
        }

        public List<Affluenze> findByCostituzione2AndTipoelezioneIdAndSezione_Numerosezione(int a, int idTipoElezione, int numeroSezione)
        {
          
            
             return  _dbset.Where(x => x.Tipoelezioneid == idTipoElezione && x.Sezione.Numerosezione == numeroSezione && x.Costituzione2 == a).ToList();
            
        }

        public Affluenze findById(int id)
        {
          
            
            return   _dbset.Find(id);
            
        }

        public Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza1(int sezione, int tipoElezioneId, int a)
        {
          
           
             return  _dbset.Include(i => i.Sezione).Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == sezione && x.Affluenza1 == a).SingleOrDefault();
            
        }

        public Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza2(int sezione, int tipoElezioneId, int a)
        {
          
            return  _dbset.Include(i => i.Sezione).Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == sezione && x.Affluenza2 == a).SingleOrDefault();
            
        }

        public Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza3(int sezione, int tipoElezioneId, int a)
        {
          
           
             return  _dbset.Include(i=>i.Sezione).Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == sezione && x.Affluenza3 == a).SingleOrDefault();
            
        }

        public Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza4(int sezione, int tipoElezioneId, int a)
        {
          
            
             return  _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == sezione && x.Affluenza4 == a).SingleOrDefault();
            
        }

        public Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza5(int sezione, int tipoElezioneId, int a)
        {
          
            
             return  _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == sezione && x.Affluenza5 == a).SingleOrDefault();
            
        }

        public Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndApertura1(int sezione, int tipoElezioneId, int a)
        {
          
            
            return   _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == sezione && x.Apertura1 == a).SingleOrDefault();
            
        }

        public Affluenze findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndCostituzione1(int sezione, int tipoElezioneId, int a)
        {
          
            
             return  _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == sezione && x.Costituzione1 == a).SingleOrDefault();
            
        }

        public Affluenze findBySezioneNumerosezioneAndTipoelezioneId(int sezione, int tipoElezioneId)
        {
          
           
            return   _dbset.Include(i=>i.Iscritti).Include(i=>i.Sezione).Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == sezione).SingleOrDefault();
            
        }

        public List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndAffluenza1(int plessoid, int tipoElezioneId, int a)
        {
                      
             return  _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Idplesso == plessoid && x.Affluenza1 == a).ToList();
            
        }

        public List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndAffluenza2(int plessoid, int tipoElezioneId, int a)
        {
          
           
             return  _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Idplesso == plessoid && x.Affluenza2 == a).ToList();
            
        }

        public List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndAffluenza3(int plessoid, int tipoElezioneId, int a)
        {
          
            
             return  _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Idplesso == plessoid && x.Affluenza3 == a).ToList();
            
        }

        public List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndAffluenza4(int plessoid, int tipoElezioneId, int a)
        {          
            
              return _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Idplesso == plessoid && x.Affluenza4 == a).ToList();
            
        }

        public List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndAffluenza5(int plessoid, int tipoElezioneId, int a)
        {
          
           
              return _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Idplesso == plessoid && x.Affluenza5 == a).ToList();
            
        }

        public List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndApertura1(int plessoid, int tipoElezioneId, int a)
        {
          
            
            return   _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Idplesso == plessoid && x.Apertura1 == a).ToList();
            
        }

        public List<Affluenze> findBySezionePlessoIdAndTipoelezioneIdAndCostituzione1(int plessoid, int tipoElezioneId, int a)
        {
          
            
             return  _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Idplesso == plessoid && x.Costituzione1 == a).ToList();
            
        }

       

        public List<Affluenze> findByTipoelezioneId(int idTipoElezione)
        {          
            
            return   _dbset.Where(x => x.Tipoelezioneid == idTipoElezione).ToList();
            
        }
        
    }
}
