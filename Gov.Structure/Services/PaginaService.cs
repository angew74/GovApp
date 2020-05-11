using Gov.Core.Entity;
using Gov.Structure.Contracts;
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

        public Pagina GetByCodice(string codice)
        {
            return _dbset.FirstOrDefault(x => x.Codice.ToUpper() == codice.ToUpper());
        }

    }
}
