using Gov.Core.Entity.Elezioni;
using Gov.Core.Contracts;
using Gov.Core.Contracts.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gov.Structure.Services.Elezioni
{
    public class VotiSindacoStoricoService : EntityService<VotiSindacoStorico>, IVotiSindacoStoricoService
    {


        readonly IContext _context;

        public VotiSindacoStoricoService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<VotiSindacoStorico>();
        }    

        public VotiSindacoStorico findById(int id)
        {
            return _dbset.Find(id);
        }

        public List<VotiSindacoStorico> findBySezioneNumerosezioneAndTipoelezioneId(int numerosezione, int tipoelezioneid)
        {
            return _dbset.Where(x => x.Sezione.Numerosezione == numerosezione && x.Tipoelezioneid == tipoelezioneid).ToList();

        }     

        public VotiSindacoStorico findBySindacoIdAndSezioneNumerosezioneAndTipoelezioneId(int sindacoid, int numerosezione, int tipoElezioneId)
        {
            return _dbset.Where(x => x.Sezione.Numerosezione == numerosezione && x.Sezioneid == sindacoid && x.Tipoelezioneid == tipoElezioneId).SingleOrDefault();

        }

        public List<VotiSindacoStorico> findBySindacoIdAndTipoelezioneId(int sindacoid, int tipoElezioneId)
        {
            return _dbset.Where(x => x.Sindacoid == sindacoid && x.Tipoelezioneid == tipoElezioneId).ToList();

        }      

    }
}
