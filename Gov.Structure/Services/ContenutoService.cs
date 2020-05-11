using Gov.Core.Entity;
using Gov.Structure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Contenuto GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

       
    }
}