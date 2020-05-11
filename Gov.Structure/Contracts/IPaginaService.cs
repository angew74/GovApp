using Gov.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Structure.Contracts
{
    public interface IPaginaService : IEntityService<Pagina>
    {
        Pagina GetById(int Id);

        List<Pagina> GetByCodice(string codice);
    }
}
