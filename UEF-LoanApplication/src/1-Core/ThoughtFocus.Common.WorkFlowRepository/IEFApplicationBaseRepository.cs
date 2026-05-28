namespace ThoughtFocus.Common.WorkFlowRepository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IEFApplicationBaseRepository<T>
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