﻿using Gov.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Core.Contracts
{
    public interface IVoceMenuService : IEntityService<VoceMenu>
    {
        VoceMenu GetById(int Id);
        List<VoceMenu> GelAllPlusRoles();
     
    }
}
