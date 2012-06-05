using System.Linq;

namespace Testing.Repository
{
    public interface IRepository
    {
        T GetByKey<T>(object key) where T : class;
        IQueryable<T> Query<T>(ISpecification<T> specification) where T : class;
        T Single<T>(ISpecification<T> specification) where T : class;
        T First<T>(ISpecification<T> specification) where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(ISpecification<T> specification) where T : class;
        int Count<T>(ISpecification<T> specification) where T : class;
    }
}