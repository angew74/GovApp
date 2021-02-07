using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Core.Contracts.Elezioni
{
    public interface IRicalcoloSindacoService : IEntityService<RicalcoloVotiSindaco>
    {
        RicalcoloVotiSindaco findBySindacoMunicipio(int tipoelezioneid, int idsindaco, int municipio);
        List<RicalcoloVotiSindaco> findBySindaco(int tipoelezioneid, int idsindaco);
        List<RicalcoloVotiSindaco> findByMunicipio(int tipoelezioneid, int municipio);
        List<RicalcoloVotiSindaco> findOverMunicipio(int tipoelezioneid);

       List<RicalcoloVotiSindaco> findByComune(int tipoelezioneid);
    }
}
