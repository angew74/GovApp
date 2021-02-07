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
    public class SindacoService : EntityService<Sindaci>,  ISindacoService
    {
        readonly IContext _context;

        public SindacoService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Sindaci>();
        }


        public List<Sindaci> findAllBy()
        {
                      
                return _dbset.ToList();
            
        }

        public List<Sindaci> findAllByTipoelezioneId(int tipoElezione)
        {
          
            
                return _dbset.Include(i=>i.Liste).Where(x=>x.Tipoelezioneid == tipoElezione).ToList();
            
        }

        public Sindaci findById(int id)
        {
          
            
                return _dbset.Find(id); 
            
        }

        public Sindaci findByNomeAndCognomeAndTipoelezioneId(string nome, string cognome, int tipoelezioneid)
        {
          
            
                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Cognome.ToUpper() == cognome.ToUpper() && x.Nome == nome.ToUpper()).SingleOrDefault();
            
        }

        public Sindaci findByProgressivoAndTipoelezioneId(int progressivo, int tipoelezioneid)
        {          
            

                return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Progressivo == progressivo).SingleOrDefault();
            
        }
    }
}
