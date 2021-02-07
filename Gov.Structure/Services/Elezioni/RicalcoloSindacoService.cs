using Gov.Core.Entity.Elezioni;
using Gov.Core.Contracts;
using Gov.Core.Contracts.Elezioni;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gov.Structure.Services.Elezioni
{
    public class RicalcoloSindacoService : EntityService<RicalcoloVotiSindaco>, IRicalcoloSindacoService
    {

        readonly IContext _context;
        public RicalcoloSindacoService(IContext context)
          : base(context)
        {
            _context = context;
            _dbset = _context.Set<RicalcoloVotiSindaco>();
        }
        public List<RicalcoloVotiSindaco> findByComune(int tipoelezioneid)
        {
            return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == 99).Include(i => i.Sindaco).ToList();
        }

        public List<RicalcoloVotiSindaco> findByMunicipio(int tipoelezioneid, int municipio)
        {
            return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == municipio).Include(i =>i.Sindaco).ToList();
        }

        public List<RicalcoloVotiSindaco> findBySindaco(int tipoelezioneid, int idsindaco)
        {
            return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sindacoid ==idsindaco).Include(i => i.Sindaco).ToList();
        }

        public RicalcoloVotiSindaco findBySindacoMunicipio(int tipoelezioneid, int idsindaco, int municipio)
        {
            return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Sindacoid == idsindaco && x.Municipio == municipio).Include(i => i.Sindaco).FirstOrDefault();
        }

        public List<RicalcoloVotiSindaco> findOverMunicipio(int tipoelezioneid)
        {
            return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).Include(i => i.Sindaco).ToList();
        }
    }
}
