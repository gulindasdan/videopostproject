﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;
using VideoPostProject.Core.Entity.Enum;
using VideoPostProject.Core.Service;
using VideoPostProject.Model.Context;

namespace VideoPostProject.Service.Base
{
    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        private static ProjectContext _context;
        public ProjectContext context
        {
            get
            {
                if (_context == null)
                    _context = new ProjectContext();

                return _context;
            }
            set
            {
                _context = value;
            }
        }

        public bool Add(T item)
        {
            context.Set<T>().Add(item);
            return Save() > 0;
        }

        public bool Add(List<T> items)
        {
            context.Set<T>().AddRange(items);
            return Save() > 0;
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().Any(exp);
        }

        public List<T> GetActive()
        {
            return context.Set<T>().Where(x => x.Status == Status.Active || x.Status == Status.Updated || x.Status == Status.None).ToList();
        }

        public List<T> GetAll(Expression<Func<T, string>> exp)
        {
            return context.Set<T>().OrderBy(exp).ToList();
        }

        public T GetByDefault(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().Where(exp).FirstOrDefault();
        }

        public T GetByID(Guid id)
        {
            return context.Set<T>().Find(id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().Where(exp).ToList();
        }

        public bool Remove(T item)
        {
            item.Status = Status.Deleted;
            return Update(item);
        }

        public bool Remove(Guid id)
        {
            T item = context.Set<T>().Find(id);
            item.Status = Status.Deleted;
            DbEntityEntry entry = context.Entry(item);
            entry.CurrentValues.SetValues(item);
            return Save() > 0;
        }

        public bool RemoveAll(Expression<Func<T, bool>> exp)
        {
            var x = GetDefault(exp);
            int recordCount = x.Count;
            int successCount = 0;
            foreach (var item in x)
            {
                item.Status = Status.Deleted;
                bool i = Update(item);
                if (i) successCount++;
            }

            if (successCount == recordCount) return true;
            else return false;
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public bool Update(T item)
        {
            T updated = GetByID(item.ID);
            item.Status = Status.Updated;
            DbEntityEntry entry = context.Entry(updated);
            entry.CurrentValues.SetValues(item);
            return Save() > 0;

        }

        public void DetechEntity(T item)
        {
            context.Entry<T>(item).State = EntityState.Detached;
        }
        
    }
}
