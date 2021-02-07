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
    public class RicalcoloListaService : EntityService<RicalcoloVotiLista>, IRicalcoloListaService
    {
        readonly IContext _context;
        public RicalcoloListaService(IContext context)
          : base(context)
        {
            _context = context;
            _dbset = _context.Set<RicalcoloVotiLista>();
        }

        public List<RicalcoloVotiLista> findByComune(int tipoelezioneid)
        {
            return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Municipio == 99).Include(i => i.IdlistaNavigation).ToList();
        }

        public List<RicalcoloVotiLista> findByLista(int tipoelezioneid, int idlista)
        {
            return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Idlista == idlista).Include(i => i.IdlistaNavigation).ToList();
        }

        public RicalcoloVotiLista findByListaMunicipio(int tipoelezioneid, int idlista, int municipio)
        {
            return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Municipio == municipio && x.Idlista == idlista).Include(i => i.IdlistaNavigation).FirstOrDefault();
        }

        public List<RicalcoloVotiLista> findByMunicipio(int tipoelezioneid, int municipio)
        {
            return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid && x.Municipio == municipio).Include(i=>i.IdlistaNavigation).ToList();
        }

        public List<RicalcoloVotiLista> findOverMunicipio(int tipoelezioneid)
        {
            return _dbset.Where(x => x.Idtipoelezione == tipoelezioneid).Include(i => i.IdlistaNavigation).ToList();
        }
    }
}
