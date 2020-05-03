using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Core.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
