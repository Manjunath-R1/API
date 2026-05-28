namespace ThoughtFocus.DocumentRepository.Repository.Neo4j
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using ThoughtFocus.DocumentRepository.Repository.Core;

    public class DocumentRepositoryNeo4jContext
    {

    }

    public abstract class AbstractNeo4jBaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        #region Fields

        //private DocumentRepositoryNeo4jContext context;

        #endregion Fields

        #region Constructors

        public AbstractNeo4jBaseRepository()
        {
            //this.context = context;
        }

        #endregion Constructors

        #region Methods

        public virtual void Add(T entity)
        {
            //this.context.Set<T>().Add(entity);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return null;
            //return this.context.Set<T>().Where(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return null;// return this.context.Set<T>().Where(predicate).FirstOrDefault();
        }

        public virtual IQueryable<T> GetAll()
        {
            return null;// return this.context.Set<T>();
        }

        public virtual void
            Update(T entity)
        {
            //this.context.Entry(entity).State = EntityState.Modified;
        }

        #endregion Methods
    }
}