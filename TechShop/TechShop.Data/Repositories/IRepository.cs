namespace TechShop.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class 
    {
        IQueryable<T> All();

        IQueryable<T> Find(Expression<Func<T, bool>> expression);

        T GetById(object id);

        T Add(T entity);

        void AddRange(IList<T> entities);

        T Update(T entity);

        void Delete(T entity);

        void Detach(T entity);

        void SaveChanges();
    }
}
