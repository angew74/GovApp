using Gov.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Structure.Contracts
{
    public interface IContenutoService :IEntityService<Contenuto>
    {
        Contenuto GetById(int Id);
        List<Contenuto> GetByCodicePagina(string codice);
    }
}
