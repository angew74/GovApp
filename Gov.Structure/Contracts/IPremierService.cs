using Gov.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Structure.Contracts
{
    public interface IPremierService :IEntityService<Premier>
    {
        Premier GetById(int Id);
    }
}
