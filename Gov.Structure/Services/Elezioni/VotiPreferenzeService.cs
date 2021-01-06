using Gov.Core.Entity.Elezioni;
using Gov.Structure.Contracts;
using Gov.Structure.Contracts.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Elezioni
{
    public class VotiPreferenzeService : EntityService<VotiPreferenze>, IVotiPreferenzeService
    {

        readonly IContext _context;

        public VotiPreferenzeService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<VotiPreferenze>();
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

        public int countPervenuteBySezioneAndTipoElezione(int numerosezione, int tipoelezioneid)
        {
            return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sezione.Numerosezione == numerosezione).Count();
        }

        public RicalcoloPreferenze countVotantiPervenute(int tipoelezioneid)
        {
          
            {
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).GroupBy(g => new { g.Tipoelezioneid }).
                    Select(g => new RicalcoloPreferenze { Tipoelezioneid = g.Key.Tipoelezioneid, IscrittiPervenute = g.Sum(item => item.Sezione.Iscritti.Iscrittitotaligen) }).FirstOrDefault();
            }
        }

        public RicalcoloPreferenze countVotantiPervenuteByMunicipio(int tipoelezioneid, int municipio)
        {
          
            {
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == municipio).GroupBy(g => new { g.Tipoelezioneid, g.Municipio }).
                    Select(g => new RicalcoloPreferenze { Tipoelezioneid = g.Key.Tipoelezioneid, Municipio = g.Key.Municipio, IscrittiPervenute = g.Sum(item => item.Sezione.Iscritti.Iscrittitotaligen) }).FirstOrDefault();
            }
        }

        public List<VotiPreferenze> findAllBy()
        {
          
            {
                return _dbset.ToList();
            }
        }

        public List<VotiPreferenze> findByCandidatoIdAndTipoelezioneId(int id, int tipoElezioneId)
        {
          
            {
                return _dbset.Where(x => x.Candidato.Id == id && x.Tipoelezioneid == tipoElezioneId).ToList();
            }

        }
        public List<VotiPreferenze> findByCandidatoIdAndTipoelezioneIdAndSezioneId(int id, int tipoElezioneId, int sezioneId)
        {
          
            {
                return _dbset.Where(x => x.Candidato.Id == id && x.Tipoelezioneid == tipoElezioneId && x.Sezioneid == sezioneId).ToList();
            }
        }

        public List<VotiPreferenze> findByCandidatoIdAndTipoelezioneIdAndSezioneMunicipio(int id, int tipoElezioneId, int municipio)
        {
          
            {
                return _dbset.Where(x => x.Candidato.Id == id && x.Tipoelezioneid == tipoElezioneId && x.Municipio == municipio).ToList();
            }
        }

        public VotiPreferenze findById(int id)
        {
          
            {
                return _dbset.Find(id);
            }
        }

        public List<VotiPreferenze> findByListaDenominazioneAndSezioneNumerosezioneAndTipoelezioneId(string denominazione, int numerosezione, int tipoElezioneId)
        {
          
            {
                return _dbset.Where(x => x.Lista.Denominazione == denominazione && x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == numerosezione).ToList();
            }
        }

        public List<VotiPreferenze> findByListaDenominazioneAndTipoelezioneId(string denominazione, int tipoElezioneId)
        {
          
            {
                return _dbset.Where(x => x.Lista.Denominazione == denominazione && x.Tipoelezioneid == tipoElezioneId).ToList();
            }
        }

        public List<VotiPreferenze> findByListaIdAndSezioneNumerosezioneAndTipoelezioneId(int listaid, int numerosezione, int tipoElezioneId)
        {
          
            {
                return _dbset.Where(x => x.Lista.Id == listaid && x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == numerosezione).ToList();
            }
        }

        public List<VotiPreferenze> findByListaIdAndTipoelezioneId(int listaid, int tipoElezioneId)
        {
          
            {
                return _dbset.Where(x => x.Lista.Id == listaid && x.Tipoelezioneid == tipoElezioneId).ToList();
            }
        }

        public List<VotiPreferenze> findByListaProgressivoAndSezioneNumerosezioneAndTipoelezioneId(int progressivo, int numerosezione, int tipoElezioneId)
        {
          
            {
                return _dbset.Where(x => x.Lista.Progressivo == progressivo && x.Tipoelezioneid == tipoElezioneId && x.Sezione.Numerosezione == numerosezione).ToList();
            }
        }

        public List<VotiPreferenze> findByListaProgressivoAndTipoelezioneId(int progressivo, int tipoElezioneId)
        {
          
            {
                return _dbset.Where(x => x.Lista.Progressivo == progressivo && x.Tipoelezioneid == tipoElezioneId).ToList();
            }
        }

        public List<VotiPreferenze> findBySezioneNumerosezioneAndTipoelezioneId(int numerosezione, int tipoelezioneid)
        {
          
            {
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sezione.Numerosezione == numerosezione).ToList();
            }
        }      
      
    }
}
