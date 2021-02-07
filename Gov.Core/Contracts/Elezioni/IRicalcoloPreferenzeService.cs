using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Core.Contracts.Elezioni
{
    public interface IRicalcoloPreferenzeService : IEntityService<RicalcoloPreferenze>
    {
        List<RicalcoloPreferenze> sumCandidato(int tipoelezioneid);
        List<RicalcoloPreferenze> sumCandidatoByLista(int tipoelezioneid, int idlista);
        List<RicalcoloPreferenze> sumCandidatoByListaMunicipio(int tipoelezioneid, int municipio, int idlista);

        List<RicalcoloPreferenze> sumCandidatoByMunicipio(int tipoelezioneid, int municipio);
    }
}
