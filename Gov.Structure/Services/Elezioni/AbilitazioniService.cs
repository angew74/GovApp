using Gov.Core.Entity.Elezioni;
using Gov.Core.Contracts;
using Gov.Core.Contracts.Elezioni;
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

        public List<FaseElezione> findAllByTipoelezioneId(int tipoelezioneid, int skip, int take)
        {
          
                return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Skip(skip).Take(take).ToList();
            
        }

        public List<FaseElezione> findByAbilitataAndTipoelezioneId(int abil, int tipoelezioneid, int skip, int take)
        {            
                return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Abilitata == abil).Skip(skip).Take(take).ToList();           
           
        }

        public List<FaseElezione> findByAbilitataAndTipoelezioneId(int abilitata, int tipoelezioneid)
        {
           return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Abilitata == abilitata).ToList();
            
        }

        public List<FaseElezione> findByCategoria(string categoria, int tipoelezioneid, int skip, int take)
        {
            return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Categoria.ToLower() == categoria.ToLower()).ToList();
        }

        public FaseElezione findByCodiceAndTipoelezioneId(string codice, int tipoelezioneid)
        {
           return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Codice == codice).FirstOrDefault();
            
        }

        public List<FaseElezione> findByDescrizioneLike(string descrizione, int tipoelezioneid, int skip, int take)
        {
            return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Descrizione.ToUpper().Contains(descrizione.ToUpper())).Skip(skip).Take(take).ToList();
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

        public List<FaseElezione> getRightsSortingBy(int tipoelezione,int skip, int take, string sortBy, bool sortDesc, string filter, string[] types)
        {
            List<FaseElezione> rights = new List<FaseElezione>();
            string ordining = "";
            if (string.IsNullOrEmpty(sortBy))
            { ordining = "UserName "; }
            else { ordining = sortBy; }
            if (sortDesc)
            { ordining += " DESC"; }
            else { ordining += " ASC"; }
            if (string.IsNullOrEmpty(filter))
            { rights =_dbset.Where(x=>x.Idtipoelezione == tipoelezione).OrderBy(ordining).Skip(skip).Take(take).AsParallel().ToList(); }
            else
            {
                foreach (string t in types)
                {
                    switch (t.ToLower())
                    {
                        case "codice":
                            rights = _dbset.Where(x => x.Codice.ToLower() == filter.ToLower()).OrderBy(ordining).Skip(skip).Take(take).AsParallel().ToList();
                            break;
                        case "descrizione":
                            rights = _dbset.Where(x => x.Descrizione.ToLower().Contains(filter.ToLower())).OrderBy(ordining).Skip(skip).Take(take).AsParallel().ToList();
                            break;
                        case "categoria":
                            rights = _dbset.Where(x => x.Categoria.ToLower() == filter.ToLower()).OrderBy(ordining).Skip(skip).Take(take).AsParallel().ToList();
                            break;

                    }
                }
            }
            return rights;
        }


        public int GetRightsCountLike(string filter,int tipoelezione, string[] types)
        {
            int count = 0;
            if (types.Length > 0)
            {
                foreach (string t in types)
                {
                    switch (t.ToLower())
                    {
                        case "categoria":
                            count += _dbset.Where(x => x.Categoria.ToLower() == filter.ToLower() && x.Idtipoelezione == tipoelezione).Count();
                            break;
                        case "codice":
                            count += _dbset.Where(x => x.Codice.ToLower() == filter.ToLower() && x.Idtipoelezione == tipoelezione).Count();
                            break;
                        case "descrizione":
                            count += _dbset.Where(x => x.Descrizione.ToLower().Contains(filter.ToLower()) && x.Idtipoelezione == tipoelezione).Count();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                count += _dbset.Where(x => x.Codice.ToLower() == filter.ToLower() || x.Categoria.ToLower() == filter.ToLower() || x.Descrizione.ToLower().Contains(filter.ToLower()) && x.Idtipoelezione == tipoelezione).Distinct().Count();

            }
            return count;
        }

        public int GetRightsCount(int tipoelezione)
        {
            return _dbset.Where(x=>x.Idtipoelezione== tipoelezione).Count();
        }
    }
}
