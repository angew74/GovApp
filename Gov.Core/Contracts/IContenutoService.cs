using Gov.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Core.Contracts
{
    public interface IContenutoService :IEntityService<Contenuto>
    {
        Contenuto GetById(int Id);
        List<Contenuto> GetByCodicePagina(string codice);
        List<Contenuto> GetByPaginaId(int id);
    }
}
