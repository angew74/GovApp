using Gov.Core.Entity;
using Gov.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Gov.Structure.Services
{
    public class VoceService : EntityService<VoceMenu>, IVoceMenuService
    {

        readonly IContext _context;

        public VoceService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<VoceMenu>();
        }

        public VoceMenu GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public List<VoceMenu> GelAllPlusRoles()
        {
            return _dbset.Include(i=>i.Role).ToList();
        }
    }
}
