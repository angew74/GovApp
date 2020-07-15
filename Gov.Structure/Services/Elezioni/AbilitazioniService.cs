using Gov.Core.Entity.Elezioni;
using Gov.Structure.Contracts;
using Gov.Structure.Contracts.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Elezioni
{
    public class AbilitazioniService : EntityService<FaseElezione>, IAbilitazioniService
    {

        readonly IContext _context;

        public AbilitazioniService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<FaseElezione>();
        }

        public int Count(int tipoelezioneid)
        {          
                return  _dbset.Where(x=>x.Idtipoelezione == tipoelezioneid).Count();            
        }

        public List<FaseElezione> findAllByTipoelezioneId(int tipoelezioneid, int page, int pagesize)
        {
          
                return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Skip((page -1) * pagesize).Take(pagesize).ToList();
            
        }

        public List<FaseElezione> findByAbilitataAndTipoelezioneId(int abil, int tipoelezioneid, int page, int pagesize)
        {            
                return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Abilitata == abil).Skip(page * pagesize).Take(pagesize).ToList();           
           
        }

        public List<FaseElezione> findByAbilitataAndTipoelezioneId(int abilitata, int tipoelezioneid)
        {
           return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Abilitata == abilitata).ToList();
            
        }

        public FaseElezione findByCodiceAndTipoelezioneId(string codice, int tipoelezioneid)
        {
           return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Codice == codice).FirstOrDefault();
            
        }

        public FaseElezione findFaseElezioneByCodice(string codice)
        {
           
                return _dbset.Where(x => x.Codice == codice).FirstOrDefault();
            
        }

        public FaseElezione findFaseElezioneById(int id)
        {
               return _dbset.Find(id);            
        }

        public List<FaseElezione> getAll()
        {            
                return _dbset.ToList();
            
        }        

       
    }
}
