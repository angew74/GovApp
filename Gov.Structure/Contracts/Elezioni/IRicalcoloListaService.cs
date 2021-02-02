using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Structure.Contracts.Elezioni
{
    public interface IRicalcoloListaService: IEntityService<RicalcoloVotiLista>
    {
        RicalcoloVotiLista findByListaMunicipio(int tipoelezioneid, int idlista, int municipio);
        List<RicalcoloVotiLista> findByLista(int tipoelezioneid, int idlista);
        List<RicalcoloVotiLista> findByMunicipio(int tipoelezioneid,int municipio);
        List<RicalcoloVotiLista> findOverMunicipio(int tipoelezioneid);

        List<RicalcoloVotiLista> findByComune(int tipoelezioneid);
    }
}
