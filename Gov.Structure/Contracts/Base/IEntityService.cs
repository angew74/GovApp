using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Structure.Contracts
{
    public interface IEntityService<T> : IService
    where T : BaseEntity
    {
        void Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}
