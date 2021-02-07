using Gov.Core.Entity.Elezioni;
using Gov.Core.Contracts;
using Gov.Core.Contracts.Elezioni;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Elezioni
{
    public class VotiSindacoService : EntityService<VotiSindaco>, IVotiSindacoService
    {


        readonly IContext _context;

        public VotiSindacoService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<VotiSindaco>();
        }

        public CountResult countPervenute(int tipoelezioneid)
        {
          
            {
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).GroupBy(g => new { g.Tipoelezioneid }).
                    Select(g => new CountResult { Key = g.Key.Tipoelezioneid.ToString(), Value = g.Count().ToString() }).FirstOrDefault();
            }
        }

        public CountResult countPervenuteByMunicipio(int tipoelezioneid, int municipio)
        {
          
            {
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == municipio).GroupBy(g => new { g.Municipio }).
                    Select(g => new CountResult { Key = g.Key.Municipio.ToString(), Value = g.Count().ToString() }).FirstOrDefault();
            }
        }

        public List<RicalcoloVotiSindaco> countSindaco(int tipoelezioneid)
        {
          
            {
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).GroupBy(g => new { g.Sindacoid, g.Sindaco.Nome, g.Sindaco.Cognome }).
                    Select(g => new RicalcoloVotiSindaco { NumeroVoti = g.Sum(i => i.NumeroVoti), NumeroVotiSoloSindaco = g.Sum(i => i.NumeroVotiSoloSindaco), Sindacoid = g.Key.Sindacoid, Sindaco = new Sindaci { Id = g.Key.Sindacoid, Nome = g.Key.Nome, Cognome = g.Key.Cognome } }).ToList();
            }
        }

        public List<RicalcoloVotiSindaco> countSindacoByMunicipio(int tipoelezioneid, int municipio)
        {
          
            {
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == municipio).GroupBy(g => new { g.Sindacoid, g.Sindaco.Nome, g.Sindaco.Cognome, g.Municipio }).
                    Select(g => new RicalcoloVotiSindaco { NumeroVoti = g.Sum(i => i.NumeroVoti), NumeroVotiSoloSindaco = g.Sum(i => i.NumeroVotiSoloSindaco), Sindacoid = g.Key.Sindacoid, Municipio = g.Key.Municipio , Sindaco = new Sindaci { Id = g.Key.Sindacoid, Nome = g.Key.Nome, Cognome = g.Key.Cognome } }).ToList();
            }
        }

        public RicalcoloVotiSindaco countSindacoSingle(int tipoelezioneid, int idsindaco)
        {
          
            {
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sindacoid == idsindaco).GroupBy(g => new { g.Sindacoid, g.Sindaco.Nome, g.Sindaco.Cognome }).
                    Select(g => new RicalcoloVotiSindaco { NumeroVoti = g.Sum(i => i.NumeroVoti), NumeroVotiSoloSindaco = g.Sum(i => i.NumeroVotiSoloSindaco), Sindacoid = g.Key.Sindacoid, Sindaco = new Sindaci { Id = g.Key.Sindacoid, Nome = g.Key.Nome, Cognome = g.Key.Cognome } }).SingleOrDefault();
            }
        }

        public List<RicalcoloVotiSindaco> countSindacoSingleMunicipio(int tipoelezioneid, int idsindaco)
        {
          
            {
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sindacoid == idsindaco).GroupBy(g => new { g.Sindacoid, g.Sindaco.Nome, g.Sindaco.Cognome, g.Municipio }).
                    Select(g => new RicalcoloVotiSindaco { NumeroVoti = g.Sum(i => i.NumeroVoti), NumeroVotiSoloSindaco = g.Sum(i => i.NumeroVotiSoloSindaco), Sindacoid = g.Key.Sindacoid, Municipio = g.Key.Municipio, Sindaco = new Sindaci { Id = g.Key.Sindacoid, Nome = g.Key.Nome, Cognome = g.Key.Cognome } }).ToList();
            }
        }

        public List<RicalcoloVotiSindaco> countVotantiPervenute(int tipoelezioneid)
        {
          
            {
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).GroupBy(g => new { g.Sindacoid, g.Sindaco.Nome, g.Sindaco.Cognome }).
                    Select(g => new RicalcoloVotiSindaco { NumeroVoti = g.Sum(i => i.NumeroVoti), NumeroVotiSoloSindaco = g.Sum(i => i.NumeroVotiSoloSindaco), IscrittiPervenute = g.Sum(i => i.Sezione.Iscritti.Iscrittitotaligen), Sindacoid = g.Key.Sindacoid, Sindaco = new Sindaci { Id = g.Key.Sindacoid, Nome = g.Key.Nome, Cognome = g.Key.Cognome } }).ToList();
            }
        }

        public List<RicalcoloVotiSindaco> countVotantiPervenuteByMunicipio(int tipoelezioneid, int municipio)
        {
          
            {
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == municipio).GroupBy(g => new { g.Sindacoid, g.Sindaco.Nome, g.Sindaco.Cognome, g.Municipio }).
                    Select(g => new RicalcoloVotiSindaco { NumeroVoti = g.Sum(i => i.NumeroVoti), NumeroVotiSoloSindaco = g.Sum(i => i.NumeroVotiSoloSindaco), IscrittiPervenute = g.Sum(i => i.Sezione.Iscritti.Iscrittitotaligen), Sindacoid = g.Key.Sindacoid, Municipio = g.Key.Municipio, Sindaco = new Sindaci { Id = g.Key.Sindacoid, Nome = g.Key.Nome, Cognome = g.Key.Cognome } }).ToList();
            }
        }

        public List<VotiSindaco> findAllBy()
        {
                return _dbset.ToList();
        }

        public VotiSindaco findById(int id)
        {
                return _dbset.Find(id);
        }

        public List<VotiSindaco> findBySezioneNumerosezioneAndTipoelezioneId(int numerosezione, int tipoelezioneid)
        {       
                return _dbset.Where(x=>x.Sezione.Numerosezione == numerosezione && x.Tipoelezioneid == tipoelezioneid).Include(i=>i.Sindaco).Include(i => i.Votigenerali).Include(i=> i.VotiLista).ThenInclude(i=>i.Lista).ToList();
        }

        public List<VotiSindaco> findBySezionePlessoIdAndTipoelezioneId(int plessoid, int tipoelezioneid)
        {
                return _dbset.Where(x => x.Sezione.Idplesso == plessoid && x.Tipoelezioneid == tipoelezioneid).ToList();
            
        }

        public VotiSindaco findBySindacoIdAndSezioneNumerosezioneAndTipoelezioneId(int sindacoid, int numerosezione, int tipoElezioneId)
        {
          
            
                return _dbset.Where(x => x.Sezione.Numerosezione == numerosezione && x.Sezioneid == sindacoid  && x.Tipoelezioneid == tipoElezioneId).SingleOrDefault();
            
        }

        public List<VotiSindaco> findBySindacoIdAndTipoelezioneId(int sindacoid, int tipoElezioneId)
        {
          
            
                return _dbset.Where(x => x.Sindacoid == sindacoid && x.Tipoelezioneid == tipoElezioneId).ToList();
            
        }

        public VotiSindaco findBySindacoNomeAndSindacoCognomeAndSezioneNumerosezioneAndTipoelezioneId(string nome, string cognome, int numerosezione, int tipoElezioneId)
        {
          
            
                return _dbset.Where(x => x.Sezione.Numerosezione == numerosezione && x.Tipoelezioneid == tipoElezioneId && x.Sindaco.Nome == nome && x.Sindaco.Cognome == cognome ).SingleOrDefault();
            
        }

        public VotiSindaco findBySindacoProgressivoAndSezioneNumerosezioneAndTipoelezioneId(int progressivo, int numerosezione, int tipoElezioneId)
        {
          
            
                return _dbset.Where(x => x.Sezione.Numerosezione == numerosezione && x.Tipoelezioneid == tipoElezioneId && x.Sindaco.Progressivo == progressivo).SingleOrDefault();
            
        }

        public List<VotiSindaco> findBySindacoProgressivoAndTipoelezioneId(int progressivo, int tipoElezioneId)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sindaco.Progressivo == progressivo).ToList();
            
        }
        
    }
}
