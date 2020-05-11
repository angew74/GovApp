using Gov.Core.Entity;
using Gov.Structure.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gov.Structure.Services
{
    public class PaginaService : EntityService<Pagina>, IPaginaService
    {
        readonly IContext _context;

        public PaginaService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Pagina>();
        }

        public Pagina GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public List<Pagina> GetByCodice(string codice)
        {
            return _dbset.Include(d => d.Contenuti).Where(x => x.Codice.ToUpper() == codice.ToUpper()).ToList();
        }

    }
}
