﻿
using Gov.Core.Entity.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Contracts.Elezioni
{
    public interface ITipoElezioneService : IEntityService<Tipoelezione>
    {
        public Tipoelezione findTipoElezioneById(int id);
        public Tipoelezione findElezioneByDescrizione(String codice);
        public List<Tipoelezione> getAll();

    }
}
