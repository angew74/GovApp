using Gov.Core.Entity;
using Gov.Structure.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Gov.Structure.Services
{
   public class ContenutoService : EntityService<Contenuto>, IContenutoService
    {
        readonly IContext _context;

        public ContenutoService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Contenuto>();
        }

        public List<Contenuto> GetByCodicePagina(string codice)
        {
            return _dbset.Include(i=>i.Pagina).Where(x => x.Pagina.Codice.ToLower() == codice.ToLower()).ToList();
        }

        public Contenuto GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public List<Contenuto> GetByPaginaId(int id)
        {
            return _dbset.Include(i => i.Pagina).Where(x => x.Pagina.Id == id).ToList();
        }
    }
}