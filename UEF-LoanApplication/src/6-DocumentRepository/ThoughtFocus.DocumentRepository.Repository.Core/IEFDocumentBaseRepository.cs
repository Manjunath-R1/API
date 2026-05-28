using System;
using System.Linq;
using System.Linq.Expressions;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IBaseRepository<T>
        where T : class
    {
        #region Methods

        void Add(T entity);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        void Update(T entity);

        #endregion Methods
    }
}
