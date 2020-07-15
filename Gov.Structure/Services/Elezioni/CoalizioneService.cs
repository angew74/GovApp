using Gov.Core;
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
    public class RaggruppamentoService : EntityService<Raggruppamento>, IRaggruppamentoService
    {

        readonly IContext _context;

        public RaggruppamentoService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Raggruppamento>();
        }
        public List<Raggruppamento> findAllBy()
        {
          
           
                return _dbset.ToList();

            
        }

        public List<Raggruppamento> findAllByTipoelezioneId(int tipoElezione)
        {
          
            
                return _dbset.Where(x=>x.Idtipoelezione== tipoElezione).ToList();

            
        }

        public Raggruppamento findByDenominazioneAndTipoelezioneId(string denominazione, int tipoelezioneid)
        {
          
            
                return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Denominazione.ToUpper() == denominazione.ToUpper()).FirstOrDefault();

            
        }

        public Raggruppamento findById(int id)
        {
          
            
                return _dbset.Find(id); 

            
        }

        public Raggruppamento findBySindacoIdAndTipoelezioneId(int sindacoid, int tipoelezioneid)
        {
          
            
                return _dbset.Include(i => i.Sindaco).Where(x => x.Idtipoelezione == tipoelezioneid && x.Sindacoid== sindacoid).FirstOrDefault();

            
        }

        public Raggruppamento findBySindacoNomeAndSindacoCognomeAndTipoelezioneId(string nome, string cognome, int tipoelezioneid)
        {
          
            
                return _dbset.Include(i=>i.Sindaco).Where(x => x.Idtipoelezione == tipoelezioneid && x.Sindaco.Nome.ToUpper() == nome.ToUpper() && x.Sindaco.Cognome.ToUpper() == cognome.ToUpper()).FirstOrDefault();
            
        }
    }
}
