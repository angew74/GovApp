using Gov.Structure.Contracts.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.Core.Entity.Elezioni;
using Gov.Structure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Gov.Structure.Services.Elezioni
{
    public class VotiListaService :   EntityService<VotiLista>, IVotiListaService
    {

        readonly IContext _context;

        public VotiListaService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<VotiLista>();
        }

        public List<RicalcoloVotiLista> countLista(int tipoelezioneid)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).GroupBy(g => new { g.Listaid, g.Lista.Denominazione }).
                    Select(g => new RicalcoloVotiLista { NumeroVoti = g.Sum(i => i.Voti), Idlista = g.Key.Listaid, Denominazione = g.Key.Denominazione }).ToList();
            
        }

        public List<RicalcoloVotiLista> countListaByMunicipio(int tipoelezioneid, int municipio)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == municipio).GroupBy(g => new { g.Listaid, g.Lista.Denominazione, g.Municipio }).
                    Select(g => new RicalcoloVotiLista { NumeroVoti = g.Sum(i => i.Voti), Idlista = g.Key.Listaid, Denominazione = g.Key.Denominazione, Municipio = (int)g.Key.Municipio }).ToList();
            
        }

        public List<RicalcoloVotiLista> countListaSingle(int tipoelezioneid, int idlista)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Listaid == idlista).GroupBy(g => new { g.Listaid, g.Lista.Denominazione }).
                    Select(g => new RicalcoloVotiLista { NumeroVoti = g.Sum(i => i.Voti), Idlista = g.Key.Listaid, Denominazione = g.Key.Denominazione }).ToList();
            
        }

        public List<RicalcoloVotiLista> countListaSingleMunicipio(int tipoelezioneid, int idlista)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Listaid == idlista).GroupBy(g => new { g.Listaid, g.Lista.Denominazione, g.Municipio }).
                    Select(g => new RicalcoloVotiLista { NumeroVoti = g.Sum(i => i.Voti), Idlista = g.Key.Listaid, Denominazione = g.Key.Denominazione, Municipio = (int)g.Key.Municipio }).ToList();
            
        }

        public CountResult countPervenute(int tipoelezioneid)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).GroupBy(g => new { g.Tipoelezioneid }).
                    Select(g => new CountResult { Key = g.Key.Tipoelezioneid.ToString(), Value = g.Count().ToString() }).FirstOrDefault();
            
        }

        public CountResult countPervenuteByMunicipio(int tipoelezioneid, int municipio)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == municipio).GroupBy(g => new { g.Municipio }).
                    Select(g => new CountResult { Key = g.Key.Municipio.ToString(), Value = g.Count().ToString() }).FirstOrDefault();
            
        }

        public RicalcoloVotiLista countVotantiPervenute(int tipoelezioneid)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).GroupBy(g => new { g.Tipoelezioneid }).
                    Select(g => new RicalcoloVotiLista { Idtipoelezione = g.Key.Tipoelezioneid, IscrittiPervenute = g.Sum(item => item.Sezione.Iscritti.Iscrittitotaligen) }).FirstOrDefault();
            
        }

        public RicalcoloVotiLista countVotantiPervenuteByMunicipio(int tipoelezioneid, int municipio)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == municipio).GroupBy(g => new { g.Tipoelezioneid, g.Municipio }).
                    Select(g => new RicalcoloVotiLista { Idtipoelezione = g.Key.Tipoelezioneid, Municipio = (int)g.Key.Municipio, IscrittiPervenute = g.Sum(item => item.Sezione.Iscritti.Iscrittitotaligen) }).FirstOrDefault();
            
        }

        public List<VotiLista> findAllBy()
        {
          
            
                return _dbset.ToList();
            
        }

        public VotiLista findById(int id)
        {
          
            
                return _dbset.Find(id);
            
        }

        public VotiLista findByListaDenominazioneAndSezioneNumerosezioneAndTipoelezioneId(string denominazione, int numerosezione, int tipoElezioneId)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == numerosezione && x.Lista.Denominazione == denominazione).SingleOrDefault();
            
        }

        public VotiLista findByListaIdAndSezioneNumerosezioneAndTipoelezioneId(int listaid, int numerosezione, int tipoElezioneId)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == numerosezione && x.Lista.Id == listaid).SingleOrDefault();
            
        }

        public List<VotiLista> findByListaIdAndTipoelezioneId(int listaid, int tipoElezioneId)
        {
         
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Lista.Id == listaid).Include(i=>i.Lista).ToList();
            
        }

        public VotiLista findByListaProgressivoAndSezioneNumerosezioneAndTipoelezioneId(int progressivo, int numerosezione, int tipoElezioneId)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == numerosezione && x.Lista.Progressivo == progressivo).SingleOrDefault();
            
        }

        public List<VotiLista> findByListaProgressivoAndTipoelezioneId(int progressivo, int tipoElezioneId)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoElezioneId &&  x.Lista.Progressivo == progressivo).ToList();
            
        }

        public List<VotiLista> findBySezioneNumerosezioneAndTipoelezioneId(int numerosezione, int tipoelezioneid)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sezione.Numerosezione == numerosezione).Include(i=>i.Lista).Include(i=>i.Sezione).ToList();
            
        }
        public List<VotiLista> findByMunicipioAndTipoelezioneId(int municipio, int tipoelezioneid)
        {

            return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sezione.Municipio == municipio).Include(i=>i.Lista).ToList();

        }

        public List<VotiLista> findBySezioneNumerosezioneAndTipoelezioneIdAndListaCoalizioneId(int numerosezione, int tipoelezioneid, int coalizioneid)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sezione.Numerosezione == numerosezione && x.Lista.Coalizioneid == coalizioneid).ToList();
            
        }

        public List<VotiLista> findBySezionePlessoIdAndTipoelezioneId(int plessoid, int tipoelezioneid)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sezione.Idplesso == plessoid).ToList();
            
        }      

      
    }
}
