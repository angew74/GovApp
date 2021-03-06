﻿using Gov.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gov.Core.Contracts
{
    public abstract class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        protected IContext _context;
        protected DbSet<T> _dbset;

        public EntityService(IContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }


        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }

            _dbset.Add(entity);
            _context.SaveChanges();
        }

        public virtual void CreateRange(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities is null");
            }

            _dbset.AddRange(entities);
            _context.SaveChanges();
        }


        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity is null");
            _dbset.Remove(entity);
            _context.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable<T>();
        }

        public void DeleteRange(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities is null");
            }

            _dbset.RemoveRange(entities);
            _context.SaveChanges();
        }
    }
}
