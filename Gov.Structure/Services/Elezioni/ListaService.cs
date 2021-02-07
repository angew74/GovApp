using Gov.Core.Contracts.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.Core.Contracts;
using Gov.Core.Entity.Elezioni;

namespace Gov.Structure.Services.Elezioni
{
    public class ListaService :  EntityService<Liste>, IListaService
    {

        readonly IContext _context;

        public ListaService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Liste>();
        }
        public List<Liste> findAllBy()
        {
          
           
               return _dbset.ToList();
            
        }

        public List<Liste> findAllByTipoelezioneId(int tipoElezione)
        {
          
            
               return _dbset.Where(x=>x.Tipoelezioneid== tipoElezione).ToList();
            
        }

        public List<Liste> findByCoalizioneDenominazioneAndTipoelezioneId(string denominazione, int tipoelezioneid)
        {
          
            
               return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Raggruppamenti.Denominazione == denominazione).ToList();
            
        }

        public List<Liste> findByCoalizioneIdAndTipoelezioneId(int idcoalizione, int tipoelezioneid)
        {
          
            
               return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Raggruppamenti.Id == idcoalizione).ToList();
            
        }

        public List<Liste> findByCoalizioneSindacoIdAndTipoelezioneId(int sindacoid, int tipoelezioneid)
        {
          
            
               return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sindacoid == sindacoid).ToList();
            

        }

        public Liste findByDenominazioneAndTipoelezioneId(string denominazione, int tipoelezioneid)
        {
          
            
               return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Denominazione == denominazione).FirstOrDefault();
            
        }

        public Liste findById(int id)
        {
          
            
               return _dbset.Find(id);
            
        }

        public Liste findByProgressivoAndTipoelezioneId(int progressivo, int tipoelezioneid)
        {
          
            
               return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Progressivo == progressivo).FirstOrDefault();
            
        }

        public List<Liste> findBySindacoId(int sindacoid)
        {
          
            
               return _dbset.Where(x => x.Sindacoid == sindacoid).ToList();
            
        }

        public List<Liste> findBySindacoIdAndTipoelezioneId(int sindacoid, int tipoelezioneid)
        {
          
            
               return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sindacoid == sindacoid).ToList();
            
        }

        public List<Liste> findBySindacoNomeAndSindacoCognomeAndTipoelezioneId(string nome, string cognome, int tipoelezioneid)
        {
          
            
               return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sindaco.Nome== nome && x.Sindaco.Cognome == cognome).ToList();
            
        }
    }
}
